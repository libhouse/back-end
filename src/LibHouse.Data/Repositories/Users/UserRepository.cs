using LibHouse.Business.Application.Users.Projections;
using LibHouse.Business.Entities.Users;
using LibHouse.Data.Context;
using LibHouse.Data.Repositories.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibHouse.Data.Repositories.Users
{
    public class UserRepository : EntityTypeRepository<User>, IUserRepository
    {
        public UserRepository(LibHouseContext context) 
            : base(context)
        {
        }

        public async Task<bool> CheckIfUserCpfIsNotRegisteredAsync(Cpf cpf)
        {
            return !await _dbSet.AnyAsync(u => u.CPF.Value == cpf.Value && u.Active);
        }

        public async Task<bool> CheckIfUserEmailIsNotRegisteredAsync(string email)
        {
            return !await _dbSet.AnyAsync(u => u.Email.Value == email && u.Active);
        }

        public async Task<User> GetUserByCpfAsync(string cpf)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.CPF.Value == cpf && u.Active);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email.Value == email && u.Active);
        }

        public async Task<LoggedUser> GetUserLoginDataByEmailAsync(string email)
        {
            Expression<Func<User, bool>> queryExpression = user => user.Email.Value == email;

            Expression<Func<User, LoggedUser>> userLoginDataProjection = user 
                => new LoggedUser(
                    user.Id, 
                    user.Name,
                    user.LastName, 
                    user.BirthDate, 
                    user.Gender, 
                    user.Email.Value,
                    user.UserType
                );

            return await GetProjectionAsync(queryExpression, userLoginDataProjection);
        }
    }
}