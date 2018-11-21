using LinkYourLaundry.Models;
using LinkYourLaundry.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LinkYourLaundry.Services
{
    public class UserService
    {
        private readonly IConfiguration configuration;
        private readonly LaundryDbContext context;
        private readonly IPasswordHasher<User> passwordHasher;

        public UserService(IConfiguration configuration, LaundryDbContext context, IPasswordHasher<User> passwordHasher)
        {
            this.configuration = configuration;
            this.context = context;
            this.passwordHasher = passwordHasher;
        }

        public User GetById(int id)
        {
            return context.Users.Find(id);
        }

        public User GetByEmail(string email)
        {
            return context.Users.FirstOrDefault(u => u.Email == email);
        }

        public async Task<User> Register(RegisterViewModel viewModel)
        {
            var user = new User
            {
                UserName = viewModel.Username,
                Email = viewModel.Email,
                PasswordHash = passwordHasher.HashPassword(null, viewModel.Password)
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return user;
        }

        public JwtSecurityToken Login(LoginViewModel viewModel)
        {
            var user = context.Users.FirstOrDefault(u => u.Email == viewModel.Username || u.UserName == viewModel.Username);
            if (user == null) return null; // TODO: Error handling

            // TODO: PasswordVerificationResult.SuccessRehashNeeded?
            if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, viewModel.Password) == PasswordVerificationResult.Success)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var token = new JwtSecurityToken(
                    issuer: configuration["Issuer"],
                    audience: configuration["Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(30),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SigningKey"])), SecurityAlgorithms.HmacSha256));

                return token;
            }
            else
            {
                return null;
            }
        }
    }
}
