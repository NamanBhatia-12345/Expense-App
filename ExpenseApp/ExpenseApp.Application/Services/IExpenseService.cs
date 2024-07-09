using ExpenseApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp.Application.Services
{
    public interface IExpenseService
    {
        Task<Expense> CreateExpenseAsync(Expense expense);
        Task<Expense> GetExpenseByIdAsync(int expenseId);
        Task<IEnumerable<Expense>> GetAllExpensesAsync();
        Task<IEnumerable<Expense>> GetExpensesForUserAsync(string userId);
        Task UpdateExpenseAsync(int id, Expense expense);
        Task SplitExpenseAsync(int expenseId);
        Task SettleExpenseAsync(int expenseId);
    }
}
