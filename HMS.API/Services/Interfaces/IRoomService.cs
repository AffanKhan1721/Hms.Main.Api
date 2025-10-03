using HMS.API.DTOs.Room;
using HMS.API.Resources;

namespace HMS.API.Services.Interfaces;

public interface IRoomService
{
    Task<IEnumerable<RoomDto>> GetAllRoomsAsync();
    Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(string? checkIn, string? checkOut);
    Task<AddRoomResultDto> AddRoomAsync(AddRoomDto dto);
    Task<DeleteRoomResultDto> DeleteRoomAsync(int roomId);
    Task<IEnumerable<RoomResource>> GetAllRoomsResourceAsync();
    Task<IEnumerable<RoomResource>> GetAvailableRoomsResourceAsync(string? checkIn, string? checkOut);
    Task<AddRoomResponse> AddRoomResourceAsync(AddRoomRequest request);
    Task<DeleteRoomResponse> DeleteRoomResourceAsync(int roomId);
}
