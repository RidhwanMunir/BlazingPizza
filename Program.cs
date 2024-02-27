// allows the app to use the BlazingPizza service
using BlazingPizza.Data;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting; 
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

// Register the LocalStorage service
builder.Services.AddBlazoredLocalStorage();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
// Register the pizzas service
builder.Services.AddSingleton<PizzaService>();

// Add the AppState class
builder.Services.AddScoped<PizzaSalesState>();
// await builder.Build().RunAsync();

// allows the app to access HTTP commands
builder.Services.AddHttpClient();
// registers the new PizzaStoreContext and provides the filename for the SQLite database
builder.Services.AddSqlite<PizzaStoreContext>("Data Source=pizza.db");

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


// Initialize the database
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PizzaStoreContext>();
    if (db.Database.EnsureCreated())
    {
        SeedData.Initialize(db);
    }
}

app.Run();