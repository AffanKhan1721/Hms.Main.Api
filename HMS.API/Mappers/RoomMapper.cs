using HMS.API.DTOs.Room;
using HMS.API.Models;

namespace HMS.API.Mappers;

public static class RoomMapper
{
    public static RoomDto ToDto(Room room)
    {
        return new RoomDto
        {
            Id = room.RoomId,
            Number = room.RoomNumber,
            Type = room.RoomType,
            Capacity = room.Capacity,
            Status = room.Status
        };
    }

    public static IEnumerable<RoomDto> ToDtoList(IEnumerable<Room> rooms)
    {
        return rooms.Select(ToDto);
    }
}
