using BookSwap.Services;
using BookSwapIntegrationTests.Infrastructure;
using Xunit;

public class UserRatingServiceIntegrationTests
{
    [Fact]
    public void AddRating_Then_GetAverageRating_WorksAgainstRealDb()
    {
        var (ctx, conn) = SqliteContextFactory.Create();
        using (ctx)
        using (conn)
        {
            var service = new UserRatingService(ctx);

            service.AddRating(fromUserId: 1, toUserId: 2, stars: 5, comment: "ok");
            service.AddRating(fromUserId: 2, toUserId: 2, stars: 3, comment: "ok2");

            var avg = service.GetAverageRating(2);

            Assert.Equal(4.0, avg);
        }
    }
}
