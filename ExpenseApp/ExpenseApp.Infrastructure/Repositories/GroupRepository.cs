using ExpenseApp.Core.IRepositories;
using ExpenseApp.Core.Models;
using ExpenseApp.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp.Infrastructure.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ExpenseDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public GroupRepository(ExpenseDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<bool> IsUserInAnyGroupAsync(string userId)
        {
            return await _context.GroupMembers.AnyAsync(gm => gm.UserId == userId);
        }
        public async Task<Group> AddGroupAsync(Group group)
        {
            await _context.Groups.AddAsync(group); 
            await _context.SaveChangesAsync();  
            return group;

        }
        public async Task AddMemberToGroupAsync(int groupId, GroupMembers member)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == member.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            var group = await GetGroupByIdAsync(groupId);
            if (group == null) throw new KeyNotFoundException("Group not found");

            if (group.Members.Count >= 10)
                throw new InvalidOperationException("Group cannot have more than 10 members.");

            if (group.Members.Any(m => m.UserId == user.Id))
                throw new KeyNotFoundException("User is already a member of the group");

            group.Members.Add(member);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Group>> GetAllGroupsAsync()
        {
            return await _context.Groups
            .Include(g => g.Members)
             .ThenInclude(m => m.User)
            .Include(g => g.Expenses)
            .ToListAsync();
        }
        public async Task<Group> GetGroupByIdAsync(int groupId)
        {
            return await _context.Groups
                .Include(g => g.Members)
                .ThenInclude(m => m.User)
                .Include(g => g.Expenses)
                .FirstOrDefaultAsync(g => g.Id == groupId);
        }
        public async Task DeleteGroupAsync(int groupId)
        {
            var group = await _context.Groups
                .Include(g => g.Members)
                .Include(g => g.Expenses)
                .FirstOrDefaultAsync(g => g.Id == groupId);

            if (group == null) throw new KeyNotFoundException("Group not found");
            // Also check that expense exist in the group or not
            if (group.Expenses.Count != 0)
            {
                // Remove all expenses associated with the group
                _context.Expenses.RemoveRange(group.Expenses);
                _context.Groups.Remove(group);
            }
            else
            {
                _context.Groups.Remove(group);
            }
            await _context.SaveChangesAsync();
        }
    }
}
