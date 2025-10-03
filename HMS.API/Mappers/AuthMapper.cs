using HMS.API.Models;
using HMS.API.Resources;

namespace HMS.API.Mappers;

public static class AuthMapper
{
    public static LoginResponse ToLoginResponse(User user)
    {
        return new LoginResponse
        {
            Role = user.Role
        };
    }

    public static MeResponse ToMeResponse(User user)
    {
        return new MeResponse
        {
            Role = user.Role
        };
    }

    public static LogoutResponse ToLogoutResponse()
    {
        return new LogoutResponse
        {
            Message = "Logout successful."
        };
    }
}
