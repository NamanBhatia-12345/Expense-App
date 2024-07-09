using ExpenseApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp.Application.Services
{
    public interface IGroupService
    {
        Task<Group> GetGroupByIdAsync(int groupId);
        Task<Group> CreateGroupAsync(Group group);
        Task AddMemberToGroupAsync(int groupId, GroupMembers member);
        Task DeleteGroupAsync(int groupId);
        Task<IEnumerable<Group>> GetAllGroupsAsync();
    }
}
