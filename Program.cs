using RackApi.Models;

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

// API endpoints:
// GET all products in the database:
app.MapGet("/products", async (IProductService productService) =>
    await productService.GetAllProductsAsync()
);

// POST a new product to the database:
app.MapPost("/products", async (IProductService productService, Product product) =>
{
    var createdProduct = await productService.CreateProductAsync(product);
    return Results.Created($"/products/{createdProduct?.Id}", createdProduct);
});

// DELETE a product from the database by its ID:
app.MapDelete("/products/{id:int}", async (IProductService productService, int id) =>
{
    var deleteWasSuccessful = await productService.DeleteProductAsync(id);
    return deleteWasSuccessful ? Results.NoContent() : Results.NotFound();
});

app.Run();
