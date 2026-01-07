using BookSwap.Services.Interfaces;
using BookSwap.Services.Strategies;


namespace BookSwap.Factories
{
    public class RatingValidationFactory
    {
        public static IRatingValidationStrategy Create()
        {
            return new RatingValidationStrategy();
        }
    }
}
