using HMS.API.DTOs.Reports;

namespace HMS.API.Services.Interfaces;

public interface IReportService
{
    Task<IEnumerable<AvailableRoomReportDto>> GetAvailableRoomsReportAsync();
    Task<IEnumerable<BookedRoomReportDto>> GetBookedRoomsReportAsync();
    Task<IEnumerable<UserReportDto>> GetUsersReportAsync();
}
