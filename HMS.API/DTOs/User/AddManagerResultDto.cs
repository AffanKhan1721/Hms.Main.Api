namespace HMS.API.DTOs.User;

public class AddManagerResultDto
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public int? UserId { get; set; }
}
