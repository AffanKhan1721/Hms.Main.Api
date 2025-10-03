namespace HMS.API.DTOs.Reservation;

public class CreateReservationDto
{
    public int UserId { get; set; }
    public int RoomRef { get; set; }
    public string CheckInDate { get; set; } = string.Empty;
    public string CheckOutDate { get; set; } = string.Empty;
}
