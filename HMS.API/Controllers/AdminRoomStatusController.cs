using HMS.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers;

[Route("api/admin/room-status")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AdminRoomStatusController : ControllerBase
{
    private readonly IRoomStatusService _roomStatusService;

    public AdminRoomStatusController(IRoomStatusService roomStatusService)
    {
        _roomStatusService = roomStatusService;
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateRoomStatuses()
    {
        try
        {
            await _roomStatusService.UpdateRoomStatusesAsync();
            return Ok(new { message = "Room statuses updated successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Error updating room statuses: {ex.Message}" });
        }
    }
}
