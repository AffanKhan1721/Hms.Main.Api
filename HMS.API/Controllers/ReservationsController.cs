using HMS.API.Mappers;
using HMS.API.Resources;
using HMS.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateReservation([FromBody] CreateReservationRequest request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var dto = ResourceMapper.ToCreateReservationDto(request, int.Parse(userId));
        var result = await _reservationService.CreateReservationAsync(dto);

        if (!result.Success)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(ResourceMapper.ToCreateReservationResponse(result));
    }

    [HttpGet("my")]
    [Authorize]
    public async Task<IActionResult> GetMyReservations()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var reservationDtos = await _reservationService.GetUserReservationsAsync(int.Parse(userId));
        var reservationResponses = ResourceMapper.ToReservationResponseList(reservationDtos);
        return Ok(reservationResponses);
    }
}
