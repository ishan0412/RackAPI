using Microsoft.EntityFrameworkCore;
using RackApi.Constants;
using RackApi.Data;
using RackApi.Exceptions;
using RackApi.Models;
using static RackApi.Helpers.ProductServiceHelper;

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

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await WrapAsyncDbOperation(() => _context.Products.ToListAsync());
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        // Validate that all required fields are present and not null:
        ThrowExceptionIfRequiredFieldIsNull(product);
        // If there already exists a product with the same name and URL, throw an exception:
        bool isDuplicate = await WrapAsyncDbOperation(() =>
            _context.Products.AnyAsync(p => (p.Name == product.Name) && (p.Url == product.Url))
        );
        if (isDuplicate)
        {
            throw new DuplicateRecordException(product.Name, product.Url);
        }

        _context.Products.Add(product);
        await WrapAsyncDbOperation(() => _context.SaveChangesAsync());
        return product;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        // If the product record with the given ID does not exist, return false:
        Product? productToDelete = await WrapAsyncDbOperation(async () =>
            await _context.Products.FindAsync(id)
        );
        if (productToDelete is null)
        {
            return false;
        }

        _context.Products.Remove(productToDelete);
        await WrapAsyncDbOperation(() => _context.SaveChangesAsync());
        return true;
    }

    public async Task<Product?> UpdateProductAsync(int id, Product product)
    {
        // Validate that all required fields are present and not null:
        ThrowExceptionIfRequiredFieldIsNull(product);
        // If the product record with the given ID does not exist, return null:
        Product? productToUpdate = await WrapAsyncDbOperation(async () =>
            await _context.Products.FindAsync(id)
        );
        if (productToUpdate is null)
        {
            return null;
        }

        productToUpdate.Name = product.Name;
        productToUpdate.Url = product.Url;
        productToUpdate.Price = product.Price;
        productToUpdate.BeforeSalePrice = product.BeforeSalePrice;
        productToUpdate.Vendor = product.Vendor;

        await WrapAsyncDbOperation(() => _context.SaveChangesAsync());
        return productToUpdate;
    }
}
