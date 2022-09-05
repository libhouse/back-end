using LibHouse.Business.Entities.Users;
using System;
using System.Threading.Tasks;

namespace LibHouse.Business.Entities.Shared
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
        Task<bool> CommitAsync();
        IUserRepository UserRepository { get; }
    }
}