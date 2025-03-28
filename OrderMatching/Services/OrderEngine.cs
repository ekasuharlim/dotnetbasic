using OrderMatching.Models;

namespace OrderMatching.Services;

public class OrderMatchingEngine
{
    private SortedDictionary<decimal, Queue<Order>> _buyOrders = 
        new SortedDictionary<decimal, Queue<Order>>(Comparer<decimal>.Create( (x,y) => y.CompareTo(x)));

    private SortedDictionary<decimal, Queue<Order>> _sellOrders = new SortedDictionary<decimal, Queue<Order>>();

    
    public List<Transaction>? Buy(Order order)
    {
        // when buying order coming in, system need to check if there is any lower price available in sell queue
        // if yes then system need to fulfil all the requested amount from sell queue as long as the price is still lower than buy price 
        // if requested qty can't be fulfiled than the left over will need to be entered in buy queue, but if the qty available is higher than the buy qty
        // the left over will need to stay at sell queue

        Decimal minSellPrice = _sellOrders.Keys.Min();

        if (order.Price < minSellPrice) {
            if(_buyOrders.ContainsKey(order.Price)) {
                _buyOrders[order.Price].Enqueue(order);
            } else {
                _buyOrders.Add(order.Price, new Queue<Order>());
                _buyOrders[order.Price].Enqueue(order);
            }
            return null;
        }

        List<Transaction> transactions = new List<Transaction>();
        int requestedQty  = order.Quantity;
        int fulfilledQty = 0;

        while(_sellOrders.Count > 0 && minSellPrice < order.Price && requestedQty > fulfilledQty) {

            Transaction transaction = new Transaction{
                Price = minSellPrice,
                BuyOrders = new List<Order>(),
                SellOrders = new List<Order>(),
            };

            Queue<Order> sellQueue = _sellOrders[minSellPrice];

            while(requestedQty > fulfilledQty && sellQueue.Count > 0) {
                Order sellOrder = sellQueue.Peek();

                if (sellOrder.Quantity > (requestedQty - fulfilledQty)) {
                    sellOrder.Quantity -= (requestedQty - fulfilledQty);
                    fulfilledQty = requestedQty;
                    transaction.SellOrders.Add(sellOrder);
                    transactions.Add(transaction);
                } else {
                    fulfilledQty += sellOrder.Quantity;
                    transaction.SellOrders.Add(sellOrder);
                    sellQueue.Dequeue();
                    if (sellQueue.Count == 0) {
                        _sellOrders.Remove(minSellPrice);
                    }
                }


            }
            minSellPrice = _sellOrders.Keys.Min();          
        }

        if (requestedQty > fulfilledQty) {
            order.Quantity = requestedQty - fulfilledQty;
            if(_buyOrders.ContainsKey(order.Price)) {
                _buyOrders[order.Price].Enqueue(order);
            } else {
                _buyOrders.Add(order.Price, new Queue<Order>());
                _buyOrders[order.Price].Enqueue(order);
            }
        }

        return transactions;

    }

    public void Sell(Order order)
    {
        Console.WriteLine($"Sell order received: {order.Id}, Price: {order.Price}, Quantity: {order.Quantity}");
    }

    
}
