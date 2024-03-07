using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazingPizza.Data;

namespace BlazingPizza.Controllers;

[Route("orders")]
[ApiController]
public class OrdersController : Controller
{
    private readonly PizzaStoreContext2 _db;

    public OrdersController(PizzaStoreContext2 db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<BlazingPizza.Data.OrderWithStatus>>> GetOrders()
    {
        var orders = await _db.Orders
         .Include(o => o.Pizzas).ThenInclude(p => p.Special)
         .Include(o => o.Pizzas).ThenInclude(p => p.Toppings).ThenInclude(t => t.Topping)
         .OrderByDescending(o => o.CreatedTime)
         .ToListAsync();

        return orders.Select(o => BlazingPizza.Data.OrderWithStatus.FromOrder(o)).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<int>> PlaceOrder(BlazingPizza.Data.Order order)
    {
        order.CreatedTime = DateTime.Now;

        // Enforce existence of Pizza.SpecialId and Topping.ToppingId
        // in the database - prevent the submitter from making up
        // new specials and toppings
        foreach (var pizza in order.Pizzas)
        {
            pizza.SpecialId = pizza.Special.Id;
            pizza.Special = null;
        }

        _db.Orders.Attach(order);
        await _db.SaveChangesAsync();

        return order.OrderId;
    }
}
