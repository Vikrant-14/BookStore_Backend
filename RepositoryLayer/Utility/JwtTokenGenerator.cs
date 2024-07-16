using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Utility
{
    public class JwtTokenGenerator
    {
        public static string GenerateToken(ApplicationDBContext context, IConfiguration configuration, UserEntity result)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, configuration["JWT:Subject"]),
                new Claim("Id", result.Id.ToString()),
                new Claim("Name", result.Name),
                new Claim("Email", result.Email)
            };

            var userRoles = context.Users.Where(u => u.Id == result.Id).ToList();   

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    configuration["JWT:Issuer"],
                    configuration["JWT:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
    }


}

