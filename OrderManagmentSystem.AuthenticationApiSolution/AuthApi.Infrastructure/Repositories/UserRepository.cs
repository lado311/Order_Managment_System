using AuthApi.Domain.Entities;
using AuthApi.Domain.Interfaces;
using AuthApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OrderManagment.SharedLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthApi.Infrastructure.Repositories
{
    public class UserRepository(UserDataContext context, IConfiguration configuration) : IUser
    {
        public async Task<User> CreateAsync(User entity)
        {
            var userFromDatabase = await GetByAsync(u => u.Email == entity.Email);
            if (userFromDatabase is not null) throw new BadRequestException("user already exist following this email!");

            entity.Password = BCrypt.Net.BCrypt.HashPassword(entity.Password);
            entity.Role = "User";
            var result = await context.Users.AddAsync(entity);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<User> DeleteAsync(User entity)
        {
            var result = context.Users.Remove(entity);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = await context.Users.ToListAsync();
            if (!users.Any()) throw new BadRequestException("users list is empty");

            return users;
        }

        public async Task<User> GetByAsync(Expression<Func<User, bool>> predicate)
        {
            var user = await context.Users.FirstOrDefaultAsync(predicate);
            return user is not null ? user : null!;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user is not null ? user : null!;
        }

        public async Task<string> LoginUser(User user)
        {
            var userFromDatabase = await GetByAsync(u => u.Email == user.Email);
            if (userFromDatabase is null) throw new NotFoundRequestException("user not found by this email");

            bool isCorrect = BCrypt.Net.BCrypt.Verify(user.Password, userFromDatabase.Password);
            if (!isCorrect) throw new BadRequestException("your password is incorrect");

            string token = GenerateToken(userFromDatabase);
            return token;
        }

        public async Task<User> UpdateAsync(User entity)
        {
            var result = context.Users.Update(entity);
            await context.SaveChangesAsync();
            return result.Entity;
        }


        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(configuration["Authentication:Key"]);
            var claims = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            });

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(1),
                Audience = configuration["Authentication:Audience"],
                Issuer = configuration["Authentication:Issuer"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); ;
        }
    }
}
