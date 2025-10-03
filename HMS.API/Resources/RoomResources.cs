namespace HMS.API.Resources;

public class RoomResource
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int People { get; set; }
    public string State { get; set; } = string.Empty;
}
