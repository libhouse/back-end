using LibHouse.Business.Entities.Localizations;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Users;
using System;
using System.Threading.Tasks;

namespace LibHouse.Business.Entities.Shared
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
        Task<bool> CommitAsync();
        Task<bool> CommitAsync(Func<Task> action);
        IUserRepository UserRepository { get; }
        IResidentRepository ResidentRepository { get; }
        IAddressRepository AddressRepository { get; }
    }
}