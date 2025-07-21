using RackApi.Data;
using RackApi.Models;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Accessor class for interacting with the clothing product database, to be attached to this app's service collection.
/// Note that this class must be registered <i>after</i> the <c>ProductDbContext</c>, as follows:
/// <code>
/// services.AddDatabaseConnection(configuration);
/// services.AddScoped&lt;IProductService, ProductService&gt;();
/// ...
/// </code>
/// </summary>
public class ProductService : IProductService
{
    /// <summary>
    /// Reference into the product database.
    /// </summary>
    private readonly ProductDbContext _context;
    
    /// <summary>
    /// Creates a new <c>ProductService</c> instance with the given <c>ProductDbContext</c>, through which it can access 
    /// the database.
    /// </summary>
    /// <param name="context">the <c>ProductDbContext</c> instance for the database, expected to already be registered 
    /// with the app's services via the <see cref="RackApi.Configuration.DatabaseConfig.AddDatabaseConnection()"/> 
    /// method.</param>
    public ProductService(ProductDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Asynchronously retrieves all products from the database.
    /// </summary>
    /// <returns>
    /// A list of all <c>Product</c> entities in the database, or an empty list if none exist.
    /// </returns>
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    /// <summary>
    /// Asynchronously creates a new product in the database.
    /// </summary>
    /// <param name="product">the product to enter into the database</param>
    /// <returns>
    /// The created <c>Product</c> entity, or <c>null</c> if adding it failed.
    /// </returns>
    public async Task<Product?> CreateProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }
}