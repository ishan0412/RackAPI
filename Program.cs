using RackApi.Models;
using RackApi.Constants;
using RackApi.Exceptions;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);
// Registers EFCore's DbContext with PostgreSQL, and opens a connection to the product database:
builder.Services.AddDatabaseConnection(builder.Configuration);
// Registers Swagger documentation:
builder.Services.AddSwaggerDocumentation();
// Registers the ProductService to access the database with:
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();
// Enables Swagger documentation and UI:
app.UseSwaggerDocumentation();
// Returns user-defined errors over ASP.NET's BadHttpRequestExceptions:
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (BadHttpRequestException ex)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        Exception baseException = ex.GetBaseException();
        CommonApiException exceptionToReturn;
        if (baseException is InvalidOperationException)
        {
            exceptionToReturn = new InvalidFieldValueTypeException();
        }
        else if ((baseException is System.Text.Json.JsonException)
        && (baseException.GetType().ToString() != Constants.UnimportedExceptionTypes.JSON_READER_EXCEPTION))
        {
            exceptionToReturn = new MissingRequiredFieldException();
        }
        else
        {
            throw;
        }
        context.Response.StatusCode = exceptionToReturn.HttpStatusCode;
        await context.Response.WriteAsJsonAsync(new
        {
            Error = exceptionToReturn.GetType().Name,
            exceptionToReturn.Message,
        });
    }
});

// API endpoints:
// GET all products in the database:
app.MapGet("/products", async (IProductService productService) =>
    await productService.GetAllProductsAsync()
);

// POST a new product to the database:
app.MapPost("/products", async (IProductService productService, Product product) =>
{
    Product createdProduct;
    try
    {
        createdProduct = await productService.CreateProductAsync(product);
    }
    // If a product with the same name and URL already exists, return a 403 Forbidden.
    // If a required field's value is null, return a 400 Bad Request.
    // For any other database-related exceptions, return a 500 Internal Server Error.
    catch (CommonApiException ex)
    {
        string errorName = ex.GetType().Name;
        string errorMessage = ex.Message;
        return Results.Json((ex is CommonDatabaseException) ?
        new
        {
            Error = errorName,
            InnerExceptionType = ex.InnerException?.GetType().Name,
            InnerExceptionMessage = ex.InnerException?.Message,
            errorMessage,
        } :
        new
        {
            Error = errorName,
            errorMessage,
        },
        statusCode: ex.HttpStatusCode);
    }
    return Results.Created($"/products/{createdProduct?.Id}", createdProduct);
});

// DELETE a product from the database by its ID:
app.MapDelete("/products/{id:int}", async (IProductService productService, int id) =>
{
    var deleteWasSuccessful = await productService.DeleteProductAsync(id);
    return deleteWasSuccessful ? Results.NoContent() : Results.NotFound();
});

// PUT an updated product in the database by its ID:
app.MapPut("/products/{id:int}", async (IProductService productService, int id, Product product) =>
{
    var updatedProduct = await productService.UpdateProductAsync(id, product);
    return updatedProduct != null ? Results.Ok(updatedProduct) : Results.NotFound();
});

app.Run();
