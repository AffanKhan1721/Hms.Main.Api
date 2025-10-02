using HMS.API.DTOs.Reports;

namespace HMS.API.Mappers;

public static class ReportMapper
{
    public static Resources.AvailableRoomReportResponse ToAvailableRoomReportResponse(AvailableRoomReportDto dto)
    {
        return new Resources.AvailableRoomReportResponse
        {
            RoomId = dto.RoomId,
            RoomNumber = dto.RoomNumber,
            RoomType = dto.RoomType,
            Capacity = dto.Capacity,
            Status = dto.Status
        };
    }

    public static IEnumerable<Resources.AvailableRoomReportResponse> ToAvailableRoomReportResponseList(IEnumerable<AvailableRoomReportDto> dtos)
    {
        return dtos.Select(ToAvailableRoomReportResponse);
    }

    public static Resources.BookedRoomReportResponse ToBookedRoomReportResponse(BookedRoomReportDto dto)
    {
        return new Resources.BookedRoomReportResponse
        {
            RoomId = dto.RoomId,
            RoomNumber = dto.RoomNumber,
            RoomType = dto.RoomType,
            Capacity = dto.Capacity,
            Status = dto.Status
        };
    }

    public static IEnumerable<Resources.BookedRoomReportResponse> ToBookedRoomReportResponseList(IEnumerable<BookedRoomReportDto> dtos)
    {
        return dtos.Select(ToBookedRoomReportResponse);
    }

    public static Resources.UserReportResponse ToUserReportResponse(UserReportDto dto)
    {
        return new Resources.UserReportResponse
        {
            UserId = dto.UserId,
            FullName = dto.FullName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Role = dto.Role
        };
    }

    public static IEnumerable<Resources.UserReportResponse> ToUserReportResponseList(IEnumerable<UserReportDto> dtos)
    {
        return dtos.Select(ToUserReportResponse);
    }
}
