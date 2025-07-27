public class FakeDatabaseException : Exception
{
    public const string MESSAGE = "This is a fake exception for a failed database operation.";

    public FakeDatabaseException()
        : base(MESSAGE) { }
}
