using BookSwap.Models;﻿
using BookSwap.Services;
using BookSwap.Services.Interfaces;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IAccountService, AccountService>();


builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);

    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // => Secure flag
    options.Cookie.SameSite = SameSiteMode.Lax;
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
    options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
    options.Secure = CookieSecurePolicy.Always;
});

builder.Services.AddHsts(options =>
{
    options.MaxAge = TimeSpan.FromDays(365);
    options.IncludeSubDomains = true;
    options.Preload = true;
});


builder.Services.AddDbContext<BookSwapContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("BookSwapDb")));


builder.Services.AddScoped<IUserRatingReader, UserRatingService>();
builder.Services.AddScoped<IUserRatingWriter, UserRatingService>();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

var app = builder.Build();

app.Use(async (context, next) =>
{
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    await next();
});

// TEST BAZE
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BookSwapContext>();

    try
    {
        if (await db.Database.CanConnectAsync())
            Console.WriteLine("✔✔✔ CONNECTED TO DATABASE!");
        else
            Console.WriteLine("❌ ERROR: Cannot connect to database.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ ERROR while connecting: " + ex.Message);
    }
}

app.UseForwardedHeaders();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    if (context.Request.Headers["X-Forwarded-Proto"] == "https")
    {
        context.Response.Headers["Strict-Transport-Security"] =
            "max-age=31536000; includeSubDomains; preload";
    }

    context.Response.Headers["Content-Security-Policy"] =
       "default-src 'self'; " +
       "object-src 'none'; " +
       "base-uri 'self'; " +
       "frame-ancestors 'none'; " +
       "form-action 'self'; " +
       "img-src 'self' data:; " +
       "script-src 'self'; " +
       "style-src 'self' https://cdn.jsdelivr.net; " +
       "font-src 'self' https://cdn.jsdelivr.net;";

    await next();
});


app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();

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

 await app.RunAsync();

public static partial  class Program { }
