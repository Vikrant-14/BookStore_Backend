using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.CustomException;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        public ApplicationDBContext _context { get; }
        
        public UserRL(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> RegisterAsync(UserML model, string role) 
        {
            var findUser = _context.Users.Where(u => u.Email.Equals(model.Email)).FirstOrDefault();

            UserEntity user;

            if (findUser == null)
            {
                model.Password = PasswordService.HashPassword(model.Password);

                user = new UserEntity()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber,
                    Role = role
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                
                return user; 
            }
            else
            {
                throw new UserException("User Already Exists");
            }
        }

    }
}
