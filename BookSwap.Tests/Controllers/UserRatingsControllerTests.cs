using System.Collections.Generic;
using BookSwap.Controllers;
using BookSwap.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Xunit;


public class UserRatingsControllerTests
{
    private static UserRatingsController CreateController(
        Mock<IUserRatingWriter> writerMock,
        string? sessionUserId,
        out DefaultHttpContext httpContext)
    {
        var readerMock = new Mock<IUserRatingReader>();

        var controller = new UserRatingsController(readerMock.Object, writerMock.Object);

        httpContext = new DefaultHttpContext();
        httpContext.Session = new FakeSession();

        if (sessionUserId != null)
            httpContext.Session.SetString("UserId", sessionUserId);

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };

        controller.TempData = new TempDataDictionary(
            httpContext,
            Mock.Of<ITempDataProvider>());

        return controller;
    }

    [Fact]
    public void Add_WhenUserNotLoggedIn_RedirectsToLogin()
    {
        var writerMock = new Mock<IUserRatingWriter>();
        var controller = CreateController(writerMock, sessionUserId: null, out _);

        var result = controller.Add(toUserId: 5, stars: 4, comment: "ok");

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Login", redirect.ActionName);
        Assert.Equal("Account", redirect.ControllerName);

        writerMock.Verify(
            w => w.AddRating(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public void Add_WhenValid_CallsWriter_AndRedirectsToProfile()
    {
        var writerMock = new Mock<IUserRatingWriter>();
        var controller = CreateController(writerMock, sessionUserId: "10", out _);

        var result = controller.Add(toUserId: 7, stars: 5, comment: "  super  ");

        writerMock.Verify(
            w => w.AddRating(10, 7, 5, "super"),
            Times.Once);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Profile", redirect.ActionName);
        Assert.Equal("Account", redirect.ControllerName);
        Assert.Equal(7, redirect.RouteValues!["id"]);
    }

    // Fake session za unit testove
    private sealed class FakeSession : ISession
    {
        private readonly Dictionary<string, byte[]> _store = new();

        public IEnumerable<string> Keys => _store.Keys;
        public string Id { get; } = "fake";
        public bool IsAvailable => true;

        public void Clear() => _store.Clear();
        public void Remove(string key) => _store.Remove(key);
        public void Set(string key, byte[] value) => _store[key] = value;
        public bool TryGetValue(string key, out byte[] value) => _store.TryGetValue(key, out value!);

        public Task CommitAsync(CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public Task LoadAsync(CancellationToken cancellationToken = default)
            => Task.CompletedTask;  
    }
}
