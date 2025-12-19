using BookSwap.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookSwap.Controllers
{
    public class UserRatingsController : Controller
    {
        private readonly IUserRatingReader _reader;
        private readonly IUserRatingWriter _writer;

        public UserRatingsController(
            IUserRatingReader reader,
            IUserRatingWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }

        [HttpPost]
        public IActionResult Add(int toUserId, int stars, string comment)
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            if (!int.TryParse(userIdString, out int fromUserId))
            {
                return RedirectToAction("Login", "Account");
            }

            if (stars < 1 || stars > 5)
            {
                TempData["Error"] = "Rating mora biti između 1 i 5.";
                return RedirectToAction("Profile", "Account", new { id = toUserId });
            }

            if (string.IsNullOrWhiteSpace(comment))
            {
                TempData["Error"] = "Komentar je obavezan.";
                return RedirectToAction("Profile", "Account", new { id = toUserId });
            }

            _writer.AddRating(fromUserId, toUserId, stars, comment.Trim());

            return RedirectToAction("Profile", "Account", new { id = toUserId });
        }




    }

}
