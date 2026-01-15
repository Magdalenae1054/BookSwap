using BookSwap.Services.Interfaces;

namespace BookSwap.Services
{
    public class RatingValidationStrategy : IRatingValidationStrategy
    {
        public ValidationResult Validate(int fromUserId, int toUserId, int stars, string comment)
        
            => (stars is >= 1 and <= 5)
                ? ValidationResult.Ok()
                : ValidationResult.Fail("Rating mora biti između 1 i 5.");
        
    }
}
