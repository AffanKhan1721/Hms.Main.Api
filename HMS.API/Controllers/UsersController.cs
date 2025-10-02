using HMS.API.Mappers;
using HMS.API.Resources;
using HMS.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> CreateGuest([FromBody] CreateGuestRequest request)
    {
        var dto = ResourceMapper.ToCreateGuestDto(request);
        var result = await _userService.CreateGuestAsync(dto);

        if (!result.Success)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(ResourceMapper.ToCreateGuestResponse(result));
    }
}
