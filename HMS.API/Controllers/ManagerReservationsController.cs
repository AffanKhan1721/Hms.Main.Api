using HMS.API.Resources;
using HMS.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace HMS.API.Controllers;

[Route("api/manager/reservations")]
[ApiController]
[Authorize(Roles = "Manager,Admin")]
public class ManagerReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ManagerReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet("pending")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReservationManagementResponse>))]
    public async Task<IActionResult> GetPendingReservations()
    {
        var reservationResponses = await _reservationService.GetPendingReservationsResourceAsync();
        return Ok(reservationResponses);
    }

    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReservationManagementResponse>))]
    public async Task<IActionResult> GetAllReservations()
    {
        var reservationResponses = await _reservationService.GetAllReservationsResourceAsync();
        return Ok(reservationResponses);
    }

    [HttpPut("status")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateReservationStatusResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateReservationStatus([FromBody] UpdateReservationStatusRequest request)
    {
        var response = await _reservationService.UpdateReservationStatusResourceAsync(request);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }
}
