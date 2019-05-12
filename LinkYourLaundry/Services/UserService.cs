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
using System.Security.Cryptography;
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

        public LoginResultViewModel Login(LoginViewModel viewModel)
        {
            var user = context.Users.FirstOrDefault(u => u.Email == viewModel.Email);
            if (user == null) return null; // TODO: Error handling

            // TODO: PasswordVerificationResult.SuccessRehashNeeded?
            if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, viewModel.Password) == PasswordVerificationResult.Success)
            {
                return GetAccessToken(user);
            }
            else
            {
                return null;
            }
        }

        public LoginResultViewModel Refresh(RefreshViewModel viewModel)
        {
            var user = context.Users.FirstOrDefault(u => u.RefreshTokens.Any(r => r.Token == viewModel.Token));
            if (user == null) return null; // TODO: Error handling

            var result = GetAccessToken(user);
            if(result != null)
            {
                user.RefreshTokens.Remove(user.RefreshTokens.FirstOrDefault(r => r.Token == viewModel.Token));
                context.SaveChanges();
            }

            return result;
        }

        private LoginResultViewModel GetAccessToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            // Access token lifetime = 1 hour
            var expiresIn = 60 * 60;

            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = GetRefreshToken(128)
            };

            user.RefreshTokens.Add(refreshToken);
            context.SaveChanges();

            var token = new JwtSecurityToken(
                issuer: configuration["Issuer"],
                audience: configuration["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(expiresIn),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SigningKey"])), SecurityAlgorithms.HmacSha256));

            return new LoginResultViewModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = expiresIn,
                RefreshToken = refreshToken.Token
            };
        }

        // Source: https://stackoverflow.com/a/1344365
        private string GetRefreshToken(int stringLength)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bitCount = (stringLength * 6);
                var byteCount = ((bitCount + 7) / 8); // rounded up
                var bytes = new byte[byteCount];
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
