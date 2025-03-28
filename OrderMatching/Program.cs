using OrderMatching.Models;
using OrderMatching.Services;

OrderMatchingEngine engine = new OrderMatchingEngine();

var buyOrder = new Order(100.0m, 10, "Eka");
var sellOrder = new Order(105.0m, 5, "Maria");

engine.Buy(buyOrder);
engine.Sell(sellOrder);

