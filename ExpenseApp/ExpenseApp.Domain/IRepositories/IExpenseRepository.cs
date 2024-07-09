using ExpenseApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp.Core.IRepositories
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetAllExpenseAsync();
        Task<Expense> GetExpenseByIdAsync(int expenseId);
        Task<IEnumerable<Expense>> GetExpensesForUserAsync(string userId);
        Task<Expense> CreateExpenseAsync(Expense expense);
        Task UpdateExpenseAsync(int id,Expense expense);
    }
}
