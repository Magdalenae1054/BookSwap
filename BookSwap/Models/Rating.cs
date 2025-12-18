using Microsoft.EntityFrameworkCore;

namespace BookSwap.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        public User FromUser { get; set; }
        public User ToUser { get; set; }

        
    }
}
