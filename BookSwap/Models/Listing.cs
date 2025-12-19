namespace BookSwap.Models
{
    public class Listing
    {
        public int ListingId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string Type { get; set; }
        public decimal? Price { get; set; }
        public string Status { get; set; }
       

        public User User { get; set; }
        public Book Book { get; set; }
        public ICollection<ChatMessage> ChatMessages { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
