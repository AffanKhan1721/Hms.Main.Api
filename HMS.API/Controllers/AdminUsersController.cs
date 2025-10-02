using HMS.API.DTOs.User;
using HMS.API.Mappers;
using HMS.API.Resources;
using HMS.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers;

[Route("api/admin/users")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AdminUsersController : ControllerBase
{
    private readonly IUserService _userService;

    public AdminUsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("managers")]
    public async Task<IActionResult> AddManager([FromBody] AddManagerRequest request)
    {
        var dto = new AddManagerDto
        {
            FullName = request.FullName,
            Email = request.Email,
            Password = request.Password,
            PhoneNumber = request.PhoneNumber
        };

        var result = await _userService.AddManagerAsync(dto);

        if (!result.Success)
        {
            return BadRequest(new AddManagerResponse
            {
                Success = false,
                Message = result.ErrorMessage
            });
        }

        return Ok(new AddManagerResponse
        {
            Success = true,
            Message = "Manager added successfully.",
            UserId = result.UserId
        });
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        var result = await _userService.DeleteUserAsync(userId);

        if (!result.Success)
        {
            return BadRequest(new DeleteUserResponse
            {
                Success = false,
                Message = result.ErrorMessage
            });
        }

        return Ok(new DeleteUserResponse
        {
            Success = true,
            Message = "User deleted successfully."
        });
    }
}
