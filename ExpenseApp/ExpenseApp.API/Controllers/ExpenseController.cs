using ExpenseApp.Application.DTOs;
using ExpenseApp.Application.Services;
using ExpenseApp.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Claims;

namespace ExpenseApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly IGroupService _groupService;
        private readonly IAuthService _authService; 
        private ResponseDTO response;
        public ExpenseController(IExpenseService expenseService, IGroupService groupService,IAuthService authService)
        {
            _expenseService = expenseService;
            _groupService = groupService;
            _authService = authService;
            response = new ResponseDTO();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{expenseId:int}")]
        public async Task<ResponseDTO> GetExpenseById(int expenseId)
        {
            try
            {
                var expense = await _expenseService.GetExpenseByIdAsync(expenseId);
                if (expense == null)
                {
                    response.Result = null;
                    response.IsSuccess = false;
                    response.Message = "Expense not found!";
                    return response;    
                }
                var expenseDTO = new ExpenseDTO()
                {
                    Id = expenseId,
                    Description = expense.Description,
                    Amount = expense.Amount,
                    Date = expense.Date,
                    PaidUserBy = expense.PaidUserBy,
                    IsSettled = expense.Issettled,
                    GroupId = expense.GroupId
                };
                response.Result = expenseDTO;   
                response.Message = "Success!";
            }
            catch (Exception ex) 
            {
                response.Message = ex.Message.ToString();
                response.IsSuccess = false;
            }    
            return response;        
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Get-All-Expenses")]
        public async Task<ResponseDTO> GetAllExpenses()
        {
            try
            {
                var expenses = await _expenseService.GetAllExpensesAsync();
                response.Result = expenses.Select(expense => new ExpenseDTO
                {
                    Id = expense.Id,
                    Description = expense.Description,
                    Amount = expense.Amount,
                    Date = expense.Date,
                    PaidUserBy = expense.PaidUserBy,
                    IsSettled = expense.Issettled,
                    GroupId = expense.GroupId
                }).ToList();
                response.Message = "All expenses retrieved successfully.";
            }
            catch (Exception ex) 
            {
                response.Message = ex.Message.ToString();
                response.IsSuccess = false;
            }    
            return response;    
        }

        [Authorize(Roles = "User")]
        [HttpPost("Add-Expense/{groupId:int}")]
        public async Task<ResponseDTO> CreateExpense(int groupId, [FromBody] CreateExpenseDTO createExpenseDTO)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var group = await _groupService.GetGroupByIdAsync(groupId);
                if (group == null)
                {
                    response.Result = null;
                    response.Message = "Group not found.";
                    response.IsSuccess = false;
                    return response;
                }
                var expense = new Expense
                {
                    Description = createExpenseDTO.Description,
                    Amount = createExpenseDTO.Amount,
                    Date = createExpenseDTO.Date,
                    PaidUserBy = createExpenseDTO.PaidUserBy,
                    GroupId = groupId
                };
                if (group.Members.Count == 0)
                {
                    throw new KeyNotFoundException("Add members to the group.");
                }
                if(!group.Members.Any(m => m.UserId == userId )) 
                {
                    throw new KeyNotFoundException("Only group members can create expenses.");

                }
                var createdExpense = await _expenseService.CreateExpenseAsync(expense);
                var expenseDTO = new ExpenseDTO
                {
                    Id = createdExpense.Id,
                    Description = createdExpense.Description,
                    Amount = createdExpense.Amount,
                    Date = createdExpense.Date,
                    PaidUserBy = createdExpense.PaidUserBy,
                    IsSettled = createdExpense.Issettled,
                    GroupId = createdExpense.GroupId
                };
                response.Result = expenseDTO;
                response.Message = "Expense created successfully.";
            }
            catch (Exception ex) 
            {
                response.Message = ex.Message.ToString();
                response.IsSuccess = false;
            }
            return response;        
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Update-Expense/{expenseId:int}")]
        public async Task<ResponseDTO> UpdateExpense(int expenseId, [FromBody] UpdateExpenseDTO updateExpenseDto)
        {
            try
            {
                var expense = await _expenseService.GetExpenseByIdAsync(expenseId);
                if (expense == null)
                {
                    response.Result = null;
                    response.Message = "Expense not found";
                    response.IsSuccess = false;
                    return response;
                }
                expense.Description = updateExpenseDto.Description;
                expense.Amount = updateExpenseDto.Amount;
                expense.Date = updateExpenseDto.Date;
                expense.PaidUserBy = updateExpenseDto.PaidUserBy;
                await _expenseService.UpdateExpenseAsync(expenseId, expense);
                response.Message = $"Expense with Id {expenseId} updated successfully.";
            }
            catch(Exception ex)
            {
                response.Message = ex.Message.ToString();
                response.IsSuccess = false;
            }
            return response;
        }


        [Authorize(Roles = "User")]
        [HttpPost("Expense-Settle/{expenseId:int}")]
        public async Task<ResponseDTO> SettleExpense(int expenseId)
        {
            try
            {
                await _expenseService.SettleExpenseAsync(expenseId);
                await _expenseService.SplitExpenseAsync(expenseId);
                var expense = await _expenseService.GetExpenseByIdAsync(expenseId);
                if (expense != null)
                {
                    var expenseDTO = new SplitsExpenseDTO()
                    {
                        Id = expenseId,
                        Description = expense.Description,
                        Amount = expense.Amount,
                        Date = expense.Date,
                        PaidUserBy = expense.PaidUserBy,
                        IsSettled = expense.Issettled,
                        GroupId = expense.GroupId,
                        Splits = expense.Splits.Select(s => new ExpenseSplitDTO
                        {
                            ExpenseId = s.ExpenseId,
                            UserId = s.UserId,
                            FullName = s.FullName,
                            Balance = s.Balance
                        }).ToList()
                    };
                    response.Result = expenseDTO;
                    response.Message = "Expense settled successfully.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Expense not found";
                }
            }
            catch (Exception ex) 
            {
                response.Message = ex.Message.ToString();
                response.IsSuccess = false;
            }
            return response;
        }

        [Authorize(Roles = "User")]
        [HttpGet("User-Expenses")]
        public async Task<ResponseDTO> GetExpensesForLoggedInUser()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var expenses = await _expenseService.GetExpensesForUserAsync(userId);
                
                var result = expenses.Select(e => new SplitsExpenseDTO
                {
                    Id = e.Id,
                    Description = e.Description,
                    Amount = e.Amount,
                    Date = e.Date,
                    PaidUserBy = e.PaidUserBy,
                    IsSettled = e.Issettled,
                    GroupId = e.GroupId,
                    Splits = e.Splits?.Select(s => new ExpenseSplitDTO
                    {
                        ExpenseId = s.ExpenseId,
                        UserId = s.UserId,
                        
                        FullName = s.FullName,  
                        Balance = s.Balance
                    }).ToList()
                }).ToList();
                response.Result = result;
                response.Message = "Expenses retrieved successfully.";
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
