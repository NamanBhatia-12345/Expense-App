using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ExpenseApp.Core.Models;
using ExpenseApp.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ExpenseApp.Core.IRepositories;

namespace ExpenseApp.Tests.Repositories
{
    public class AuthRepositoryTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IGroupRepository> _groupRepositoryMock;
        private readonly AuthRepository _authRepository;

        public AuthRepositoryTests()
        {
            // Mock UserManager<ApplicationUser>
            var store = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            // Mock IGroupRepository
            _groupRepositoryMock = new Mock<IGroupRepository>();

            // Create AuthRepository instance with mocked dependencies
            _authRepository = new AuthRepository(_userManagerMock.Object, _groupRepositoryMock.Object);
        }

        [Fact]
        public async Task CheckPasswordAsync_ReturnsTrue_WhenPasswordIsCorrect()
        {
            // Arrange
            var user = new ApplicationUser { UserName = "testuser" };
            var password = "Test@123";

            _userManagerMock.Setup(um => um.CheckPasswordAsync(user, password))
                            .ReturnsAsync(true);

            // Act
            var result = await _authRepository.CheckPasswordAsync(user, password);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteUserIfNotInGroupAsync_ThrowsKeyNotFoundException_WhenUserIsNull()
        {
            // Arrange
            var userId = "1";

            _groupRepositoryMock.Setup(gr => gr.IsUserInAnyGroupAsync(userId))
                                .ReturnsAsync(false);

            _userManagerMock.Setup(um => um.FindByIdAsync(userId))
                            .ReturnsAsync((ApplicationUser)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await _authRepository.DeleteUserIfNotInGroupAsync(userId);
            });
        }

        [Fact]
        public async Task GetUserByEmailAsync_ReturnsUser_WhenUserExists()
        {
            // Arrange
            var userEmail = "test@example.com";
            var user = new ApplicationUser { UserName = "testuser", Email = userEmail };

            _userManagerMock.Setup(um => um.FindByEmailAsync(userEmail))
                            .ReturnsAsync(user);

            // Act
            var result = await _authRepository.GetUserByEmailAsync(userEmail);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userEmail, result.Email);
        }

        [Fact]
        public async Task UpdateUserAsync_ReturnsTrue_WhenUserIsUpdatedSuccessfully()
        {
            // Arrange
            var user = new ApplicationUser { UserName = "testuser" };

            _userManagerMock.Setup(um => um.UpdateAsync(user))
                            .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authRepository.UpdateUserAsync(user);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetAllUsersAsync_ReturnsUsers_WhenUsersExist()
        {
            // Arrange
            var roleName = "User";
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1", UserName = "user1" },
                new ApplicationUser { Id = "2", UserName = "user2" }
            };

            _userManagerMock.Setup(um => um.GetUsersInRoleAsync(roleName))
                            .ReturnsAsync(users);

            // Act
            var result = await _authRepository.GetAllUsersAsync(roleName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(users.Count, result.Count());
            // Add more assertions as needed based on your repository implementation
        }

        [Fact]
        public async Task DeleteUserIfNotInGroupAsync_DeletesUser_WhenNotInGroup()
        {
            // Arrange
            var userId = "1";
            var user = new ApplicationUser { Id = userId, UserName = "testuser" };

            _groupRepositoryMock.Setup(gr => gr.IsUserInAnyGroupAsync(userId))
                                .ReturnsAsync(false);

            _userManagerMock.Setup(um => um.FindByIdAsync(userId))
                            .ReturnsAsync(user);

            _userManagerMock.Setup(um => um.DeleteAsync(user))
                            .ReturnsAsync(IdentityResult.Success);

            //Act

            await _authRepository.DeleteUserIfNotInGroupAsync(userId);

            //Assert
            _userManagerMock.Verify(um => um.DeleteAsync(user), Times.Once);
        }

        [Fact]
        public async Task DeleteUserIfNotInGroupAsync_ThrowsInvalidOperationException_WhenInGroup()
        {
            // Arrange
            var userId = "1";

            _groupRepositoryMock.Setup(gr => gr.IsUserInAnyGroupAsync(userId))
                                .ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await _authRepository.DeleteUserIfNotInGroupAsync(userId);
            });
        }
    }
}
