using BookSwap.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace BookSwap.Controllers
{
    public class AccountController : Controller
    {
        private static List<User> users = new List<User>
        {
            new User { Username = "admin", Password = "password" }
        };

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User model)
        {
            var user = users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = "Neispravno korisničko ime ili lozinka.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Login");
        }
    }
}
