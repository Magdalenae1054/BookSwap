using System;
using BookSwap.Models;
using BookSwap.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class UserRatingServiceTests
{
    private static BookSwapContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<BookSwapContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new BookSwapContext(options);
    }

    [Fact]
    public void AddRating_AddsRowToDb()
    {
        using var ctx = CreateContext();
        var service = new UserRatingService(ctx);

        service.AddRating(fromUserId: 1, toUserId: 2, stars: 5, comment: "super");

        var saved = Assert.Single(ctx.Ratings);
        Assert.Equal(1, saved.FromUserId);
        Assert.Equal(2, saved.ToUserId);
        Assert.Equal(5, saved.Stars);
        Assert.Equal("super", saved.Comment);
    }

    [Fact]
    public void GetAverageRating_WhenNoRatings_Returns0()
    {
        using var ctx = CreateContext();
        var service = new UserRatingService(ctx);

        var avg = service.GetAverageRating(userId: 999);

        Assert.Equal(0, avg);
    }

    [Fact]
    public void GetAverageRating_ReturnsCorrectAverage_ForUser()
    {
        using var ctx = CreateContext();
        ctx.Ratings.AddRange(
            new Rating { FromUserId = 1, ToUserId = 10, Stars = 5, Comment = "a" },
            new Rating { FromUserId = 2, ToUserId = 10, Stars = 3, Comment = "b" },
            new Rating { FromUserId = 3, ToUserId = 11, Stars = 1, Comment = "c" } 
        );
        ctx.SaveChanges();

        var service = new UserRatingService(ctx);

        var avg = service.GetAverageRating(userId: 10);

        Assert.Equal(4.0, avg);
    }
}
