using Microsoft.AspNetCore.Mvc;
using BookSwap.Models;
using BookSwap.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

public class AccountController : Controller
{
    private readonly BookSwapContext _context;

    public AccountController(BookSwapContext context)
    {
        _context = context;
    }

    // GET: Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // POST: Login
    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            ViewBag.Error = "Pogrešan email ili lozinka.";
            return View();
        }

        HttpContext.Session.SetString("UserId", user.UserId.ToString());
        HttpContext.Session.SetString("FullName", user.FullName);

        return RedirectToAction("Index", "Books");
    }

    // GET: Register
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // POST: Register
    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // Provjeri postoji li korisnik
        if (_context.Users.Any(u => u.Email == model.Email))
        {
            ViewBag.Error = "Ovaj email već postoji.";
            return View();
        }

        var user = new User
        {
            FullName = model.FullName,
            Email = model.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
            Role = "User",
            CreatedAt = DateTime.Now
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return RedirectToAction("Login");
    }


    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
