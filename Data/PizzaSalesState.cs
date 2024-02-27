using System;
namespace BlazingPizza.Data;

public class PizzaSalesState
{
    // Define a property to hold the number of pizzas sold today 
    public int PizzasSoldToday { get; set; }

    // Define a method to increment the number of pizzas sold today
    public void IncrementSales()
    {
        PizzasSoldToday++;
    }
    
}