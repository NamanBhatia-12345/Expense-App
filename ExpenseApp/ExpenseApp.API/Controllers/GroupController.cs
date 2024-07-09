using ExpenseApp.Application.DTOs;
using ExpenseApp.Application.Services;
using ExpenseApp.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private ResponseDTO response;
        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
            response = new ResponseDTO();
        }

        [HttpGet("Get-All-Groups")]
        public async Task<ResponseDTO> GetAllGroups()
        {
            try
            {
                var groups = await _groupService.GetAllGroupsAsync();
                var groupDtos = groups.Select(group => new GroupDTO
                {
                    Id = group.Id,
                    Name = group.Name,
                    Description = group.Description,
                    CreatedDate = group.CreatedDate,
                    Members = group.Members.Select(m => new GroupMemberDTO
                    {
                        UserId = m.UserId,
                        FullName = m.User.FullName,
                        Email = m.User.Email
                    }).ToList(),
                    Expenses = group.Expenses.Select(e => new ExpenseDTO
                    {
                        Id = e.Id,
                        Description = e.Description,
                        Amount = e.Amount,
                        Date = e.Date,
                        PaidUserBy = e.PaidUserBy,
                        GroupId = e.GroupId,    
                        IsSettled = e.Issettled
                    }).ToList()
                }).ToList();
                response.Result = groupDtos;
                response.IsSuccess = true;
                response.Message = "Success";
            }
            catch (Exception ex) 
            {
                response.IsSuccess = false;
                response.Message = ex.Message.ToString();

            }
            return response;
        }

        [HttpGet("{groupId:int}")]
        public async Task<ResponseDTO> GetGroupById(int groupId)
        {
            try
            {
                var group = await _groupService.GetGroupByIdAsync(groupId);
                if (group == null)
                {
                    response.Result = null;
                    response.IsSuccess = false;
                    response.Message = "Group not found";
                    return response;
                }
                var groupDto = new GroupDTO()
                {
                    Id = groupId,
                    Name = group.Name,
                    Description = group.Description,
                    CreatedDate = group.CreatedDate,
                    Members = group.Members.Select(m => new GroupMemberDTO
                    {
                        UserId = m.UserId,
                        FullName = m.User.FullName,
                        Email = m.User.Email,
                    }).ToList(),
                    Expenses = group.Expenses.Select(e => new ExpenseDTO
                    {
                        Id = e.Id,
                        Description = e.Description,
                        Amount = e.Amount,
                        Date = e.Date,
                        GroupId = groupId,
                        PaidUserBy = e.PaidUserBy,
                        IsSettled = e.Issettled
                    }).ToList()
                };
                response.Result = groupDto;
                response.Message = "Success";
            }
            catch (Exception ex) 
            {
               response.Message = ex.Message.ToString();
               response.IsSuccess = false;
            }
            return response;
        }

        [Authorize(Roles = "User")]
        [HttpPost("Create-Group")]
        public async Task<ResponseDTO> CreateGroup([FromBody] CreateGroupDTO createGroupDto)
        {
            try
            {
                var group = new Group
                {
                    Name = createGroupDto.Name,
                    Description = createGroupDto.Description,
                    CreatedDate = DateTime.UtcNow.Date
                };
                var createdGroup = await _groupService.CreateGroupAsync(group);
                var model = new GroupDTO
                {
                    Id= createdGroup.Id,
                    Name = createdGroup.Name,
                    Description = createdGroup.Description,
                    CreatedDate = createdGroup.CreatedDate,
                    Members = new List<GroupMemberDTO>(),
                    Expenses = new List<ExpenseDTO>()
                };
                response.Message = "Group is created successfully!";
                response.Result = model;
            }
            catch (Exception ex) 
            {
                response.Message = ex.Message.ToString();
                response.IsSuccess = false;
            }
            return response;
        }

        [Authorize(Roles = "User")]
        [HttpPost("Add-Members/{groupId:int}")]
        public async Task<ResponseDTO> AddMemberToGroup(int groupId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if(userId == null)
                {
                    throw new UnauthorizedAccessException("Unauthorized access");
                } 
                var groupMember = new GroupMembers
                {
                    GroupId = groupId,
                    UserId = userId
                };
                await _groupService.AddMemberToGroupAsync(groupId, groupMember);
                var updatedGroup = await _groupService.GetGroupByIdAsync(groupId);
                var groupDto = new GroupDTO
                {
                    Id = updatedGroup.Id,
                    Name = updatedGroup.Name,
                    Description = updatedGroup.Description,
                    CreatedDate = updatedGroup.CreatedDate,
                    Members = updatedGroup.Members.Select(m => new GroupMemberDTO
                    {
                        UserId = m.UserId,
                        FullName = m.User.FullName,
                        Email = m.User.Email,
                    }).ToList(),
                    Expenses = updatedGroup.Expenses.Select(e => new ExpenseDTO
                    {
                        Id = e.Id,
                        Description = e.Description,
                        Amount = e.Amount,
                        Date = e.Date,
                        PaidUserBy = e.PaidUserBy,
                        IsSettled = e.Issettled
                    }).ToList()
                };
                response.Result = groupDto;
                response.Message = "Member added successfully!";

            }
            catch(Exception ex) 
            {
                response.Message = ex.Message.ToString();
                response.IsSuccess = false;
            }
            return response;
        }

        [Authorize(Roles = "User")]
        [HttpDelete("Delete-Group/{groupId:int}")]
        public async Task<ResponseDTO> DeleteGroup(int groupId)
        {
            try
            {
                await _groupService.DeleteGroupAsync(groupId);
                response.Message = $"All expenses within the group Id - {groupId} are deleted.";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message.ToString();
                response.IsSuccess = false;
            }
            return response;
        }
    }
}
