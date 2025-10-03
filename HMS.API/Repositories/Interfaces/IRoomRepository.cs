using HMS.API.Models;

namespace HMS.API.Repositories.Interfaces;

public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetAllAsync();
    Task<Room?> GetByIdAsync(int roomId);
    Task<Room> AddAsync(Room room);
    Task DeleteAsync(Room room);
    Task UpdateStatusAsync(int roomId, string status);
    Task UpdateRoomStatusesBasedOnReservationsAsync();
    Task SaveChangesAsync();
}