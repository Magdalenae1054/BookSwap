using BCrypt.Net;
using BookSwap.Models;
using BookSwap.Models.ViewModels;
using BookSwap.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

public class AccountController : Controller
{
    private readonly BookSwapContext _context;
    private readonly IUserRatingReader _reader;
    private readonly IUserRatingWriter _writer;

    public AccountController(BookSwapContext context, IUserRatingReader reader, IUserRatingWriter writer)
    {
        _context = context;
        _reader = reader;
        _writer = writer;
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
            //CreatedAt = DateTime.Now
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

    public IActionResult Profile(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == id);
        if (user == null)
            return NotFound();

        var avgRating = _reader.GetAverageRating(id);

        var loggedUserId = HttpContext.Session.GetString("UserId");

        var vm = new UserProfileViewModel
        {
            User = user,
            AverageRating = avgRating,
            CanRate = loggedUserId != null && loggedUserId != id.ToString()
        };

        return View(vm);
    }


    [HttpGet]
    public IActionResult AddRating(int toUserId)
    {
        ViewBag.ToUserId = toUserId;
        return View();
    }

    [HttpPost]
    public IActionResult AddRating(int toUserId, int stars, string comment)
    {
        var fromUserId = int.Parse(HttpContext.Session.GetString("UserId"));

        _writer.AddRating(fromUserId, toUserId, stars, comment);

        return RedirectToAction("Profile", new { id = toUserId });
    }

    public IActionResult Users()
    {
        var users = _context.Users.ToList();
        return View(users);
    }




}
