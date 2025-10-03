using HMS.API.Resources;
using HMS.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace HMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RoomResource>))]
    public async Task<IActionResult> GetAllRooms()
    {
        var roomResources = await _roomService.GetAllRoomsResourceAsync();
        return Ok(roomResources);
    }

    [HttpGet("available")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RoomResource>))]
    public async Task<IActionResult> GetAvailableRooms([FromQuery] string? checkIn, [FromQuery] string? checkOut)
    {
        var roomResources = await _roomService.GetAvailableRoomsResourceAsync(checkIn, checkOut);
        return Ok(roomResources);
    }
}
