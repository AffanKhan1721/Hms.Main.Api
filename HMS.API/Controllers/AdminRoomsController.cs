using HMS.API.DTOs.Room;
using HMS.API.Mappers;
using HMS.API.Resources;
using HMS.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
    public async Task<IActionResult> AddRoom([FromBody] AddRoomRequest request)
    {
        var dto = new AddRoomDto
        {
            RoomNumber = request.RoomNumber,
            RoomType = request.RoomType,
            Capacity = request.Capacity,
            Status = request.Status
        };

        var result = await _roomService.AddRoomAsync(dto);

        if (!result.Success)
        {
            return BadRequest(new AddRoomResponse
            {
                Success = false,
                Message = result.ErrorMessage
            });
        }

        return Ok(new AddRoomResponse
        {
            Success = true,
            Message = "Room added successfully.",
            RoomId = result.RoomId
        });
    }

    [HttpDelete("{roomId}")]
    public async Task<IActionResult> DeleteRoom(int roomId)
    {
        var result = await _roomService.DeleteRoomAsync(roomId);

        if (!result.Success)
        {
            return BadRequest(new DeleteRoomResponse
            {
                Success = false,
                Message = result.ErrorMessage
            });
        }

        return Ok(new DeleteRoomResponse
        {
            Success = true,
            Message = "Room deleted successfully."
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRooms()
    {
        var roomDtos = await _roomService.GetAllRoomsAsync();
        var roomResponses = ResourceMapper.ToRoomResponseList(roomDtos);
        return Ok(roomResponses);
    }
}
