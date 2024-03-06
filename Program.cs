// allows the app to use the BlazingPizza service
using BlazingPizza.Data;
using BlazingPizza.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

// Register the LocalStorage service
builder.Services.AddBlazoredLocalStorage();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
// Register the pizzas service
builder.Services.AddSingleton<PizzaService>();
// Register the orderstate service
builder.Services.AddScoped<OrderState>();

// Add the AppState class
builder.Services.AddScoped<PizzaSalesState>();
// await builder.Build().RunAsync();

// allows the app to access HTTP commands
builder.Services.AddHttpClient();
// registers the new PizzaStoreContext and provides the filename for the SQLite database
builder.Services.AddSqlite<PizzaStoreContext>("Data Source=pizza.db");
builder.Services.AddSqlite<PizzaStoreContext2>("Data Source=pizza2.db");

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
// Defines the default route, mapping URLs to {controller}/{action}/{id?}
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

// Initialize the databases
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    // Get the PizzaStoreContext from the service provider
    var db1 = scope.ServiceProvider.GetRequiredService<PizzaStoreContext>();
    if (db1.Database.EnsureCreated())
    {
        // Seed the PizzaStoreContext database
        SeedData.Initialize(db1);
    }

    // Get the PizzaStoreContext2 from the service provider
    var db2 = scope.ServiceProvider.GetRequiredService<PizzaStoreContext2>();
    if (db2.Database.EnsureCreated())
    {
        // Seed the PizzaStoreContext2 database
        SeedData2.Initialize(db2); // Assuming you have a separate SeedData2 class for PizzaStoreContext2
    }
}


app.Run();