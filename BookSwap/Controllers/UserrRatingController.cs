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
            var fromUserIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(fromUserIdString))
            {
                return RedirectToAction("Login", "Account");
            }

            if (stars < 1 || stars > 5)
            {
                return RedirectToAction("Profile", "Account", new { id = toUserId });
            }

            int fromUserId = int.Parse(fromUserIdString);

            _writer.AddRating(fromUserId, toUserId, stars, comment);

            return RedirectToAction("Profile", "Account", new { id = toUserId });
        }



    }

}
