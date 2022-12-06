using LibHouse.Business.Entities.Localizations;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Users;
using System;
using System.Threading.Tasks;

namespace LibHouse.Business.Entities.Shared
{
    public interface IUnitOfWork : IDisposable
    {
        void StartWork();
        Task StartWorkAsync();
        bool Commit();
        Task<bool> CommitAsync();
        IUserRepository UserRepository { get; }
        IResidentRepository ResidentRepository { get; }
        IAddressRepository AddressRepository { get; }
    }
}