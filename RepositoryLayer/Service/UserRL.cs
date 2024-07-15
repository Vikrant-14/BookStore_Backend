using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.CustomException;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        public ApplicationDBContext _context { get; }
        private readonly IConfiguration _configuration;
        private String encryptPassword;
        private String decryptPassword;
        public UserRL(ApplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<UserEntity> RegisterAsync(UserML model, string role) 
        {
            var findUser = _context.Users.Where(u => u.Email.Equals(model.Email)).FirstOrDefault();

            UserEntity user;

            if (findUser == null)
            {
                //model.Password = PasswordService.HashPassword(model.Password);
                encryptPassword = EncryptionHelper.Encrypt(model.Password);
                user = new UserEntity()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = encryptPassword,
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

        public async Task<string> LoginAsync(LoginML model)
        {
            var result = await _context.Users.Where(u => u.Email == model.Email).FirstOrDefaultAsync();

            if (result == null)
            {
                throw new UserException("Invalid Email/Password");
            }

            encryptPassword = EncryptionHelper.Encrypt(result.Password);
            decryptPassword = EncryptionHelper.Decrypt(encryptPassword);

            //if (PasswordService.VerifyPassword(model.Password, result.Password))
            //{
            //    return JwtTokenGenerator.GenerateToken(_context, _configuration, result);
            //}
            if (decryptPassword.Equals(result.Password))
            {
                return JwtTokenGenerator.GenerateToken(_context, _configuration, result);
            }
            else
            {
                throw new UserException("Invalid Email/Password");
            }
        }

        public async Task<UserEntity> GetUserbyId(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                throw new UserException($"User Id : {id} not found");
            }

            return user;
        }
    }
}
