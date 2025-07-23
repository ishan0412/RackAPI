namespace RackApi.Exceptions;

public class DuplicateRecordException : CommonApiException
{
    public override int HttpStatusCode { get; } = (int) System.Net.HttpStatusCode.Forbidden;
    public DuplicateRecordException(string name, string url)
        : base($"A product record with the name '{name}' and URL '{url}' already exists in the database, indicating "
        + "that the one passed here might be a duplicate.")
    {}
}