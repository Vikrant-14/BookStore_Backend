using ModelLayer;
using RepositoryLayer.CustomException;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Queries.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Queries.Service
{
    public class UserQuery : IUserQuery
    {
        public IUserRL UserRL { get; }
        public UserQuery(IUserRL userRL)
        {
            UserRL = userRL;
        }


        public async Task<string> LoginAsync(LoginML model)
        {
            try
            {
                var result = await UserRL.LoginAsync(model);

                return result;
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
                var result = await UserRL.GetUserbyId(id);

                return result;
            }
            catch (UserException)
            {
                throw;
            }
        }
    }
}
