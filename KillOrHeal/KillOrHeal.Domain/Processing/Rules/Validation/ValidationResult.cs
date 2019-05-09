namespace KillOrHeal.Domain.Processing.Rules.Validation
{
    public class ValidationResult
    {
        private ValidationResult(bool isValid, string errorMessage)
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
        }

        public bool IsValid { get; }
        public string ErrorMessage { get; }

        public static ValidationResult Success()
        {
            return new ValidationResult(true, null);
        }

        public static ValidationResult Error(string message)
        {
            return new ValidationResult(false, message);
        }        
    }
}
