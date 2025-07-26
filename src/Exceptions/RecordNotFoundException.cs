namespace RackApi.Exceptions;

public class RecordNotFoundException : CommonApiException
{
    public override int HttpStatusCode { get; } = (int)System.Net.HttpStatusCode.NotFound;

    public RecordNotFoundException(int id)
        : base($"No product record with ID {id} exists in the database.") { }
}
