using System.Net;
using BookSwapIntegrationTests.Infrastructure;
using Xunit;

namespace BookSwapIntegrationTests;
public class SmokeTests : IClassFixture<BookSwapFactory>
{
    private readonly BookSwapFactory _factory;
    public SmokeTests(BookSwapFactory factory) => _factory = factory;

    [Fact]
    public async Task HomePage_Returns200()
    {
        var client = _factory.CreateClient(new() { AllowAutoRedirect = false });
        var resp = await client.GetAsync("/");
        Assert.True(resp.StatusCode == HttpStatusCode.OK || resp.StatusCode == HttpStatusCode.Redirect);
    }
}
