namespace Ikt201_Sultan_side.Models;

public class Review
{
    public int Id { get; set; }
    public required string CustomerName { get; set; }
    public required string Message { get; set; }
    public int Rating { get; set; } // 1-5 stjerner
    public bool IsApproved { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}