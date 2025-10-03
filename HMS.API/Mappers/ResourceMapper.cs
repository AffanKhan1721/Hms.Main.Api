using HMS.API.DTOs.Auth;
using HMS.API.DTOs.Reservation;
using HMS.API.DTOs.Room;
using HMS.API.DTOs.User;

namespace HMS.API.Mappers;

public static class ResourceMapper
{
    public static LoginDto ToLoginDto(Resources.LoginRequest request)
    {
        return new LoginDto
        {
            Email = request.Email,
            Password = request.Password,
            RememberMe = request.RememberMe
        };
    }

    public static Resources.LoginResponse ToLoginResponse(LoginResultDto dto)
    {
        return new Resources.LoginResponse
        {
            Role = dto.Role ?? string.Empty
        };
    }

    public static CreateGuestDto ToCreateGuestDto(Resources.CreateGuestCommand request)
    {
        return new CreateGuestDto
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            Phone = request.Phone
        };
    }

    public static Resources.CreateGuestResource ToCreateGuestResponse(CreateGuestResultDto dto)
    {
        return new Resources.CreateGuestResource
        {
            Message = dto.Success ? "Guest user created successfully." : dto.ErrorMessage ?? "Unknown error occurred."
        };
    }

    public static Resources.RoomResource ToRoomResponse(RoomDto dto)
    {
        return new Resources.RoomResource
        {
            Id = dto.Id,
            Number = dto.Number,
            Type = dto.Type,
            People = dto.Capacity,
            State = dto.Status
        };
    }

    public static IEnumerable<Resources.RoomResource> ToRoomResponseList(IEnumerable<RoomDto> dtos)
    {
        return dtos.Select(ToRoomResponse);
    }

    public static CreateReservationDto ToCreateReservationDto(Resources.CreateReservationCommand request, int userId)
    {
        return new CreateReservationDto
        {
            UserId = userId,
            RoomRef = request.RoomRef,
            CheckInDate = request.CheckInDate.ToString("yyyy-MM-dd"),
            CheckOutDate = request.CheckOutDate.ToString("yyyy-MM-dd")
        };
    }

    public static Resources.CreateReservationResource ToCreateReservationResponse(CreateReservationResultDto dto)
    {
        return new Resources.CreateReservationResource
        {
            Id = dto.ReservationId ?? 0,
            Price = dto.Price ?? 0
        };
    }

    public static Resources.ReservationResource ToReservationResponse(ReservationDto dto)
    {
        return new Resources.ReservationResource
        {
            Id = dto.Id,
            RoomNumber = dto.RoomNumber,
            RoomType = dto.RoomType,
            CheckIn = dto.CheckInDate.ToString("yyyy-MM-dd"),
            CheckOut = dto.CheckOutDate.ToString("yyyy-MM-dd"),
            Status = dto.Status
        };
    }

    public static IEnumerable<Resources.ReservationResource> ToReservationResponseList(IEnumerable<ReservationDto> dtos)
    {
        return dtos.Select(ToReservationResponse);
    }

    public static AddRoomDto ToAddRoomDto(Resources.AddRoomRequest request)
    {
        return new AddRoomDto
        {
            RoomNumber = request.RoomNumber,
            RoomType = request.RoomType,
            Capacity = request.Capacity,
            Status = request.Status
        };
    }

    public static Resources.AddRoomResponse ToAddRoomResponse(AddRoomResultDto dto)
    {
        return new Resources.AddRoomResponse
        {
            Success = dto.Success,
            Message = dto.Success ? "Room added successfully." : dto.ErrorMessage ?? "Unknown error occurred.",
            RoomId = dto.RoomId
        };
    }

    public static Resources.DeleteRoomResponse ToDeleteRoomResponse(DeleteRoomResultDto dto)
    {
        return new Resources.DeleteRoomResponse
        {
            Success = dto.Success,
            Message = dto.Success ? "Room deleted successfully." : dto.ErrorMessage ?? "Unknown error occurred."
        };
    }

    public static AddManagerDto ToAddManagerDto(Resources.AddManagerRequest request)
    {
        return new AddManagerDto
        {
            FullName = request.FullName,
            Email = request.Email,
            Password = request.Password,
            PhoneNumber = request.PhoneNumber
        };
    }

    public static Resources.AddManagerResponse ToAddManagerResponse(AddManagerResultDto dto)
    {
        return new Resources.AddManagerResponse
        {
            Success = dto.Success,
            Message = dto.Success ? "Manager added successfully." : dto.ErrorMessage ?? "Unknown error occurred.",
            UserId = dto.UserId
        };
    }

    public static Resources.DeleteUserResponse ToDeleteUserResponse(DeleteUserResultDto dto)
    {
        return new Resources.DeleteUserResponse
        {
            Success = dto.Success,
            Message = dto.Success ? "User deleted successfully." : dto.ErrorMessage ?? "Unknown error occurred."
        };
    }

    public static Resources.ReservationManagementResponse ToReservationManagementResponse(ReservationManagementDto dto)
    {
        return new Resources.ReservationManagementResponse
        {
            Id = dto.Id,
            CustomerId = dto.CustomerId,
            CustomerName = dto.CustomerName,
            CustomerEmail = dto.CustomerEmail,
            RoomId = dto.RoomId,
            RoomNumber = dto.RoomNumber,
            RoomType = dto.RoomType,
            CheckIn = dto.CheckInDate.ToString("yyyy-MM-dd"),
            CheckOut = dto.CheckOutDate.ToString("yyyy-MM-dd"),
            ReservationDate = dto.ReservationDate.ToString("yyyy-MM-dd"),
            Status = dto.Status,
            Price = dto.Price
        };
    }

    public static IEnumerable<Resources.ReservationManagementResponse> ToReservationManagementResponseList(IEnumerable<ReservationManagementDto> dtos)
    {
        return dtos.Select(ToReservationManagementResponse);
    }

    public static UpdateReservationStatusDto ToUpdateReservationStatusDto(Resources.UpdateReservationStatusRequest request)
    {
        return new UpdateReservationStatusDto
        {
            ReservationId = request.ReservationId,
            Status = request.Status
        };
    }

    public static Resources.UpdateReservationStatusResponse ToUpdateReservationStatusResponse(UpdateReservationStatusResultDto dto)
    {
        return new Resources.UpdateReservationStatusResponse
        {
            Success = dto.Success,
            Message = dto.Success ? "Reservation status updated successfully." : dto.ErrorMessage ?? "Unknown error occurred."
        };
    }
}
