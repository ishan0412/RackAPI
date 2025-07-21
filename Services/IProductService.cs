using RackApi.Models;

/// <summary>
/// Abstraction of a service for managing clothing products, provisioning methods for accessing and manipulating product 
/// data.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Asynchronously retrieves all products from the database.
    /// </summary>
    /// <returns>
    /// An <c>IEnumerable</c> of all the <c>Product</c> entities in the database
    /// </returns>
    Task<IEnumerable<Product>> GetAllProductsAsync();

    /// <summary>
    /// Asynchronously creates a new product in the database.
    /// </summary>
    /// <param name="product">the product to enter into the database</param>
    /// <returns>
    /// The created <c>Product</c> entity, or <c>null</c> if adding it failed.
    /// </returns>
    Task<Product?> CreateProductAsync(Product product);
}