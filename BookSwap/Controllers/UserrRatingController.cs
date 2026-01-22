using BookSwap.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookSwap.Controllers
{
    public class UserRatingsController : Controller
    {
        
        private readonly IUserRatingWriter _writer;

        public UserRatingsController(
            IUserRatingReader reader,
            IUserRatingWriter writer)
        {
           
            _writer = writer;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int toUserId, int stars, string comment)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(userIdString))
                return RedirectToAction("Login", "Account");

            if (!int.TryParse(userIdString, out var fromUserId))
                return RedirectToAction("Login", "Account");

            if (toUserId <= 0)
                ModelState.AddModelError(nameof(toUserId), "Neispravan korisnik.");

            if (stars < 1 || stars > 5)
                ModelState.AddModelError(nameof(stars), "Ocjena mora biti između 1 i 5.");

            if (string.IsNullOrWhiteSpace(comment))
                ModelState.AddModelError(nameof(comment), "Komentar je obavezan.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _writer.AddRating(fromUserId, toUserId, stars, comment.Trim());

            return RedirectToAction("Profile", "Account", new { id = toUserId });
        }





    }

}
