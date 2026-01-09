
using BookSwap.Models;﻿
using BookSwap.Models;
using BookSwap.Services;
using BookSwap.Services.Interfaces;
using BookSwap.Services;
using BookSwap.Services.Interfaces;

using Microsoft.EntityFrameworkCore;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddScoped<IAccountService, AccountService>();


builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);

});

builder.Services.AddDbContext<BookSwapContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("BookSwapDb")));

builder.Services.AddScoped<IUserRatingReader, UserRatingService>();
builder.Services.AddScoped<IUserRatingWriter, UserRatingService>();

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

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();   


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public partial class Program { }
