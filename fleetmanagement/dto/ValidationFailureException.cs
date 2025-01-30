using fleetmanagement.dto;

public class ValidationFailureException : Exception
{
    public ValidationFailure[] Failures { get; }

    // Constructor to accept ValidationFailure[] instead of string
    public ValidationFailureException(ValidationFailure[] failures)
        : base("Validation failed")
    {
        Failures = failures;
    }
}