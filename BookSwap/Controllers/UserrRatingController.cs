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

                if (string.IsNullOrEmpty(userIdString))
                {
                   
                    return RedirectToAction("Login", "Account");
                }

                int fromUserId = int.Parse(userIdString);

                _writer.AddRating(fromUserId, toUserId, stars, comment);

                return RedirectToAction("Profile", "Account", new { id = toUserId });
            }

        
    }

}
