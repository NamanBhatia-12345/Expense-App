using ExpenseApp.Core.Models;

namespace ExpenseApp.Core.IRepositories
{
    public interface IAuthRepository
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<bool> UpdateUserAsync(ApplicationUser user);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(string roleName);
        Task DeleteUserIfNotInGroupAsync(string userId);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
    }
}
