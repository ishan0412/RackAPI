using Microsoft.EntityFrameworkCore;
using RackApi.Data;

/// <summary>
/// Static configuration class for setting up a connection to this app's underlying PostgreSQL database, registering a
/// <c>DbContext</c> with which the database can be accessed in the app's service collection.
/// </summary>
public static class DatabaseConfig
{
    /// <summary>
    /// Registers a <c>DbContext</c> with the app's service collection, opening a connection to the database.
    /// </summary>
    /// <param name="services">the app's service collection</param>
    /// <param name="configuration">the app's configuration, in which the environment variable <c>DefaultConnection</c>
    /// -- a string for connecting to the database -- must be defined</param>
    /// <returns>
    /// the input <paramref name="services"/> with a <c>DbContext</c> registered for accessing the database
    /// </returns>
    /// <exception cref="InvalidOperationException">if no value for <c>DefaultConnection</c> has yet been set</exception>
    public static IServiceCollection AddDatabaseConnection(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString =
            configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Database connection string not found.");

        // Register the ProductDbContext with PostgreSQL:
        services.AddDbContext<ProductDbContext>(options => options.UseNpgsql(connectionString));

        return services;
    }
}
