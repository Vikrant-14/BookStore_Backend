using ModelLayer;
using RepositoryLayer.Commands.Interface;
using RepositoryLayer.CustomException;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.Service
{
    public class UserCommand : IUserCommand
    {
        public IUserRL _userRL { get; }
        public UserCommand(IUserRL userRL)
        {
            _userRL = userRL;
        }


        public async Task<UserEntity> RegisterAsync(UserML model, string role)
        {
            try
            {
                var user = await _userRL.RegisterAsync(model, role);

                return user;
            }
            catch(UserException) 
            {
                throw;
            }
        }
    }
}
