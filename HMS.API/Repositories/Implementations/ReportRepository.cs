using HMS.API.Data;
using HMS.API.DTOs.Reports;
using HMS.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HMS.API.Repositories.Implementations;

public class ReportRepository : IReportRepository
{
    private readonly HmsDbContext _dbContext;

    public ReportRepository(HmsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<AvailableRoomReportDto>> GetAllAvailableRoomsAsync()
    {
        return await _dbContext.Database
            .SqlQueryRaw<AvailableRoomReportDto>("EXEC dbo.GetAllAvailableRooms")
            .ToListAsync();
    }

    public async Task<IEnumerable<BookedRoomReportDto>> GetAllBookedRoomsAsync()
    {
        try
        {
            Console.WriteLine("Executing GetAllBookedRooms stored procedure...");
            var result = await _dbContext.Database
                .SqlQueryRaw<BookedRoomReportDto>("EXEC dbo.GetAllBookedRooms")
                .ToListAsync();
            Console.WriteLine($"GetAllBookedRooms returned {result.Count} rooms");
            foreach (var room in result)
            {
                Console.WriteLine($"Room: {room.RoomNumber} - Status: {room.Status}");
            }
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetAllBookedRoomsAsync: {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<UserReportDto>> GetAllUsersAsync()
    {
        return await _dbContext.Database
            .SqlQueryRaw<UserReportDto>("EXEC dbo.GetAllUsers")
            .ToListAsync();
    }
}
