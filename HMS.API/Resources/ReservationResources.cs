namespace HMS.API.Resources;

public class CreateReservationRequest
{
    public int RoomRef { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }

}

public class CreateReservationResponse
{
    public int Id { get; set; }
    public decimal Price { get; set; }
}

public class ReservationResponse
{
    public int Id { get; set; }
    public string? RoomNumber { get; set; }
    public string? RoomType { get; set; }
    public string CheckIn { get; set; } = string.Empty;
    public string CheckOut { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
