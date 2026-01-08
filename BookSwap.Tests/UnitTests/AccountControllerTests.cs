using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BookSwap.Controllers;
using BookSwap.Services.Interfaces;
using BookSwap.Models.ViewModels;
using BookSwap.Models;
using System.Collections.Generic;

namespace BookSwap.Tests
{
    public class AccountControllerTests
    {
        private readonly Mock<IAccountService> _mockService;
        private readonly Mock<IUserRatingReader> _mockReader;
        private readonly Mock<IUserRatingWriter> _mockWriter;
        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            _mockService = new Mock<IAccountService>();
            _mockReader = new Mock<IUserRatingReader>();
            _mockWriter = new Mock<IUserRatingWriter>();
            _controller = new AccountController(_mockService.Object, _mockReader.Object, _mockWriter.Object);

            // Mocking Session (kontroler sprema UserId u session)
            var mockSession = new Mock<ISession>();
            var httpContext = new DefaultHttpContext();
            httpContext.Session = mockSession.Object;
            _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
        }

        [Fact]
        public void Profile_UserNotFound_ReturnsNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.GetUserById(It.IsAny<int>())).Returns((User)null);

            // Act
            var result = _controller.Profile(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Login_Post_InvalidModel_ReturnsView()
        {
            // Arrange: Simuliramo grešku u validaciji (npr. prazna polja)
            _controller.ModelState.AddModelError("Email", "Required");

            // Act
            var result = _controller.Login(new LoginViewModel());

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Users_ReturnsViewWithUserList()
        {
            // Arrange: Mockamo servis da vrati listu korisnika za UI
            var users = new List<User> { new User { FullName = "Test" } };
            _mockService.Setup(s => s.GetAllUsers()).Returns(users);

            // Act
            var result = _controller.Users() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<List<User>>(result.Model);
            Assert.Single(model);
        }
    }
}
