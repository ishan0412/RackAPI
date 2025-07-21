using Microsoft.EntityFrameworkCore;
using RackApi.Models;

namespace RackApi.Data;

/// <summary>
/// The <c>DbContext</c> for the database of clothing products this app queries, representing a session into it.
/// </summary>
public class ProductDbContext : DbContext
{   
    /// <summary>
    /// Creates a new <c>ProductDbContext</c> instance with the given options, which are used to configure the database 
    /// connection.
    /// </summary>
    /// <param name="options">a <c>DbContextOptions</c> object defining the database provider and connection string</param>
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options) { }

    /// <summary>
    /// Representation of the table in the database for clothing products (i.e., <c>Product</c> entries).
    /// </summary>
    public DbSet<Product> Products => Set<Product>();
}