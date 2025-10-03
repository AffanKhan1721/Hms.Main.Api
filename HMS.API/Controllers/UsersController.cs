using HMS.API.Resources;
using HMS.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace HMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("guests")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateGuestResource))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateGuest([FromBody] CreateGuestCommand request)
    {
        var response = await _userService.CreateGuestResourceAsync(request);
        if (response.Message.StartsWith("Guest user created successfully") == false)
        {
            return BadRequest(response.Message);
        }
        return Ok(response);
    }
}
