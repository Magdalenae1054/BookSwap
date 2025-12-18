using Microsoft.AspNetCore.Mvc;
using BookSwap.Models.ViewModels;
using BookSwap.Services.Interfaces;

namespace BookSwap.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = _accountService.Login(model.Email, model.Password);
            if (!result.Success)
            {
                ViewBag.Error = result.Error;
                return View(model);
            }

            var user = _accountService.GetUserByEmail(model.Email);
            HttpContext.Session.SetString("UserId", user.UserId.ToString());
            HttpContext.Session.SetString("FullName", user.FullName);

            return RedirectToAction("Index", "Books");
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = _accountService.Register(model);
            if (!result.Success)
            {
                ViewBag.Error = result.Error;
                return View(model);
            }

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            _accountService.Logout();
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
