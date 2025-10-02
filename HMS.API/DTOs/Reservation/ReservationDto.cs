namespace HMS.API.DTOs.Reservation;

public class ReservationDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public int RoomId { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public string RoomType { get; set; } = string.Empty;
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public DateTime ReservationDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
