using Microsoft.EntityFrameworkCore;

namespace BlazingPizza.Data;

public class PizzaStoreContext : DbContext
{
    /*
    public PizzaStoreContext(DbContextOptions options) : base(options)
    {
    }
    */
    public PizzaStoreContext(DbContextOptions<PizzaStoreContext> options)
        : base(options)
    { 
        // Your code here
    }

    public DbSet<PizzaSpecial> Specials { get; set; }
}    