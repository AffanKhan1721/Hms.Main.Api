namespace HMS.API.DTOs.Room;

public class AddRoomResultDto
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public int? RoomId { get; set; }
}
