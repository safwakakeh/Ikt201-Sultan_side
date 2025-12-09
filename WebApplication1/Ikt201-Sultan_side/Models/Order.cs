namespace Ikt201_Sultan_side.Models;

public class Order
{
    public int Id { get; set; }
    public required string CustomerName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Received"; // Received, Preparing, Delivered
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Relationship
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order? Order { get; set; }

    public int DishId { get; set; }
    public Dish? Dish { get; set; }

    public int Quantity { get; set; }
    public decimal PriceAtOrder { get; set; }
}
