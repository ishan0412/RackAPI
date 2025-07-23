namespace RackApi.Constants;

public static class Constants
{
    public static class Swagger
    {
        public const string TITLE = "Rack API";
        public const string VERSION = "v1";
        public const string DESCRIPTION = "API for managing products in a rack.";
        public const string ENDPOINT = $"/swagger/{VERSION}/swagger.json";
    }

    public static class Database
    {
        public static readonly string[] NON_NULLABLE_FIELDS = ["Name", "Url", "Price", "Vendor"];
    }

    public static class UnimportedExceptionTypes
    {
        public const string JSON_READER_EXCEPTION = "System.Text.Json.JsonReaderException";
    }
}