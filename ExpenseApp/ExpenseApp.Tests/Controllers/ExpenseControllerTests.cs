using ExpenseApp.API.Controllers;
using ExpenseApp.Application.DTOs;
using ExpenseApp.Application.Services;
using ExpenseApp.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExpenseApp.Tests.Controllers
{
    public class ExpenseControllerTests
    {
        private readonly Mock<IExpenseService> _expenseServiceMock;
        private readonly Mock<IGroupService> _groupServiceMock;
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly ExpenseController _controller;

        public ExpenseControllerTests()
        {
            _expenseServiceMock = new Mock<IExpenseService>();
            _groupServiceMock = new Mock<IGroupService>();
            _authServiceMock = new Mock<IAuthService>();
            _controller = new ExpenseController(_expenseServiceMock.Object, _groupServiceMock.Object, _authServiceMock.Object);
        }

        [Fact]
        public async Task GetExpenseById_ReturnsExpense_WhenExpenseExists()
        {
            // Arrange
            int expenseId = 1;
            var expense = new Expense { Id = expenseId, Description = "Test Expense" };
            _expenseServiceMock.Setup(service => service.GetExpenseByIdAsync(expenseId)).ReturnsAsync(expense);

            // Act
            var result = await _controller.GetExpenseById(expenseId);

            // Assert
            var response = Assert.IsType<ResponseDTO>(result);
            Assert.True(response.IsSuccess);
            Assert.Equal("Success!", response.Message);
            var expenseDTO = Assert.IsType<ExpenseDTO>(response.Result);
            Assert.Equal(expense.Id, expenseDTO.Id);
            Assert.Equal(expense.Description, expenseDTO.Description);
        }

        [Fact]
        public async Task GetExpenseById_ReturnsNotFound_WhenExpenseDoesNotExist()
        {
            // Arrange
            int expenseId = 1;
            _expenseServiceMock.Setup(service => service.GetExpenseByIdAsync(expenseId)).ReturnsAsync((Expense)null);

            // Act
            var result = await _controller.GetExpenseById(expenseId);

            // Assert
            var response = Assert.IsType<ResponseDTO>(result);
            Assert.False(response.IsSuccess);
            Assert.Equal("Expense not found!", response.Message);
            Assert.Null(response.Result);
        }

        [Fact]
        public async Task GetAllExpenses_ReturnsAllExpenses()
        {
            // Arrange
            var expenses = new List<Expense>
            {
                new Expense { Id = 1, Description = "Expense 1" },
                new Expense { Id = 2, Description = "Expense 2" }
            };
            _expenseServiceMock.Setup(service => service.GetAllExpensesAsync()).ReturnsAsync(expenses);

            // Act
            var result = await _controller.GetAllExpenses();

            // Assert
            var response = Assert.IsType<ResponseDTO>(result);
            Assert.True(response.IsSuccess);
            Assert.Equal("All expenses retrieved successfully.", response.Message);
            var expenseList = Assert.IsType<List<ExpenseDTO>>(response.Result);
            Assert.Equal(expenses.Count, expenseList.Count);
        }

        [Fact]
        public async Task CreateExpense_CreatesExpense_WhenGroupExistsAndUserIsMember()
        {
            // Arrange
            var createExpenseDTO = new CreateExpenseDTO
            {
                Description = "New Expense",
                Amount = 100,
                Date = DateTime.UtcNow,
                PaidUserBy = "User123"
            };
            var userId = "User123";
            var groupId = 1;
            var group = new Group { Id = groupId, Members = new List<GroupMembers> { new GroupMembers { UserId = userId } } };
            var createdExpense = new Expense { Id = 1, Description = "New Expense" };

            _groupServiceMock.Setup(service => service.GetGroupByIdAsync(groupId)).ReturnsAsync(group);
            _expenseServiceMock.Setup(service => service.CreateExpenseAsync(It.IsAny<Expense>())).ReturnsAsync(createdExpense);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, userId) })) }
            };

            // Act
            var result = await _controller.CreateExpense(groupId, createExpenseDTO);

            // Assert
            var response = Assert.IsType<ResponseDTO>(result);
            Assert.True(response.IsSuccess);
            Assert.Equal("Expense created successfully.", response.Message);
            var expenseDTO = Assert.IsType<ExpenseDTO>(response.Result);
            Assert.Equal(createdExpense.Id, expenseDTO.Id);
            Assert.Equal(createExpenseDTO.Description, expenseDTO.Description);
        }

        [Fact]
        public async Task CreateExpense_ReturnsGroupNotFound_WhenGroupDoesNotExist()
        {
            // Arrange
            var createExpenseDTO = new CreateExpenseDTO();
            var userId = "User123";
            var groupId = 1;

            _groupServiceMock.Setup(service => service.GetGroupByIdAsync(groupId)).ReturnsAsync((Group)null);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, userId) })) }
            };
            // Act
            var result = await _controller.CreateExpense(groupId, createExpenseDTO);

            // Assert
            var response = Assert.IsType<ResponseDTO>(result);
            Assert.False(response.IsSuccess);
            Assert.Equal("Group not found.", response.Message);
            Assert.Null(response.Result);
        }

        [Fact]
        public async Task CreateExpense_ReturnsNotMemberOfGroup_WhenUserIsNotMemberOfGroup()
        {
            // Arrange
            var createExpenseDTO = new CreateExpenseDTO();
            var userId = "User123";
            var groupMemberUserId = "User456";
            var groupId = 1;
            var group = new Group { Id = groupId,
                Members = new List<GroupMembers>
                {
                    new GroupMembers { UserId = groupMemberUserId }
                }
            };

            _groupServiceMock.Setup(service => service.GetGroupByIdAsync(groupId)).ReturnsAsync(group);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userId)
                    }, "mock"))
                }
            };

            // Act
            var result = await _controller.CreateExpense(groupId, createExpenseDTO);

            // Assert
            var response = Assert.IsType<ResponseDTO>(result);
            Assert.False(response.IsSuccess);
            Assert.Equal("Only group members can create expenses.", response.Message);
            Assert.Null(response.Result);
        }

        [Fact]
        public async Task UpdateExpense_UpdatesExpense_WhenExpenseExists()
        {
            // Arrange
            int expenseId = 1;
            var updateExpenseDTO = new UpdateExpenseDTO { Description = "Updated Expense" };
            var expense = new Expense { Id = expenseId, Description = "Original Expense" };

            _expenseServiceMock.Setup(service => service.GetExpenseByIdAsync(expenseId)).ReturnsAsync(expense);
            _expenseServiceMock.Setup(service => service.UpdateExpenseAsync(expenseId, It.IsAny<Expense>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateExpense(expenseId, updateExpenseDTO);

            // Assert
            var response = Assert.IsType<ResponseDTO>(result);
            Assert.True(response.IsSuccess);
            Assert.Equal($"Expense with Id {expenseId} updated successfully.", response.Message);
            Assert.Null(response.Result); // No specific result expected for update
        }

        [Fact]
        public async Task UpdateExpense_ReturnsExpenseNotFound_WhenExpenseDoesNotExist()
        {
            // Arrange
            int expenseId = 1;
            var updateExpenseDTO = new UpdateExpenseDTO();

            _expenseServiceMock.Setup(service => service.GetExpenseByIdAsync(expenseId)).ReturnsAsync((Expense)null);

            // Act
            var result = await _controller.UpdateExpense(expenseId, updateExpenseDTO);

            // Assert
            var response = Assert.IsType<ResponseDTO>(result);
            Assert.False(response.IsSuccess);
            Assert.Equal("Expense not found", response.Message);
            Assert.Null(response.Result);
        }

        [Fact]
        public async Task SettleExpense_SettlesExpense_WhenExpenseExists()
        {
            // Arrange
            int expenseId = 1;
            var expense = new Expense
            {
                Id = expenseId,
                Description = "Test Expense",
                Amount = 100,
                Date = DateTime.Now,
                PaidUserBy = "User123",
                Issettled = true, // Ensure the initial state of the expense
                GroupId = 1,
                Splits = new List<ExpenseSplit> // Mocking splits for the expense
                {
                    new ExpenseSplit { ExpenseId = 1, UserId = "User456", FullName = "User456 Name", Balance = 50 },
                    new ExpenseSplit { ExpenseId = 1, UserId = "User789", FullName = "User789 Name", Balance = 50 }
                }
            };

            _expenseServiceMock.Setup(service => service.GetExpenseByIdAsync(expenseId)).ReturnsAsync(expense);
            _expenseServiceMock.Setup(service => service.SettleExpenseAsync(expenseId)).Returns(Task.CompletedTask);
            _expenseServiceMock.Setup(service => service.SplitExpenseAsync(expenseId)).Returns(Task.CompletedTask);
            // Act
            var result = await _controller.SettleExpense(expenseId);

            // Assert
            var response = Assert.IsType<ResponseDTO>(result);
            Assert.True(response.IsSuccess);
            Assert.Equal("Expense settled successfully.", response.Message);
            var expenseDTO = Assert.IsType<SplitsExpenseDTO>(response.Result);
            Assert.Equal(expense.Id, expenseDTO.Id);
            Assert.True(expense.Issettled);
        }

        [Fact]
        public async Task SettleExpense_ReturnsExpenseNotFound_WhenExpenseDoesNotExist()
        {
            // Arrange
            int expenseId = 1;
            _expenseServiceMock.Setup(service => service.GetExpenseByIdAsync(expenseId)).ReturnsAsync((Expense)null);

            // Act
            var result = await _controller.SettleExpense(expenseId);

            // Assert
            var response = Assert.IsType<ResponseDTO>(result);
            Assert.False(response.IsSuccess);
            Assert.Equal("Expense not found", response.Message);
            Assert.Null(response.Result);
        }

        [Fact]
        public async Task GetExpensesForLoggedInUser_ReturnsUserExpenses()
        {
            // Arrange
            var userId = "User123";
            var expenses = new List<Expense>
            {
                new Expense { Id = 1, Description = "Expense 1", GroupId = 1, PaidUserBy = "User 123" },
                new Expense { Id = 2, Description = "Expense 2", GroupId = 2, PaidUserBy = "User 123" }

            };

            _expenseServiceMock.Setup(service => service.GetExpensesForUserAsync(userId)).ReturnsAsync(expenses);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                   {
                        new Claim(ClaimTypes.NameIdentifier, userId)
                   }, "mock"))
                }
            };

            // Act
            var result = await _controller.GetExpensesForLoggedInUser();

            // Assert
            var response = Assert.IsType<ResponseDTO>(result);
            Assert.True(response.IsSuccess);
            Assert.Equal("Expenses retrieved successfully.", response.Message);
            var expenseList = Assert.IsType<List<SplitsExpenseDTO>>(response.Result);
            Assert.Equal(expenses.Count, expenseList.Count);
        }

    }
}
