namespace RackApi.Exceptions;

public class CommonDatabaseException : CommonApiException
{
    public override int HttpStatusCode { get; } = (int) System.Net.HttpStatusCode.InternalServerError;
    public CommonDatabaseException(Exception innerException) 
        : base("There was an unexpected error in interacting with the database.", innerException)
    {}
}