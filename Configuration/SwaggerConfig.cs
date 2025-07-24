using Microsoft.OpenApi.Models;
using RackApi.Constants;

/// <summary>
/// Static configuration class with convenience methods for setting this app up with Swagger services, with which API
/// documentation can be generated and a UI provided for testing the implemented API.
/// </summary>
public static class SwaggerConfig
{
    /// <summary>
    /// Registers Swagger services to an app's service collection, allowing them to find and document the API endpoints
    /// the app exposes.
    /// </summary>
    /// <param name="services">the app's service collection</param>
    /// <returns>
    /// the input <paramref name="services"/> with API metadata and an OpenAPI document generator registered
    /// </returns>
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(
                Constants.Swagger.VERSION,
                new OpenApiInfo
                {
                    Title = Constants.Swagger.TITLE,
                    Description = Constants.Swagger.DESCRIPTION,
                    Version = Constants.Swagger.VERSION,
                }
            );
        });
        return services;
    }

    /// <summary>
    /// Enables Swagger middleware on an app to serve a web interface and registered OpenAPI JSON document.
    /// </summary>
    /// <param name="app">the app</param>
    /// <returns>
    /// the input <paramref name="app"/> with middleware for the Swagger/OpenAPI frontend added
    /// </returns>
    public static WebApplication UseSwaggerDocumentation(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    Constants.Swagger.ENDPOINT,
                    $"{Constants.Swagger.TITLE} {Constants.Swagger.VERSION}"
                );
            });
        }
        return app;
    }
}
