namespace BookSwap.Services.Interfaces
{
   
        public interface IRatingValidationStrategy
        {
            ValidationResult Validate(int fromUserId, int toUserId, int stars, string comment);
        }

        public record ValidationResult(bool IsValid, string? ErrorMessage)
        {
            public static ValidationResult Ok() => new(true, null);
            public static ValidationResult Fail(string message) => new(false, message);
        }
    
}
