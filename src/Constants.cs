namespace RackApi.Constants;

public static class ApiInfo
{
    public const string TITLE = "Rack API";
    public const string VERSION = "v1";
    public const string DESCRIPTION = "API for managing products in a rack.";
    public const string ENDPOINT = $"/swagger/{VERSION}/swagger.json";
}

public static class ApiPaths
{
    public const string PRODUCTS = "/products";
    public const string PRODUCT_BY_ID = "/products/{id:int}";
}

public static class UnimportedExceptionTypes
{
    public const string JSON_READER_EXCEPTION = "System.Text.Json.JsonReaderException";
}

public static class AppDeploymentEnvironments
{
    public const string DEVELOPMENT = "Development";
}
