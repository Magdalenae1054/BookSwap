using BookSwap.Factories;
using BookSwap.Services.Interfaces;

namespace BookSwap.Services.Decorators
{
    public class UserRatingDecorator
    {

        private readonly IUserRatingWriter _inner;
        private readonly RatingValidationFactory _factory;

        public UserRatingDecorator(IUserRatingWriter inner, RatingValidationFactory factory)
        {
            _inner = inner;
            _factory = factory;
        }

        public void AddRating(int fromUserId, int toUserId, int stars, string comment)
        {
            comment ??= string.Empty;

            foreach (var strategy in _factory.GetStrategies())
            {
                var result = strategy.Validate(fromUserId, toUserId, stars, comment);
                if (!result.IsValid)
                    throw new RatingValidationException(result.ErrorMessage ?? "Neispravni podaci.");
            }

            _inner.AddRating(fromUserId, toUserId, stars, comment.Trim());
        }
    }

    public class RatingValidationException : Exception
    {
        public RatingValidationException(string message) : base(message) { }
    }
}

