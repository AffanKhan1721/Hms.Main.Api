using HMS.API.Data;
using HMS.API.Models;
using HMS.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HMS.API.Repositories.Implementations;

public class RoomRepository : IRoomRepository
{
    private readonly HmsDbContext _dbContext;

    public RoomRepository(HmsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        return await _dbContext.Rooms.ToListAsync();
    }

    public async Task<Room?> GetByIdAsync(int roomId)
    {
        return await _dbContext.Rooms.FirstOrDefaultAsync(r => r.RoomId == roomId);
    }

    public async Task<Room> AddAsync(Room room)
    {
        await _dbContext.Rooms.AddAsync(room);
        return room;
    }

    public async Task DeleteAsync(Room room)
    {
        _dbContext.Rooms.Remove(room);
        await Task.CompletedTask;
    }

    public async Task UpdateStatusAsync(int roomId, string status)
    {
        var room = await _dbContext.Rooms.FindAsync(roomId);
        if (room != null)
        {
            room.Status = status;
            _dbContext.Rooms.Update(room);
        }
        await Task.CompletedTask;
    }

    public async Task UpdateRoomStatusesBasedOnReservationsAsync()
    {
        var today = DateTime.Today;
        var approvedReservations = await _dbContext.Reservations
            .Where(r => r.Status == "Approved")
            .ToListAsync();
        var allRooms = await _dbContext.Rooms.ToListAsync();

        foreach (var room in allRooms)
        {
            var hasActiveReservation = approvedReservations.Any(r =>
                r.RoomId == room.RoomId &&
                r.CheckInDate <= today &&
                r.CheckOutDate > today);

            if (hasActiveReservation && room.Status != "Reserved")
            {
                room.Status = "Reserved";
                _dbContext.Rooms.Update(room);
            }
            else if (!hasActiveReservation && room.Status == "Reserved")
            {
                room.Status = "Available";
                _dbContext.Rooms.Update(room);
            }
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
