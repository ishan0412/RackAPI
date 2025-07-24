namespace RackApi.Exceptions;

public abstract class CommonApiException : Exception
{
    public abstract int HttpStatusCode { get; }

    protected CommonApiException(string message)
        : base(message) { }

    protected CommonApiException(string message, Exception innerException)
        : base(message, innerException) { }
}
