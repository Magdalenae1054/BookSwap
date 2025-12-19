using System.Reflection;

namespace BookSwap.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
       
       

        //public ICollection<Listing> Listings { get; set; }
        ////public ICollection<ChatMessage> SentMessages { get; set; }
        ////public ICollection<ChatMessage> ReceivedMessages { get; set; }
        //public ICollection<Transaction> LenderTransactions { get; set; }
        //public ICollection<Transaction> BorrowerTransactions { get; set; }
        //public ICollection<Rating> FromRatings { get; set; }
        //public ICollection<Rating> ToRatings { get; set; }
    }
}
