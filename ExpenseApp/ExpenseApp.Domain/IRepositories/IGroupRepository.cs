

using ExpenseApp.Core.Models;

namespace ExpenseApp.Core.IRepositories
{
    public interface IGroupRepository
    {
        Task<Group> AddGroupAsync(Group group);
        Task AddMemberToGroupAsync(int groupId, GroupMembers member);
        Task<Group> GetGroupByIdAsync(int groupId);
        Task<IEnumerable<Group>> GetAllGroupsAsync();
        Task DeleteGroupAsync(int groupId);
        Task<bool> IsUserInAnyGroupAsync(string userId);
    }
}
