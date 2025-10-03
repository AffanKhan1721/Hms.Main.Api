using HMS.API.Resources;
using HMS.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddManagerResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddManager([FromBody] AddManagerRequest request)
    {
        var response = await _userService.AddManagerResourceAsync(request);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpDelete("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteUserResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        var response = await _userService.DeleteUserResourceAsync(userId);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }
}
