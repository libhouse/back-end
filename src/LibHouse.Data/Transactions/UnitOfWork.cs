using LibHouse.Business.Entities.Shared;
using LibHouse.Business.Entities.Users;
using LibHouse.Data.Context;
using LibHouse.Data.Repositories.Users;
using System.Threading.Tasks;

namespace LibHouse.Data.Transactions
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly LibHouseContext _context;

        private IUserRepository _userRepository;

        public UnitOfWork(LibHouseContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository
        {
            get => _userRepository ??= new UserRepository(_context);
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}