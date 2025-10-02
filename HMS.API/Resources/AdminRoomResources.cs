namespace HMS.API.Resources;

public class AddRoomRequest
{
    public string RoomNumber { get; set; } = string.Empty;
    public string RoomType { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Status { get; set; } = "Available";
}

public class AddRoomResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public int? RoomId { get; set; }
}

public class DeleteRoomResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
}
