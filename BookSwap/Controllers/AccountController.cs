
﻿using BCrypt.Net;
using BookSwap.Models;
using BookSwap.Models.ViewModels;
using BookSwap.Models.ViewModels;
using BookSwap.Services;
using BookSwap.Services.Interfaces;
using BookSwap.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;


namespace BookSwap.Controllers
{

   

    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IUserRatingReader _reader;
        private readonly IUserRatingWriter _writer;

        public AccountController(IAccountService accountService, IUserRatingReader reader, IUserRatingWriter writer)
        {
            _accountService = accountService;
            _reader = reader;
            _writer = writer;
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


        public IActionResult Profile(int id)
        {
            var user = _accountService.GetUserById(id);
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
            var users = _accountService.GetAllUsers();
            return View(users);
        }



    }
}
