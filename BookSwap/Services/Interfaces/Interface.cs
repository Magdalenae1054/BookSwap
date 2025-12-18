namespace BookSwap.Services.Interfaces
{
    public interface IUserRatingReader
    {
        double GetAverageRating(int userId);
    }
}
