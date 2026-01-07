namespace BookSwap.Services.Interfaces
{
    public interface IRatingValidationStrategy
    {
        void Validate(int stars, string comment);
    }
}
