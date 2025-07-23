namespace RackApi.Exceptions;

public class InvalidFieldValueTypeException : CommonApiException
{
    public override int HttpStatusCode { get; } = (int) System.Net.HttpStatusCode.BadRequest;
    public InvalidFieldValueTypeException()
        : base("The values in the request body for one or more fields were not of their expected types.")
    {}
}