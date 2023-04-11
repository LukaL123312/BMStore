namespace BMStore.Application.CustomExceptions;

public class AuthenticationException : Exception
{
    public AuthenticationException() : base("Authentication was failed.")
    { }
    public AuthenticationException(string message) : base(message) { }
    public AuthenticationException(string message, Exception innerException) : base(message, innerException)
    { }
}
