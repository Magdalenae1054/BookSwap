using BookSwap.Services.Interfaces;

namespace BookSwap.Services.Strategies

{
    public class RatingValidationStrategy : IRatingValidationStrategy
    {
        public void Validate(int stars, string comment)
        {
            if (stars < 1 || stars > 5)
                throw new ArgumentException("Rating mora biti između 1 i 5.");

            if (string.IsNullOrWhiteSpace(comment))
                throw new ArgumentException("Komentar je obavezan.");
        }
    }
}
