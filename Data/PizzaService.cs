
namespace BlazingPizza.Data;

public class PizzaService
{
    public async Task<Pizza2[]> GetPizzasAsync()
    {
        // Fetch pizzas from a database or API
        // Handle exceptions appropriately
        // Return an array of Pizza objects
        // Example:
        return await Task.FromResult(new Pizza2[]
        {
            new Pizza2 { PizzaId = 1, Name = "Mozarella", Description = "New spicy chicken", Price = 10.99m, Vegetarian = true, Vegan = false },
            // Add more pizzas...
        });

    }
}
