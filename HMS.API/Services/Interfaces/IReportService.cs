using HMS.API.DTOs.Reports;
using HMS.API.Resources;

namespace HMS.API.Services.Interfaces;

public interface IReportService
{
    Task<IEnumerable<AvailableRoomReportResponse>> GetAvailableRoomsReportAsync();
    Task<IEnumerable<BookedRoomReportResponse>> GetBookedRoomsReportAsync();
    Task<IEnumerable<UserReportResponse>> GetUsersReportAsync();
}
