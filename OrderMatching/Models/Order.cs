using System.Diagnostics.CodeAnalysis;

namespace OrderMatching.Models;

public class Order
{
    public Guid Id { get; } = Guid.NewGuid();
    public decimal Price { get; }
    public int Quantity { get; set; }
    public DateTime Timestamp { get; } = DateTime.UtcNow;
    
    public required string UserId {get; set;}
    
    [SetsRequiredMembers]
    public Order(decimal price, int quantity, string userId)
    {
        Price = price;
        Quantity = quantity;
        UserId = userId;
    }
}