namespace Ikt201_Sultan_side.Models;

public class Dish
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public required string Category { get; set; }
    public string? Description { get; set; }
    public string? Ingredients { get; set; }
    public string? Allergens { get; set; } // Kommaseparert: "gluten,laktose,nøtter"
    public string? ImagePath { get; set; }
    public bool IsAvailable { get; set; } = true;
    public bool IsVegan { get; set; }
    public bool IsHalal { get; set; } = true; // Default to true as per previous assumption
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}