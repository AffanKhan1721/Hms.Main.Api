namespace HMS.API.Resources;

public class AddManagerRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
}

public class AddManagerResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public int? UserId { get; set; }
}

public class DeleteUserResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
}
