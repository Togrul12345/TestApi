namespace Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string failures)
        {
            Failures = failures;
        }
        public string Failures { get; }
    }
}
