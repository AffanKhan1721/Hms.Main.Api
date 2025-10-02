namespace HMS.API.Resources;

public class ReservationManagementResponse
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public int RoomId { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public string RoomType { get; set; } = string.Empty;
    public string CheckIn { get; set; } = string.Empty;
    public string CheckOut { get; set; } = string.Empty;
    public string ReservationDate { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

public class UpdateReservationStatusRequest
{
    public int ReservationId { get; set; }
    public string Status { get; set; } = string.Empty; // "Approved", "Declined", "Pending"
}

public class UpdateReservationStatusResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
}
