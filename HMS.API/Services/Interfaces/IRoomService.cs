using HMS.API.DTOs.Room;

namespace HMS.API.Services.Interfaces;

public interface IRoomService
{
    Task<IEnumerable<RoomDto>> GetAllRoomsAsync();
    Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(string? checkIn, string? checkOut);
    Task<AddRoomResultDto> AddRoomAsync(AddRoomDto dto);
    Task<DeleteRoomResultDto> DeleteRoomAsync(int roomId);
}
