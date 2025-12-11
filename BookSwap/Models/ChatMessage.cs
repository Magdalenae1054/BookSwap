using System.ComponentModel.DataAnnotations;

namespace BookSwap.Models
{
    public class ChatMessage
    {
        [Key]
        public int MessageId { get; set; }

        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public int ListingId { get; set; }
        public string MessageText { get; set; }
        public DateTime SentAt { get; set; }

        public User FromUser { get; set; }
        public User ToUser { get; set; }
        public Listing Listing { get; set; }
    }
}
