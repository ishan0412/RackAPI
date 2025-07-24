namespace RackApi.Exceptions;

public class MissingRequiredFieldException : CommonApiException
{
    public override int HttpStatusCode { get; } = (int)System.Net.HttpStatusCode.BadRequest;

    public MissingRequiredFieldException()
        : base("One or more required fields are missing from the request body.") { }
}
