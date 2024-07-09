using Xunit;
using Moq;
using ExpenseApp.API.Controllers;
using ExpenseApp.Application.Services;
using ExpenseApp.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ExpenseApp.Core.Models;

namespace ExpenseApp.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly AuthController _authController;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _authController = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public async Task Login_ReturnsSuccess_WhenCredentialsAreValid()
        {
            // Arrange
            var loginRequest = new LoginRequestDTO { Email = "test@example.com", Password = "password" };
            var user = new ApplicationUser { Email = "test@example.com", Role = "User" };
            var token = "fake-jwt-token";
            _authServiceMock.Setup(s => s.AuthenticateUserAsync(loginRequest.Email, loginRequest.Password))
                            .ReturnsAsync(user);
            _authServiceMock.Setup(s => s.GenerateJwtTokenAsync(user, user.Role))
                            .Returns(token);

            // Act
            var result = await _authController.Login(loginRequest);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Login Successfully!", result.Message);
            Assert.Equal(token, result.Result);
        }

        [Fact]
        public async Task Login_ReturnsInvalidCredentials_WhenCredentialsAreInvalid()
        {
            // Arrange
            var loginRequest = new LoginRequestDTO { Email = "test@example.com", Password = "wrongpassword" };
            _authServiceMock.Setup(s => s.AuthenticateUserAsync(loginRequest.Email, loginRequest.Password))
                            .ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _authController.Login(loginRequest);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid Credentials.", result.Message);
            Assert.Null(result.Result);
        }

        [Fact]
        public async Task GetUserDetails_ReturnsUser_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var user = new ApplicationUser { Id = userId, FullName = "Test User", Email = "test@example.com", UserName = "testuser", PhoneNumber = "1234567890", Role = "User" };
            _authServiceMock.Setup(s => s.GetUserByIdAsync(userId)).ReturnsAsync(user);

            // Act
            var result = await _authController.GetUserDetails(userId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Success", result.Message);
            Assert.NotNull(result.Result);
        }

        [Fact]
        public async Task GetUserDetails_ReturnsUserNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            _authServiceMock.Setup(s => s.GetUserByIdAsync(userId)).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _authController.GetUserDetails(userId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("User not Found!", result.Message);
            Assert.Null(result.Result);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsUsersList_WhenUsersExist()
        {
            // Arrange
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1", FullName = "User One", Email = "user1@example.com", UserName = "userone", PhoneNumber = "1234567890", Role = "User" },
                new ApplicationUser { Id = "2", FullName = "User Two", Email = "user2@example.com", UserName = "usertwo", PhoneNumber = "0987654321", Role = "User" }
            };
            _authServiceMock.Setup(s => s.GetAllUsersAsync("User")).ReturnsAsync(users);

            // Act
            var result = await _authController.GetAllUsers();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Success", result.Message);
            Assert.NotNull(result.Result);
            var userList = Assert.IsType<List<UserDTO>>(result.Result);
            Assert.All(userList, u => Assert.Equal("User", users.Find(x => x.Id == u.UserId).Role));
        }

        [Fact]
        public async Task GetAllUsers_ReturnsEmptyList_WhenNoUsersExist()
        {
            // Arrange
            var users = new List<ApplicationUser>();
            _authServiceMock.Setup(s => s.GetAllUsersAsync("User")).ReturnsAsync(users);

            // Act
            var result = await _authController.GetAllUsers();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Success", result.Message);
            Assert.Empty((List<UserDTO>)result.Result);
        }

        [Fact]
        public async Task UpdateUser_ReturnsSuccess_WhenUserIsUpdated()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var updateUser = new UpdateUserDTO { FullName = "Updated User", Email = "updated@example.com", PhoneNumber = "1234567890" };
            var user = new ApplicationUser { Id = userId, FullName = "Old User", Email = "old@example.com", UserName = "olduser", PhoneNumber = "0987654321", Role = "User" };
            _authServiceMock.Setup(s => s.GetUserByIdAsync(userId)).ReturnsAsync(user);
            _authServiceMock.Setup(s => s.UpdateUserAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(true);
            _authServiceMock.Setup(s => s.GetUserByIdAsync(userId)).ReturnsAsync(user);

            // Act
            var result = await _authController.UpdateUser(userId, updateUser);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("User details updated successfully!", result.Message);
            Assert.NotNull(result.Result);
        }

        [Fact]
        public async Task UpdateUser_ReturnsUserNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var updateUser = new UpdateUserDTO { FullName = "Updated User", Email = "updated@example.com", PhoneNumber = "1234567890" };
            _authServiceMock.Setup(s => s.GetUserByIdAsync(userId)).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _authController.UpdateUser(userId, updateUser);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("User not Found!", result.Message);
            Assert.Null(result.Result);
        }

        [Fact]
        public async Task Delete_ReturnsSuccess_WhenUserIsDeleted()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var user = new ApplicationUser { Id = userId, FullName = "User To Delete", Email = "delete@example.com", UserName = "usertodelete", PhoneNumber = "1234567890" };
            _authServiceMock.Setup(s => s.GetUserByIdAsync(userId)).ReturnsAsync(user);
            _authServiceMock.Setup(s => s.DeleteUser(userId)).Returns(Task.CompletedTask);

            // Act
            var result = await _authController.Delete(userId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("User deleted successfully.", result.Message);
            Assert.NotNull(result.Result);
        }

        [Fact]
        public async Task Delete_ReturnsUserNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            _authServiceMock.Setup(s => s.GetUserByIdAsync(userId)).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _authController.Delete(userId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("User not Found!", result.Message);
            Assert.Null(result.Result);
        }
    }
}
