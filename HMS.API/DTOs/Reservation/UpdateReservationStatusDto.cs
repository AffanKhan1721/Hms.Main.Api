namespace HMS.API.DTOs.Reservation;

public class UpdateReservationStatusDto
{
    public int ReservationId { get; set; }
    public string Status { get; set; } = string.Empty;
}
