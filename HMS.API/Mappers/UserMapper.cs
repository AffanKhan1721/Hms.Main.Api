using HMS.API.Models;
using HMS.API.Resources;

namespace HMS.API.Mappers;

public static class UserMapper
{
    public static User ToModel(CreateGuestCommand request)
    {
        return new User
        {
            FullName = request.Name,
            Email = request.Email,
            PhoneNumber = request.Phone ?? string.Empty,
            Role = "Guest"
        };
    }

    public static UserResource ToResponse(User user)
    {
        return new UserResource
        {
            UserId = user.UserId,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role,
            PhoneNumber = user.PhoneNumber
        };
    }
}
