using System.Reflection;

namespace BookSwap.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }

        //public ICollection<Listing> Listings { get; set; }
        ////public ICollection<ChatMessage> SentMessages { get; set; }
        ////public ICollection<ChatMessage> ReceivedMessages { get; set; }
        //public ICollection<Transaction> LenderTransactions { get; set; }
        //public ICollection<Transaction> BorrowerTransactions { get; set; }
        //public ICollection<Rating> FromRatings { get; set; }
        //public ICollection<Rating> ToRatings { get; set; }
    }
}
