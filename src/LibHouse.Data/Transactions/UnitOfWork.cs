using LibHouse.Business.Entities.Localizations;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Shared;
using LibHouse.Business.Entities.Users;
using LibHouse.Data.Context;
using LibHouse.Data.Repositories.Localizations;
using LibHouse.Data.Repositories.Residents;
using LibHouse.Data.Repositories.Users;
using Microsoft.EntityFrameworkCore.Storage;
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

        private IDbContextTransaction DbContextTransaction { get; set; }

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
            _ = _context.SaveChanges();
            DbContextTransaction.Commit();
            return true;
        }

        public async Task<bool> CommitAsync()
        {
            _ = await _context.SaveChangesAsync();
            await DbContextTransaction.CommitAsync();
            return true;
        }

        public void Dispose()
        {
            DbContextTransaction?.Dispose();
            _context?.Dispose();
        }

        public void StartWork()
        {
            DbContextTransaction = _context.Database.BeginTransaction();
        }

        public async Task StartWorkAsync()
        {
            DbContextTransaction = await _context.Database.BeginTransactionAsync();
        }
    }
}