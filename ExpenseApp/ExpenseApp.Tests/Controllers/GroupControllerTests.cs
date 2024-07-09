using ExpenseApp.API.Controllers;
using ExpenseApp.Application.DTOs;
using ExpenseApp.Application.Services;
using ExpenseApp.Core.IRepositories;
using ExpenseApp.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq.Language;
using Moq.Language.Flow;
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
    public class GroupControllerTests
    {
        private readonly Mock<IGroupService> _groupServiceMock; 
        private readonly Mock<IGroupRepository> _groupRepositoryMock = new Mock<IGroupRepository>();
        private readonly GroupController _controller;
        public GroupControllerTests() 
        {
            _groupServiceMock = new Mock<IGroupService>();
            _controller = new GroupController(_groupServiceMock.Object);
        }

        [Fact]
        public async Task GetAllGroups_ReturnsGroups_WhenGroupsExist()
        {
            //Arrange
            var groups = new List<Group>
            {
                new Group { Id = 1, Name = "Group 1", CreatedDate = DateTime.UtcNow, Description = "Description 1", Members = new List<GroupMembers>(), Expenses = new List<Expense>() },
                new Group { Id = 2, Name = "Group 2", CreatedDate = DateTime.UtcNow, Description = "Description 2", Members = new List<GroupMembers>(), Expenses = new List<Expense>() }
            };
            _groupServiceMock.Setup(service => service.GetAllGroupsAsync())
                             .ReturnsAsync(groups);

            // Act
            var result = await _controller.GetAllGroups();

            // Assert
            var response = Assert.IsType<ResponseDTO>(result);
            Assert.True(response.IsSuccess);
            Assert.Equal("Success", response.Message);
            Assert.NotNull(response.Result);
            var groupDtos = Assert.IsAssignableFrom<List<GroupDTO>>(response.Result);
            Assert.Equal(groups.Count, groupDtos.Count);
        }

        [Fact]
        public async Task GetGroupById_ReturnsGroupNotFound_WhenGroupDoesNotExist()
        {
            // Arrange
            var groupId = 1;
            _groupServiceMock.Setup(service => service.GetGroupByIdAsync(groupId))
                             .ReturnsAsync((Group)null);

            // Act
            var result = await _controller.GetGroupById(groupId);

            // Assert
            var response = Assert.IsType<ResponseDTO>(result);
            Assert.False(response.IsSuccess);
            Assert.Equal("Group not found", response.Message);
            Assert.Null(response.Result);
        }

        [Fact]
        public async Task CreateGroup_ReturnsCreatedGroup()
        {
            // Arrange
            var createGroupDto = new CreateGroupDTO { Name = "New Group", Description = "Test Group" };
            var group = new Group { Id = 1, Name = createGroupDto.Name, Description = createGroupDto.Description };
            _groupServiceMock.Setup(service => service.CreateGroupAsync(It.IsAny<Group>()))
                             .ReturnsAsync(group);

            // Act
            var result = await _controller.CreateGroup(createGroupDto);

            // Assert
            var response = Assert.IsType<ResponseDTO>(result);
            Assert.True(response.IsSuccess);
            Assert.Equal("Group is created successfully!", response.Message);
            Assert.NotNull(response.Result);

            var groupDto = Assert.IsType<GroupDTO>(response.Result);
            Assert.Equal(group.Id, groupDto.Id);
            Assert.Equal(group.Name, groupDto.Name);
        }

        [Fact]
        public async Task AddMemberToGroup_AuthenticatedUser_Success()
        {
            // Arrange
            var userId = "testUserId";
            var groupId = 1;

            var groupMember = new GroupMembers
            {
                GroupId = groupId,
                UserId = userId
            };
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
            _groupServiceMock.Setup(service => service.AddMemberToGroupAsync(groupId,groupMember));
            _groupServiceMock.Setup(service => service.GetGroupByIdAsync(groupId))
                             .ReturnsAsync(new Group
                             {
                                 Id = groupId,
                                 Name = "Test Group",
                                 Description = "Description",
                                 CreatedDate = DateTime.UtcNow,
                                 Members = new List<GroupMembers>(),
                                 Expenses = new List<Expense>()
                             }); // Mock the service method call

            // Act
            var result = await _controller.AddMemberToGroup(groupId);

            // Assert
            var response = Assert.IsType<ResponseDTO>(result);
            Assert.True(response.IsSuccess);
            Assert.Equal("Member added successfully!", response.Message);
            Assert.NotNull(response.Result);

            var groupDto = Assert.IsType<GroupDTO>(response.Result);
            Assert.Equal(groupId, groupDto.Id);
            // Additional assertions for groupDto properties if needed
        }


        [Fact]
        public async Task AddMemberToGroup_ReturnsUnauthorized_WhenUserIsNotAuthenticated()
        {
            // Arrange
            var groupId = 1;
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var result = await _controller.AddMemberToGroup(groupId);

            // Assert
            var response = Assert.IsType<ResponseDTO>(result);
            Assert.False(response.IsSuccess);
            Assert.Equal("Unauthorized access", response.Message);
            Assert.Null(response.Result);
        }

        [Fact]
        public async Task DeleteGroup_ReturnsSuccessMessage_OnSuccessfulDeletion()
        {
            // Arrange
            var groupId = 1;
            _groupServiceMock.Setup(service => service.DeleteGroupAsync(groupId))
                             .Returns(Task.CompletedTask); 

            // Act
            var result = await _controller.DeleteGroup(groupId);

            // Assert
            var response = Assert.IsType<ResponseDTO>(result);
            Assert.True(response.IsSuccess);
            Assert.Contains("deleted", response.Message);                                
        }
    }
}
