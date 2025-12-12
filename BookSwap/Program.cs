using BookSwap.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
using Microsoft.EntityFrameworkCore;
using BookSwap.Models; // ili namespace gdje je BookSwapContext


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookSwapContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookSwapConnection")));


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);

});

builder.Services.AddDbContext<BookSwapContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookSwapDb")));

var app = builder.Build();

// TEST BAZE
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BookSwapContext>();

    try
    {
        if (db.Database.CanConnect())
            Console.WriteLine("✔✔✔ CONNECTED TO DATABASE!");
        else
            Console.WriteLine("❌ ERROR: Cannot connect to database.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ ERROR while connecting: " + ex.Message);
    }
}

// ❗ REDOSLIJED MORA BITI OVAKAV
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();   // ← OVDJE ide session, SAMO JEDNOM

app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
