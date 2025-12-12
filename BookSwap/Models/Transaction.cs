namespace BookSwap.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int ListingId { get; set; }
        public int LenderId { get; set; }
        public int BorrowerId { get; set; }
        public string Type { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }

        public Listing Listing { get; set; }
        public User Lender { get; set; }
        public User Borrower { get; set; }
    }
}
