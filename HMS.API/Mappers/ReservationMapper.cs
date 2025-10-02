using HMS.API.DTOs.Reservation;
using HMS.API.Models;

namespace HMS.API.Mappers;

public static class ReservationMapper
{
    public static ReservationDto ToDto(Reservation reservation)
    {
        return new ReservationDto
        {
            Id = reservation.ReservationId,
            CustomerId = reservation.CustomerId,
            CustomerName = reservation.Customer?.FullName ?? "N/A",
            RoomId = reservation.RoomId,
            RoomNumber = reservation.Room?.RoomNumber ?? "N/A",
            RoomType = reservation.Room?.RoomType ?? "N/A",
            CheckInDate = reservation.CheckInDate,
            CheckOutDate = reservation.CheckOutDate,
            ReservationDate = reservation.ReservationDate,
            Status = reservation.Status,
            Price = 0 // Placeholder, actual price should come from service
        };
    }

    public static IEnumerable<ReservationDto> ToDtoList(IEnumerable<Reservation> reservations)
    {
        return reservations.Select(ToDto);
    }

    public static ReservationManagementDto ToManagementDto(Reservation reservation)
    {
        return new ReservationManagementDto
        {
            Id = reservation.ReservationId,
            CustomerId = reservation.CustomerId,
            CustomerName = reservation.Customer?.FullName ?? "N/A",
            CustomerEmail = reservation.Customer?.Email ?? "N/A",
            RoomId = reservation.RoomId,
            RoomNumber = reservation.Room?.RoomNumber ?? "N/A",
            RoomType = reservation.Room?.RoomType ?? "N/A",
            CheckInDate = reservation.CheckInDate,
            CheckOutDate = reservation.CheckOutDate,
            ReservationDate = reservation.ReservationDate,
            Status = reservation.Status,
            Price = 0 // Placeholder, actual price should come from service
        };
    }

    public static IEnumerable<ReservationManagementDto> ToManagementDtoList(IEnumerable<Reservation> reservations)
    {
        return reservations.Select(ToManagementDto);
    }
}
