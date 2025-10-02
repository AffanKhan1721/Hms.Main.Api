using HMS.API.DTOs.Reports;

namespace HMS.API.Repositories.Interfaces;

public interface IReportRepository
{
    Task<IEnumerable<AvailableRoomReportDto>> GetAllAvailableRoomsAsync();
    Task<IEnumerable<BookedRoomReportDto>> GetAllBookedRoomsAsync();
    Task<IEnumerable<UserReportDto>> GetAllUsersAsync();
}
