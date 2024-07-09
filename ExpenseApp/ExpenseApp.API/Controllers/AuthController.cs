using ExpenseApp.Application.DTOs;
using ExpenseApp.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private ResponseDTO response;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
            response = new ResponseDTO();
        }

        [HttpPost("login")]
        public async Task<ResponseDTO> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            try
            {
                var user = await _authService.AuthenticateUserAsync(loginRequestDTO.Email, loginRequestDTO.Password);
                if (user == null)
                {
                    response.Result = null;
                    response.Message = "Invalid Credentials.";
                    response.IsSuccess = false;
                    return response;
                }
                var token = _authService.GenerateJwtTokenAsync(user, user.Role);
                response.Result = token;
                response.Message = "Login Successfully!";
            }
            catch (Exception ex) 
            {
                response.Message = ex.Message.ToString();
                response.IsSuccess = false;
            }
            return response;    
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("User-Detail/{userId:Guid}")]
        public async Task<ResponseDTO> GetUserDetails(string userId)
        {
            try
            {
                var user = await _authService.GetUserByIdAsync(userId);
                if (user == null || user.Role != "User")
                {
                    response.Result = null;
                    response.Message = "User not Found!";
                    response.IsSuccess = false;
                    return response;
                }
                var userDto = new UserDTO
                {
                    UserId = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber
                };
                response.Message = "Success";
                response.Result = userDto;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message.ToString();
                response.IsSuccess = false;
            }
            return response;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Get-All-Users")]
        public async Task<ResponseDTO> GetAllUsers()
        {
            try
            {
                var users = await _authService.GetAllUsersAsync();
                var userDTO = new List<UserDTO>();
                foreach (var user in users)
                {
                    userDTO.Add(new UserDTO
                    {
                        UserId = user.Id,
                        FullName = user.FullName,
                        Email = user.Email,
                        UserName = user.UserName,
                        PhoneNumber = user.PhoneNumber, 
                    });
                }
                response.Result = userDTO;
                response.Message = "Success";
            }
            catch (Exception ex) 
            {
                response.Message = ex.Message.ToString();   
                response.IsSuccess = false; 
            }
            return response;    
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Update-User/{userId:Guid}")]
        public async Task<ResponseDTO> UpdateUser(string userId, [FromBody] UpdateUserDTO updateUser)
        {
            try
            {
                var user = await _authService.GetUserByIdAsync(userId);
                if (user == null || user.Role != "User")
                {
                    response.Result = null;
                    response.Message = "User not Found!";
                    response.IsSuccess = false;
                    return response;    
                }
                user.FullName = updateUser.FullName;
                user.Email = updateUser.Email;
                user.UserName = updateUser.Email;
                user.PhoneNumber = updateUser.PhoneNumber;
                user.NormalizedUserName = user.UserName.ToUpper();
                user.NormalizedEmail = user.Email.ToUpper();
                user.EmailConfirmed = true;

                var result = await _authService.UpdateUserAsync(user);
                if (!result)
                {
                    response.Result = null;
                    response.IsSuccess = false;
                    response.Message = "A problem happened while handling your request.";
                    return response;
                }
                var getUpdatedUser = await _authService.GetUserByIdAsync(userId);
                var userDTO = new UserDTO
                {
                    UserId = getUpdatedUser.Id,
                    FullName = getUpdatedUser.FullName,
                    Email = getUpdatedUser.Email,
                    UserName = getUpdatedUser.UserName,
                    PhoneNumber = getUpdatedUser.PhoneNumber
                };
                response.Result = userDTO;
                response.Message = "User details updated successfully!";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message.ToString();
                response.IsSuccess = false;
            }
            return response;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete-Users/{userId:Guid}")]
        public async Task<ResponseDTO> Delete(string userId)
        {
            try
            {
                var userDetails = await _authService.GetUserByIdAsync(userId);
                if(userDetails == null)
                {
                    response.Result = null;
                    response.IsSuccess = false;
                    response.Message = "User not Found!";
                    return response;        
                }
                await _authService.DeleteUser(userId);

                var userDTO = new UserDTO
                {
                    UserId = userDetails.Id,
                    FullName = userDetails.FullName,
                    Email = userDetails.Email,
                    UserName = userDetails.UserName,
                    PhoneNumber = userDetails.PhoneNumber
                };
                response.Result = userDTO;
                response.Message = $"User deleted successfully.";
            }
            catch(Exception ex)
            {
                response.Message = ex.Message.ToString();   
                response.IsSuccess = false;
            }
            return response;

        }

    }
}
