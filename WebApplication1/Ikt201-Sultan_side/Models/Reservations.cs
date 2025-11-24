namespace Ikt201_Sultan_side.Models;

public class Reservation
{
    public int Id { get; set; }
    public required string CustomerName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public DateTime ReservationDate { get; set; }
    public int NumberOfGuests { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Confirmed, Cancelled, Completed
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}