using System.Net.Mime;
using RackApi.Constants;
using RackApi.Dto;
using RackApi.Exceptions;
using RackApi.Models;
using static RackApi.Helpers.ProgramHelper;

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
app.Use(
    async (context, next) =>
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
            else if (
                (baseException is System.Text.Json.JsonException)
                && (
                    baseException.GetType().ToString()
                    != Constants.UnimportedExceptionTypes.JSON_READER_EXCEPTION
                )
            )
            {
                exceptionToReturn = new MissingRequiredFieldException();
            }
            else
            {
                throw;
            }
            context.Response.StatusCode = exceptionToReturn.HttpStatusCode;
            await context.Response.WriteAsJsonAsync(
                new { Error = exceptionToReturn.GetType().Name, exceptionToReturn.Message }
            );
        }
    }
);

// API endpoints:
// GET all products in the database:
app.MapGet(
    "/products",
    async (IProductService productService) =>
    {
        // Return a 500 Internal Server Error for any database-related exception.
        return await WrapAsyncServiceActionAndResult(async () =>
            Results.Ok(await productService.GetAllProductsAsync())
        );
    }
);

// POST a new product to the database:
app.MapPost(
    "/products",
    async (ProductDto productDto, IProductService productService) =>
    {
        // Map the DTO to the Product model:
        Product product = MapDtoToProduct(productDto);
        // Attempt adding the product to the database.
        // If a product with the same name and URL already exists, return a 403 Forbidden.
        // If a required field's value is null, return a 400 Bad Request.
        // For any other database-related exceptions, return a 500 Internal Server Error.
        return await WrapAsyncServiceActionAndResult(async () =>
        {
            Product createdProduct = await productService.CreateProductAsync(product);
            return Results.Created($"/products/{createdProduct.Id}", createdProduct);
        });
    }
);

// DELETE a product from the database by its ID:
app.MapDelete(
    "/products/{id:int}",
    async (int id, IProductService productService) =>
    {
        // Atempt deleting the product with the given ID from the database.
        // If the record with that ID does not exist, return a 404 Not Found.
        // For any other database-related exceptions, return a 500 Internal Server Error.
        return await WrapAsyncServiceActionAndResult(async () =>
        {
            if (!await productService.DeleteProductAsync(id))
            {
                throw new RecordNotFoundException(id);
            }
            return Results.NoContent();
        });
    }
);

// PUT an updated product in the database by its ID:
app.MapPut(
    "/products/{id:int}",
    async (ProductDto productDto, int id, IProductService productService) =>
    {
        // Map the DTO to the Product model:
        Product product = MapDtoToProduct(productDto);
        // Attempt updating the product with the given ID in the database.
        // If the record with that ID does not exist, return a 404 Not Found.
        // If a required field's value is null, return a 400 Bad Request.
        // For any other database-related exceptions, return a 500 Internal Server Error.
        return await WrapAsyncServiceActionAndResult(async () =>
        {
            Product? updatedProduct = await productService.UpdateProductAsync(id, product);
            if (updatedProduct is null)
            {
                throw new RecordNotFoundException(id);
            }
            return Results.Ok(updatedProduct);
        });
    }
);

app.Run();
