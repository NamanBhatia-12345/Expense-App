using ExpenseApp.Core.IRepositories;
using ExpenseApp.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseApp.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        public AuthService(IAuthRepository authRepository,IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }
        public async Task<ApplicationUser> AuthenticateUserAsync(string email, string password)
        {
            var user = await _authRepository.GetUserByEmailAsync(email);
            if (user == null || !await _authRepository.CheckPasswordAsync(user, password))
            {
                return null;
            }
            return user;
        }

        public string GenerateJwtTokenAsync(ApplicationUser user, string roles)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Role, roles));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim("Name", user.FullName));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(string roleName = "User")
        {
            return await _authRepository.GetAllUsersAsync(roleName);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _authRepository.GetUserByIdAsync(userId);
        }

        public async Task<bool> UpdateUserAsync(ApplicationUser user)
        {
            return await _authRepository.UpdateUserAsync(user); 
        }

        public async Task DeleteUser(string userId)
        {
            await _authRepository.DeleteUserIfNotInGroupAsync(userId);   
        }
    }
}
