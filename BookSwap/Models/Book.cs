using System.Reflection;

namespace BookSwap.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<Listing> Listings { get; set; }
    }
}
