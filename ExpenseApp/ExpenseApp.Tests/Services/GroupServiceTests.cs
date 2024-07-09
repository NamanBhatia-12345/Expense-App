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
    public class GroupServiceTests
    {
        private readonly Mock<IGroupRepository> _groupRepositoryMock;
        private readonly GroupService _groupService;
        public GroupServiceTests()
        {
            _groupRepositoryMock = new Mock<IGroupRepository>();
            _groupService = new GroupService(_groupRepositoryMock.Object);
        }
        [Fact]
        public async Task GetAllGroupsAsync_ReturnsGroups()
        {
            // Arrange
            var expectedGroups = new List<Group>
            {
                new Group { Id = 1, Name = "Group 1" },
                new Group { Id = 2, Name = "Group 2" }
            };

            _groupRepositoryMock.Setup(repo => repo.GetAllGroupsAsync())
                                .ReturnsAsync(expectedGroups);

            // Act
            var result = await _groupService.GetAllGroupsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedGroups.Count, ((List<Group>)result).Count);
        }
        [Fact]
        public async Task GetGroupByIdAsync_ReturnsGroup_WhenGroupExists()
        {
            // Arrange
            var groupId = 1;
            var expectedGroup = new Group { Id = groupId, Name = "Test Group" };

            _groupRepositoryMock.Setup(repo => repo.GetGroupByIdAsync(groupId))
                                .ReturnsAsync(expectedGroup);

            // Act
            var result = await _groupService.GetGroupByIdAsync(groupId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedGroup.Id, result.Id);
            Assert.Equal(expectedGroup.Name, result.Name);
        }

        [Fact]
        public async Task CreateGroupAsync_ReturnsCreatedGroup()
        {
            // Arrange
            var groupToCreate = new Group { Name = "New Group", Description = "Test Group" };

            _groupRepositoryMock.Setup(repo => repo.AddGroupAsync(groupToCreate))
                                .ReturnsAsync(groupToCreate);

            // Act
            var result = await _groupService.CreateGroupAsync(groupToCreate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(groupToCreate.Name, result.Name);
            Assert.Equal(groupToCreate.Description, result.Description);
        }

        [Fact]
        public async Task DeleteGroupAsync_DeletesGroup()
        {
            // Arrange
            var groupIdToDelete = 1;

            // Act
            await _groupService.DeleteGroupAsync(groupIdToDelete);

            // Assert
            _groupRepositoryMock.Verify(repo => repo.DeleteGroupAsync(groupIdToDelete), Times.Once);
        }

        [Fact]
        public async Task AddMemberToGroupAsync_AddsMemberToGroup()
        {
            // Arrange
            var groupId = 1;
            var memberToAdd = new GroupMembers { GroupId = groupId, UserId = "user1" };

            // Act
            await _groupService.AddMemberToGroupAsync(groupId, memberToAdd);

            // Assert
            _groupRepositoryMock.Verify(repo => repo.AddMemberToGroupAsync(groupId, memberToAdd), Times.Once);
        }

    }
}
