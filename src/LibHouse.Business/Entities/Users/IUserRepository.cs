using LibHouse.Business.Application.Users.Projections;
using LibHouse.Business.Entities.Shared;
using System.Threading.Tasks;

namespace LibHouse.Business.Entities.Users
{
    public interface IUserRepository : IEntityRepository<User>
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByCpfAsync(string cpf);
        Task<LoggedUser> GetUserLoginDataByEmailAsync(string email);
        Task<bool> CheckIfUserCpfIsNotRegisteredAsync(Cpf cpf);
        Task<bool> CheckIfUserEmailIsNotRegisteredAsync(string email);
    }
}