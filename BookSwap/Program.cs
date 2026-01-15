using BookSwap.Models;﻿
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

app.Use(async (context, next) =>
{
    context.Response.Headers["Content-Security-Policy"] =
        "default-src 'self'; " +
        "object-src 'none'; " +
        "base-uri 'self'; " +
        "frame-ancestors 'none'; " +
        "form-action 'self'; " +
        "img-src 'self' data:; " +
        "script-src 'self'; " +
        "style-src 'self';";

    await next();
});


app.UseStaticFiles();

app.UseRouting();

app.UseSession();

if (app.Environment.IsEnvironment("Testing"))
{
    app.MapGet("/test/login/{userId:int}", (HttpContext ctx, int userId) =>
    {
        ctx.Session.SetString("UserId", userId.ToString());
        return Results.Ok(new { ok = true, userId });
    });
}


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public partial class Program { }
