using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.API.Models;

public class Reservation
{
    [Key]
    public int ReservationId { get; set; }

    // Foreign Key for Customer
    [Required]
    public int CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    public virtual User Customer { get; set; } = null!;

    // Foreign Key for Room
    [Required]
    public int RoomId { get; set; }

    [ForeignKey("RoomId")]
    public virtual Room Room { get; set; } = null!;

    [Required]
    public DateTime CheckInDate { get; set; }

    [Required]
    public DateTime CheckOutDate { get; set; }

    public DateTime ReservationDate { get; set; } = DateTime.UtcNow;

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Pending";
}