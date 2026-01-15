using BookSwap.Services.Interfaces;

namespace BookSwap.Factories
{
    public class RatingValidationFactory
    {
        private readonly IEnumerable<IRatingValidationStrategy> _strategies;

        public RatingValidationFactory(IEnumerable<IRatingValidationStrategy> strategies)
        {
            _strategies = strategies;
        }

        public IEnumerable<IRatingValidationStrategy> GetStrategies() => _strategies;
    }
}
