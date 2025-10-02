using HMS.API.DTOs.Reports;
using HMS.API.Mappers;
using HMS.API.Resources;
using HMS.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> GetAvailableRoomsReport()
    {
        var reportDtos = await _reportService.GetAvailableRoomsReportAsync();
        var reportResponses = ReportMapper.ToAvailableRoomReportResponseList(reportDtos);
        return Ok(reportResponses);
    }

    [HttpGet("booked-rooms")]
    public async Task<IActionResult> GetBookedRoomsReport()
    {
        var reportDtos = await _reportService.GetBookedRoomsReportAsync();
        var reportResponses = ReportMapper.ToBookedRoomReportResponseList(reportDtos);
        return Ok(reportResponses);
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsersReport()
    {
        var reportDtos = await _reportService.GetUsersReportAsync();
        var reportResponses = ReportMapper.ToUserReportResponseList(reportDtos);
        return Ok(reportResponses);
    }
}
