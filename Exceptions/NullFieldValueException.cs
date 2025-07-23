namespace RackApi.Exceptions;

public class NullFieldValueException : CommonApiException
{
    public override int HttpStatusCode { get; } = (int) System.Net.HttpStatusCode.BadRequest;
    public NullFieldValueException(string fieldName)
        : base($"The value for the passed product entity's '{fieldName}' field cannot be null.")
    {}
}