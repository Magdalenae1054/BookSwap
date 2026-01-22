namespace BookSwap.Models.ViewModels
{
    public class UserProfileViewModel
    {
        public User? User { get; set; }
        public double AverageRating { get; set; }
        public bool CanRate { get; set; }
    }
}
