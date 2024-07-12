using BusinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Commands.Interface;
using RepositoryLayer.CustomException;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        public IUserCommand _userCommand { get; }
        public UserBL(IUserCommand userCommand)
        {
            _userCommand = userCommand;
        }


        public async Task<UserEntity> RegisterUserAsync(UserML model, string role)
        {
            try
            {
                var result = await _userCommand.RegisterAsync(model, role);

                return result;
            }
            catch (UserException)
            {
                throw;
            }
        }
    }
}
