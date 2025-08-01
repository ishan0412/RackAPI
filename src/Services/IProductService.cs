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
    /// The created <c>Product</c> entity.
    /// </returns>
    Task<Product> CreateProductAsync(Product product);

    /// <summary>
    /// Asynchronously deletes a product from the database by its ID.
    /// </summary>
    /// <param name="id">the ID of the product to delete</param>
    /// <returns>
    /// A boolean for if the deletion was successful or not.
    /// </returns>
    Task<bool> DeleteProductAsync(int id);

    /// <summary>
    /// Asynchronously updates an existing product in the database by its ID.
    /// </summary>
    /// <param name="id">the ID of the product to update</param>
    /// <param name="product">the updated properties of that product</param>
    /// <returns>
    /// The updated <c>Product</c> entity, or <c>null</c> if the update failed.
    /// </returns>
    Task<Product?> UpdateProductAsync(int id, Product product);
}
