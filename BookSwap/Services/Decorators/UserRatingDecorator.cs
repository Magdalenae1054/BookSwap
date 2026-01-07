using BookSwap.Services.Interfaces;

namespace BookSwap.Services.Decorators
{
    public class UserRatingDecorator : IUserRatingWriter
    {

        private readonly IUserRatingWriter _inner;


        public UserRatingDecorator(IUserRatingWriter inner)
        {
            _inner = inner;
        }
        public void AddRating(int fromUserId, int toUserId, int stars, string comment)
        {

            Console.WriteLine($"User {fromUserId} rated user {toUserId} with {stars} stars");

            _inner.AddRating(fromUserId, toUserId, stars, comment);
        }
    }
}
