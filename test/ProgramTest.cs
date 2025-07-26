using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RackApi.Data;
using Swashbuckle.AspNetCore.Swagger;

namespace test;

public class ProgramTest
{
    [Fact]
    public void AllRegisteredServices_NotNull()
    {
        WebApplicationFactory<Program> _factory = new WebApplicationFactory<Program>();
        using var scope = _factory.Services.CreateScope();

        Assert.NotNull(scope.ServiceProvider.GetService(typeof(ProductDbContext)));
        Assert.NotNull(
            scope.ServiceProvider.GetService(typeof(IApiDescriptionGroupCollectionProvider))
        );
        Assert.NotNull(scope.ServiceProvider.GetService(typeof(ISwaggerProvider)));
        Assert.NotNull(scope.ServiceProvider.GetService(typeof(IProductService)));
    }
}
