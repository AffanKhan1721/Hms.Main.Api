namespace HMS.API.DTOs.Reservation;

public class CreateReservationResultDto
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public int? ReservationId { get; set; }
    public decimal? Price { get; set; }
}
