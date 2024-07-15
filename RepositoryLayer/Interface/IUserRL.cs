using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public Task<UserEntity> RegisterAsync(UserML model, string role);
        public Task<string> LoginAsync(LoginML model);
        public Task<UserEntity> GetUserbyId(int id);
    }
}
