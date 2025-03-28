using OrderMatching.Models;

public class Transaction 
{
    public Guid TransactionId { get; } = Guid.NewGuid();

    public int Qty {get; set;}
    
    public decimal Price {get ; set;}

    public List<Order>? BuyOrders {get; set;}
    
    public List<Order>? SellOrders {get; set;}

}