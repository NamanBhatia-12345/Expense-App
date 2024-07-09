using ExpenseApp.Core.Models;

namespace ExpenseApp.Application.Services
{
    public interface IAuthService
    {
        Task<bool> UpdateUserAsync(ApplicationUser user);
        string GenerateJwtTokenAsync(ApplicationUser user, string roles);
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<ApplicationUser> AuthenticateUserAsync(string email, string password);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(string roleName = "User");
        Task DeleteUser(string userId);
    }
}
