using System.Net;
using BookSwap.Models;
using BookSwapIntegrationTests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
namespace BookSwapIntegrationTests.Features.UserRatings;

public class UserRatingsHttpIntegrationTests : IClassFixture<BookSwapFactory>
{
    private readonly BookSwapFactory _factory;

    public UserRatingsHttpIntegrationTests(BookSwapFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Post_Add_WhenNotLoggedIn_RedirectsToLogin()
    {
        var client = _factory.CreateClient(new() { AllowAutoRedirect = false });

        var form = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string,string>("toUserId", "2"),
            new KeyValuePair<string,string>("stars", "5"),
            new KeyValuePair<string,string>("comment", "ok")
        });

        var resp = await client.PostAsync("/UserRatings/Add", form);

        Assert.Equal(HttpStatusCode.Redirect, resp.StatusCode);
        Assert.NotNull(resp.Headers.Location);
        // može biti /Account/Login ili /Account/Login?...
        Assert.Contains("/Account/Login", resp.Headers.Location!.ToString());
    }

    [Fact]
    public async Task Post_Add_WhenLoggedIn_SavesRating_AndRedirectsToProfile()
    {
        var client = _factory.CreateClient(new() { AllowAutoRedirect = false });

      
        var loginResp = await client.GetAsync("/test/login/1");
        Assert.Equal(HttpStatusCode.OK, loginResp.StatusCode);

        
        var form = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string,string>("toUserId", "2"),
            new KeyValuePair<string,string>("stars", "4"),
            new KeyValuePair<string,string>("comment", "  super  ")
        });

        var resp = await client.PostAsync("/UserRatings/Add", form);

        Assert.Equal(HttpStatusCode.Redirect, resp.StatusCode);
        Assert.Contains("/Account/Profile", resp.Headers.Location!.ToString());
        var location = resp.Headers.Location!.ToString();
        Assert.Contains("/Account/Profile", location);
        Assert.True(location.EndsWith("/2") || location.Contains("id=2"), $"Unexpected redirect: {location}");


        // 3) Provjeri DB (pravi integration dokaz)
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<BookSwapContext>();

        var saved = db.Ratings.Single(r => r.FromUserId == 1 && r.ToUserId == 2);
        Assert.Equal(4, saved.Stars);
        Assert.Equal("super", saved.Comment); 
    }
}
