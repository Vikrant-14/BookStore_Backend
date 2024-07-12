using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.Interface
{
    public interface IUserCommand
    {
        public Task<UserEntity> RegisterAsync(UserML model, string role);
    }
}
