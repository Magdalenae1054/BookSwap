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
            int fromUserId = int.Parse(HttpContext.Session.GetString("UserId"));

            _writer.AddRating(fromUserId, toUserId, stars, comment);

            return RedirectToAction("Profile", "Account", new { id = toUserId });
        }
    }

}
