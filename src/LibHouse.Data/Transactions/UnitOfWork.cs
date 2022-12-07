using LibHouse.Business.Entities.Localizations;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Shared;
using LibHouse.Business.Entities.Users;
using LibHouse.Data.Context;
using LibHouse.Data.Repositories.Localizations;
using LibHouse.Data.Repositories.Residents;
using LibHouse.Data.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace LibHouse.Data.Transactions
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly LibHouseContext _context;

        private IUserRepository _userRepository;
        private IResidentRepository _residentRepository;
        private IAddressRepository _addressRepository;

        public UnitOfWork(LibHouseContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository
        {
            get => _userRepository ??= new UserRepository(_context);
        }

        public IResidentRepository ResidentRepository
        {
            get => _residentRepository ??= new ResidentRepository(_context);
        }

        public IAddressRepository AddressRepository
        {
            get => _addressRepository ??= new AddressRepository(_context);
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CommitAsync(Func<Task> action)
        {
            IExecutionStrategy strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async() =>
            {
                await using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
                await action();
                await transaction.CommitAsync();
            });
            return true;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}