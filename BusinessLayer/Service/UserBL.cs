using BusinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Commands.Interface;
using RepositoryLayer.CustomException;
using RepositoryLayer.Entity;
using RepositoryLayer.Queries.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        public IUserCommand UserCommand { get; }
        public IUserQuery UserQuery { get; }

        public UserBL(IUserCommand userCommand, IUserQuery userQuery)
        {
            UserCommand = userCommand;
            UserQuery = userQuery;
        }


        public async Task<UserEntity> RegisterUserAsync(UserML model, string role)
        {
            try
            {
                 return await UserCommand.RegisterAsync(model, role);
            }
            catch (UserException)
            {
                throw;
            }
        }

        public async Task<string> LoginAsync(LoginML model)
        {
            try
            {
                return await UserQuery.LoginAsync(model);
            }
            catch (UserException)
            {
                throw;
            }
        }

        public async Task<UserEntity> GetUserbyId(int id)
        {
            try
            {
                return await UserQuery.GetUserbyId(id);
            }
            catch (UserException)
            {
                throw;
            }
        }
    }
}
