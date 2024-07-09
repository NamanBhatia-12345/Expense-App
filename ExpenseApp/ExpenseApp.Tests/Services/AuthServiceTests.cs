using ExpenseApp.Application.Services;
using ExpenseApp.Core.IRepositories;
using ExpenseApp.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExpenseApp.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IAuthRepository> _authRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly AuthService _authService;  

        public AuthServiceTests()
        {
            _authRepositoryMock = new Mock<IAuthRepository>();
            _configurationMock = new Mock<IConfiguration>();
            _authService = new AuthService(_authRepositoryMock.Object, _configurationMock.Object);
        }

        [Fact]
        public async Task AuthenticateUserAsync_ReturnsUser_WhenCredentialsAreValid()
        {
            // Arrange
            var email = "test@example.com";
            var password = "password";
            var user = new ApplicationUser { Email = email };

            _authRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(email))
                               .ReturnsAsync(user);
            _authRepositoryMock.Setup(repo => repo.CheckPasswordAsync(user, password))
                               .ReturnsAsync(true);

            // Act
            var result = await _authService.AuthenticateUserAsync(email, password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(email, result.Email);
        }
        [Fact]
        public async Task AuthenticateUserAsync_ReturnsNull_WhenUserNotFound()
        {
            // Arrange
            var email = "test@example.com";
            var password = "password";

            _authRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(email))
                               .ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _authService.AuthenticateUserAsync(email, password);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AuthenticateUserAsync_ReturnsNull_WhenPasswordIsIncorrect()
        {
            // Arrange
            var email = "test@example.com";
            var password = "wrongpassword";
            var user = new ApplicationUser { Email = email };

            _authRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(email))
                               .ReturnsAsync(user);
            _authRepositoryMock.Setup(repo => repo.CheckPasswordAsync(user, password))
                               .ReturnsAsync(false);

            // Act
            var result = await _authService.AuthenticateUserAsync(email, password);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GenerateJwtTokenAsync_ReturnsValidToken()
        {
            // Arrange
            var user = new ApplicationUser
            {
                UserName = "testuser",
                Email = "test@example.com",
                FullName = "Test User",
                Id = Guid.NewGuid().ToString() 
            };
            var roles = "User";
            _configurationMock.SetupGet(config => config["Jwt:Key"]).Returns("6AD2EFDE-AB2C-4841-A05E-7045C855BA22");
            _configurationMock.SetupGet(config => config["Jwt:Issuer"]).Returns("https://localhost:7011");
            _configurationMock.SetupGet(config => config["Jwt:Audience"]).Returns("https://localhost:7011");

            // Act
            var result = _authService.GenerateJwtTokenAsync(user, roles);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAllUsersAsync_ReturnsUsers_WhenUsersExist()
        {
                // Arrange
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1", UserName = "user1", Email = "user1@example.com", Role = "User" },
                new ApplicationUser { Id = "2", UserName = "user2", Email = "user2@example.com", Role = "User" }
            };

            _authRepositoryMock.Setup(repo => repo.GetAllUsersAsync("User"))
                               .ReturnsAsync(users);

            // Act
            var result = await _authService.GetAllUsersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(users.Count, result.Count());

            for (int i = 0; i < users.Count; i++)
            {
                var expectedUser = users[i];
                var actualUser = result.ElementAt(i);

                Assert.Equal(expectedUser.Id, actualUser.Id);
                Assert.Equal(expectedUser.UserName, actualUser.UserName);
                Assert.Equal(expectedUser.Email, actualUser.Email);
                Assert.Equal("User", expectedUser.Role); 
                                                         
            }
        }

        [Fact]
        public async Task GetUserByIdAsync_ReturnsUser_WhenUserExists()
        {
            // Arrange
            var userId = "1";
            var user = new ApplicationUser { Id = userId, UserName = "testuser", Email = "test@example.com", Role = "User" };

            _authRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId))
                               .ReturnsAsync(user);

            // Act
            var result = await _authService.GetUserByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
        }

        [Fact]
        public async Task UpdateUserAsync_ReturnsTrue_WhenUserIsUpdated()
        {
            // Arrange
            var user = new ApplicationUser { Id = "1", UserName = "testuser", Email = "test@example.com", Role = "User" };

            _authRepositoryMock.Setup(repo => repo.UpdateUserAsync(user))
                               .ReturnsAsync(true);

            // Act
            var result = await _authService.UpdateUserAsync(user);

            // Assert
            Assert.True(result);
        }
    }
}
