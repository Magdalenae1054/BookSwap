namespace BookSwap.Services.Interfaces
{
    public interface IUserRatingWriter
    {
        void AddRating(int fromUserId, int toUserId, int stars, string comment);
    }
}
