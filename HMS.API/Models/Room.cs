using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.API.Models;

public class Room
{
    [Key]
    public int RoomId { get; set; }

    [Required]
    [StringLength(20)]
    public string RoomNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string RoomType { get; set; } = string.Empty;

    public int Capacity { get; set; }

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Pending";
}