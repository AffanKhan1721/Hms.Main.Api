using HMS.API.DTOs.Reports;
using HMS.API.Mappers;
using HMS.API.Resources;
using HMS.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace HMS.API.Controllers;

[Route("api/admin/reports")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AdminReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public AdminReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("available-rooms")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AvailableRoomReportResponse>))]
    public async Task<IActionResult> GetAvailableRoomsReport()
    {
        var reportResponses = await _reportService.GetAvailableRoomsReportAsync();
        return Ok(reportResponses);
    }

    [HttpGet("booked-rooms")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BookedRoomReportResponse>))]
    public async Task<IActionResult> GetBookedRoomsReport()
    {
        var reportResponses = await _reportService.GetBookedRoomsReportAsync();
        return Ok(reportResponses);
    }

    [HttpGet("users")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserReportResponse>))]
    public async Task<IActionResult> GetUsersReport()
    {
        var reportResponses = await _reportService.GetUsersReportAsync();
        return Ok(reportResponses);
    }
}
