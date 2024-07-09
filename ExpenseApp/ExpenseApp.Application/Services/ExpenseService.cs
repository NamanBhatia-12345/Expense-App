using ExpenseApp.Core.IRepositories;
using ExpenseApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp.Application.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IAuthRepository _authRepository;
        public ExpenseService(IExpenseRepository expenseRepository, IGroupRepository groupRepository, IAuthRepository authRepository)
        {
            _expenseRepository = expenseRepository;
            _groupRepository = groupRepository;
            _authRepository = authRepository;   
        }
        public async Task<Expense> CreateExpenseAsync(Expense expense)
        {
            return await _expenseRepository.CreateExpenseAsync(expense);
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            return await _expenseRepository.GetAllExpenseAsync();
        }

        public async Task<Expense> GetExpenseByIdAsync(int expenseId)
        {
            var expense =  await _expenseRepository.GetExpenseByIdAsync(expenseId);
            if (expense == null) throw new Exception("Expense not Found");
            return expense;
        }

        public async Task<IEnumerable<Expense>> GetExpensesForUserAsync(string userId)
        {
            return await _expenseRepository.GetExpensesForUserAsync(userId);
        }

        public async Task SettleExpenseAsync(int expenseId)
        {

            var expense = await _expenseRepository.GetExpenseByIdAsync(expenseId);
            if (expense == null) throw new Exception("Expense not found.");
            var group = await _groupRepository.GetGroupByIdAsync(expense.GroupId);
            if (group == null) throw new Exception("Group not found.");
            if (group.Members.Count == 0) throw new Exception("No member exists in the group for expense settlement.");
            expense.Issettled = true;
            await _expenseRepository.UpdateExpenseAsync(expenseId,expense);
        }

        public async Task SplitExpenseAsync(int expenseId)
        {
            var expense = await _expenseRepository.GetExpenseByIdAsync(expenseId);
            var group = await _groupRepository.GetGroupByIdAsync(expense.GroupId);
            decimal splitAmount = 0;
            splitAmount += ((expense.Amount) / (group.Members.Count));
            List<ExpenseSplit> expenseSplits = new List<ExpenseSplit>();
            foreach (var member in group.Members)
            {
                var user = await _authRepository.GetUserByIdAsync(member.UserId);
                string fullName = user.FullName;
                var split = new ExpenseSplit
                {
                    ExpenseId = expenseId,
                    UserId = member.UserId,
                    FullName = fullName,
                    Balance = splitAmount
                };
                expenseSplits.Add(split);
            }
            expense.Splits = expenseSplits; 
            await _expenseRepository.UpdateExpenseAsync(expenseId, expense);
        }

        public async Task UpdateExpenseAsync(int id, Expense expense)
        {
            await _expenseRepository.UpdateExpenseAsync(id, expense);
        }
    }
}
