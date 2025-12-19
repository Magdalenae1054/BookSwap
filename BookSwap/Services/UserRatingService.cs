using BookSwap.Models;
using BookSwap.Services.Interfaces;

namespace BookSwap.Services
{
    public class UserRatingService : IUserRatingReader, IUserRatingWriter
    {

        private readonly BookSwapContext _context;

        public UserRatingService(BookSwapContext context)
        {
            _context = context;
        }
        public void AddRating(int fromUserId, int toUserId, int stars, string comment)
        {
            var rating = new Rating
            {
                FromUserId = fromUserId,
                ToUserId = toUserId,
                Stars = stars,
                Comment = comment,
                
            };

            _context.Ratings.Add(rating);
            _context.SaveChanges();
        }

        public double GetAverageRating(int userId)
        {
            return _context.Ratings
               .Where(r => r.ToUserId == userId)
               .Average(r => (double?)r.Stars) ?? 0;
        }
    }
}
