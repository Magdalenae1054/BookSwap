using BookSwap.Models;
using BookSwap.Models.ViewModels;
using BookSwap.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class AccountServiceTests
{
    private BookSwapContext GetContext()
    {
        var options = new DbContextOptionsBuilder<BookSwapContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new BookSwapContext(options);
    }

    [Fact]
    public void Login_InvalidEmail_ReturnsFail()
    {
        // GIVEN
        var context = GetContext();
        var service = new AccountService(context);

        // WHEN
        var result = service.Login("x@mail.com", "123");

        // THEN
        Assert.False(result.Success);
    }

    [Fact]
    public void Register_NewUser_Success()
    {
        // GIVEN
        var context = GetContext();
        var service = new AccountService(context);

        var model = new RegisterViewModel
        {
            FullName = "Test User",
            Email = "test@mail.com",
            Password = "1234"
        };

        // WHEN
        var result = service.Register(model);

        // THEN
        Assert.True(result.Success);
        Assert.Single(context.Users);
    }

    [Fact]
    public void Register_DuplicateEmail_Fails()
    {
        // GIVEN
        var context = GetContext();
        context.Users.Add(new User { Email = "test@mail.com", PasswordHash = "x" });
        context.SaveChanges();

        var service = new AccountService(context);

        var model = new RegisterViewModel
        {
            FullName = "Test",
            Email = "test@mail.com",
            Password = "1234"
        };

        // WHEN
        var result = service.Register(model);

        // THEN
        Assert.False(result.Success);
    }
}
