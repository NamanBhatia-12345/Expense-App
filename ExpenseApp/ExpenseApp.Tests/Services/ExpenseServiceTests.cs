using ExpenseApp.Application.Services;
using ExpenseApp.Core.IRepositories;
using ExpenseApp.Core.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExpenseApp.Tests.Services
{
    public class ExpenseServiceTests
    {
        private readonly Mock<IExpenseRepository> _expenseRepositoryMock;
        private readonly Mock<IGroupRepository> _groupRepositoryMock;
        private readonly Mock<IAuthRepository> _authRepositoryMock;
        private readonly ExpenseService _expenseService;
        public ExpenseServiceTests()
        {
            _expenseRepositoryMock = new Mock<IExpenseRepository>();
            _groupRepositoryMock = new Mock<IGroupRepository>();
            _authRepositoryMock = new Mock<IAuthRepository>();
            _expenseService = new ExpenseService(_expenseRepositoryMock.Object, _groupRepositoryMock.Object, _authRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateExpenseAsync_ReturnsCreatedExpense()
        {
            // Arrange
            var expense = new Expense { Id = 1, Description = "Test Expense", Amount = 100 };
            _expenseRepositoryMock.Setup(repo => repo.CreateExpenseAsync(expense)).ReturnsAsync(expense);

            // Act
            var result = await _expenseService.CreateExpenseAsync(expense);

            // Assert
            Assert.Equal(expense, result);
            _expenseRepositoryMock.Verify(repo => repo.CreateExpenseAsync(expense), Times.Once);
        }

        [Fact]
        public async Task GetAllExpensesAsync_ReturnsAllExpenses()
        {
            // Arrange
            var expenses = new List<Expense> { new Expense { Id = 1, Description = "Test Expense 1", Amount = 100 } };
            _expenseRepositoryMock.Setup(repo => repo.GetAllExpenseAsync()).ReturnsAsync(expenses);

            // Act
            var result = await _expenseService.GetAllExpensesAsync();

            // Assert
            Assert.Equal(expenses, result);
            _expenseRepositoryMock.Verify(repo => repo.GetAllExpenseAsync(), Times.Once);
        }

        [Fact]
        public async Task GetExpenseByIdAsync_ReturnsExpense_WhenExpenseExists()
        {
            // Arrange
            var expense = new Expense { Id = 1, Description = "Test Expense", Amount = 100 };
            _expenseRepositoryMock.Setup(repo => repo.GetExpenseByIdAsync(1)).ReturnsAsync(expense);

            // Act
            var result = await _expenseService.GetExpenseByIdAsync(1);

            // Assert
            Assert.Equal(expense, result);
            _expenseRepositoryMock.Verify(repo => repo.GetExpenseByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetExpenseByIdAsync_ThrowsException_WhenExpenseDoesNotExist()
        {
            // Arrange
            _expenseRepositoryMock.Setup(repo => repo.GetExpenseByIdAsync(1)).ReturnsAsync((Expense)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _expenseService.GetExpenseByIdAsync(1));
        }

        [Fact]
        public async Task SettleExpenseAsync_SettlesExpense_WhenExpenseExists()
        {
            // Arrange
            var expense = new Expense { Id = 1, GroupId = 1, Issettled = false };
            var group = new Group { Id = 1, Members = new List<GroupMembers> { new GroupMembers { UserId = "User123" } } };

            _expenseRepositoryMock.Setup(repo => repo.GetExpenseByIdAsync(1)).ReturnsAsync(expense);
            _groupRepositoryMock.Setup(repo => repo.GetGroupByIdAsync(1)).ReturnsAsync(group);

            // Act
            await _expenseService.SettleExpenseAsync(1);

            // Assert
            Assert.True(expense.Issettled);
            _expenseRepositoryMock.Verify(repo => repo.UpdateExpenseAsync(1, expense), Times.Once);
        }

        [Fact]
        public async Task SettleExpenseAsync_ThrowsException_WhenExpenseDoesNotExist()
        {
            // Arrange
            _expenseRepositoryMock.Setup(repo => repo.GetExpenseByIdAsync(1)).ReturnsAsync((Expense)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _expenseService.SettleExpenseAsync(1));
            Assert.Equal("Expense not found.", exception.Message);
        }

        [Fact]
        public async Task SettleExpenseAsync_ThrowsException_WhenGroupDoesNotExist()
        {
            // Arrange
            var expense = new Expense { Id = 1, GroupId = 1 };
            _expenseRepositoryMock.Setup(repo => repo.GetExpenseByIdAsync(1)).ReturnsAsync(expense);
            _groupRepositoryMock.Setup(repo => repo.GetGroupByIdAsync(1)).ReturnsAsync((Group)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _expenseService.SettleExpenseAsync(1));
            Assert.Equal("Group not found.", exception.Message);
        }

        [Fact]
        public async Task SettleExpenseAsync_ThrowsException_WhenGroupHasNoMembers()
        {
            // Arrange
            var expense = new Expense { Id = 1, GroupId = 1, Issettled = false };
            var group = new Group { Id = 1, Members = new List<GroupMembers>() };

            _expenseRepositoryMock.Setup(repo => repo.GetExpenseByIdAsync(1)).ReturnsAsync(expense);
            _groupRepositoryMock.Setup(repo => repo.GetGroupByIdAsync(1)).ReturnsAsync(group);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _expenseService.SettleExpenseAsync(1));
        }


        [Fact]
        public async Task SplitExpenseAsync_SplitsExpenseCorrectly()
        {
            // Arrange
            var expense = new Expense { Id = 1, Amount = 100, GroupId = 1 };
            var group = new Group
            {
                Id = 1,
                Members = new List<GroupMembers>
                {
                    new GroupMembers { UserId = "User123" },
                    new GroupMembers { UserId = "User456" }
                }
            };
            var user1 = new ApplicationUser { Id = "User123", FullName = "User 123" };
            var user2 = new ApplicationUser { Id = "User456", FullName = "User 456" };

            _expenseRepositoryMock.Setup(repo => repo.GetExpenseByIdAsync(1)).ReturnsAsync(expense);
            _groupRepositoryMock.Setup(repo => repo.GetGroupByIdAsync(1)).ReturnsAsync(group);
            _authRepositoryMock.Setup(repo => repo.GetUserByIdAsync("User123")).ReturnsAsync(user1);
            _authRepositoryMock.Setup(repo => repo.GetUserByIdAsync("User456")).ReturnsAsync(user2);

            // Act
            await _expenseService.SplitExpenseAsync(1);

            // Assert
            Assert.NotNull(expense.Splits);
            Assert.Equal(2, expense.Splits.Count);

            Assert.Collection(expense.Splits,
                split =>
                {
                    Assert.Equal(50, split.Balance);
                    Assert.Equal("User123", split.UserId);
                    Assert.Equal("User 123", split.FullName);
                },
                split =>
                {
                    Assert.Equal(50, split.Balance);
                    Assert.Equal("User456", split.UserId);
                    Assert.Equal("User 456", split.FullName);
                }
            );

            _expenseRepositoryMock.Verify(repo => repo.UpdateExpenseAsync(1, expense), Times.Once);
        }

    }
}
