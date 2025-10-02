namespace HMS.API.DTOs.Room;

public class AddRoomDto
{
    public string RoomNumber { get; set; } = string.Empty;
    public string RoomType { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Status { get; set; } = "Available";
}
