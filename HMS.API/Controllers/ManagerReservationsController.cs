using HMS.API.DTOs.Reservation;
using HMS.API.Mappers;
using HMS.API.Resources;
using HMS.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> GetPendingReservations()
    {
        var reservationDtos = await _reservationService.GetPendingReservationsAsync();
        var reservationResponses = ResourceMapper.ToReservationManagementResponseList(reservationDtos);
        return Ok(reservationResponses);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllReservations()
    {
        var reservationDtos = await _reservationService.GetAllReservationsAsync();
        var reservationResponses = ResourceMapper.ToReservationManagementResponseList(reservationDtos);
        return Ok(reservationResponses);
    }

    [HttpPut("status")]
    public async Task<IActionResult> UpdateReservationStatus([FromBody] UpdateReservationStatusRequest request)
    {
        var dto = new UpdateReservationStatusDto
        {
            ReservationId = request.ReservationId,
            Status = request.Status
        };

        var result = await _reservationService.UpdateReservationStatusAsync(dto);

        if (!result.Success)
        {
            return BadRequest(new UpdateReservationStatusResponse
            {
                Success = false,
                Message = result.ErrorMessage
            });
        }

        return Ok(new UpdateReservationStatusResponse
        {
            Success = true,
            Message = "Reservation status updated successfully."
        });
    }
}
