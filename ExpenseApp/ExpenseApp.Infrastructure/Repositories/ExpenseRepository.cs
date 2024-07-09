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
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ExpenseDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ExpenseRepository(ExpenseDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<Expense> CreateExpenseAsync(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            return expense;
        }

        public async Task<IEnumerable<Expense>> GetAllExpenseAsync()
        {
            return await _context.Expenses
            .Include(e => e.Splits)
            .ToListAsync();
        }

        public async Task<Expense> GetExpenseByIdAsync(int expenseId)
        {
            return await _context.Expenses
            .Include(e => e.Splits)
            .FirstOrDefaultAsync(e => e.Id == expenseId);
        }


        public async Task UpdateExpenseAsync(int id, Expense expense)
        {
            var model = await _context.Expenses.FindAsync(id);
            if (model != null)
            {
                _context.Expenses.Update(expense);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Expense>> GetExpensesForUserAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            return await _context.Expenses
            .Include(e => e.Splits)
            .Where(e => e.PaidUserBy == user.FullName || e.Splits.Any(s => s.UserId == userId))
            .ToListAsync();
        }
    }
}
