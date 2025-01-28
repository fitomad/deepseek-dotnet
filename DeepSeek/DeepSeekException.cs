namespace Fitomad.DeepSeek;

public class DeepSeekException: Exception
{
    public Failure FailureReason { get; init; }
    
    public enum Failure
    {
        InvalidFormat = 400,
        AuthenticationFails = 401,
        InsufficientBalance = 402,
        InvalidParameters = 422,
        RateLimitExceeded = 429,
        ServerError = 500,
        ServerOverloaded = 503,
        Unknown = -1000,
        RequestParameter = -1001
    }
    
    public DeepSeekException(string message, Failure failure) : base(message)
    {
        FailureReason = failure;
    }
}