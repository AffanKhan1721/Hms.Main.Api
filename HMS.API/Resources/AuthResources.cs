namespace HMS.API.Resources;

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool RememberMe { get; set; } = false;
}

public class LoginResponse
{
    public string Role { get; set; } = string.Empty;
}

public class LogoutResponse
{
    public string Message { get; set; } = string.Empty;
}

public class MeResponse
{
    public string Role { get; set; } = string.Empty;
}
