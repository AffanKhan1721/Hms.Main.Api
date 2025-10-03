using HMS.API.Resources;
using HMS.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace HMS.API.Controllers;

[Route("api/admin/rooms")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AdminRoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public AdminRoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddRoomResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddRoom([FromBody] AddRoomRequest request)
    {
        var response = await _roomService.AddRoomResourceAsync(request);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpDelete("{roomId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteRoomResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteRoom(int roomId)
    {
        var response = await _roomService.DeleteRoomResourceAsync(roomId);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RoomResource>))]
    public async Task<IActionResult> GetAllRooms()
    {
        var roomResources = await _roomService.GetAllRoomsResourceAsync();
        return Ok(roomResources);
    }
}
