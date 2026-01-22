using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BookSwapIntegrationTests;
public class AccountIntegrationTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AccountIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_Login_Returns200()
    {
        var response = await _client.GetAsync("/Account/Login");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_Register_Returns200()
    {
        var response = await _client.GetAsync("/Account/Register");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_Users_Returns200()
    {
        var response = await _client.GetAsync("/Account/Users");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
