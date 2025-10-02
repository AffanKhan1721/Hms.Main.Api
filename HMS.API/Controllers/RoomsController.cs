using HMS.API.Mappers;
using HMS.API.Resources;
using HMS.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRooms()
    {
        var roomDtos = await _roomService.GetAllRoomsAsync();
        var roomResponses = ResourceMapper.ToRoomResponseList(roomDtos);
        return Ok(roomResponses);
    }

    [HttpGet("available")]
    public async Task<IActionResult> GetAvailableRooms([FromQuery] string? checkIn, [FromQuery] string? checkOut)
    {
        var roomDtos = await _roomService.GetAvailableRoomsAsync(checkIn, checkOut);
        var roomResponses = ResourceMapper.ToRoomResponseList(roomDtos);
        return Ok(roomResponses);
    }
}
