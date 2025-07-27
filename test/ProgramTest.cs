using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using RackApi.Constants;
using RackApi.Data;
using RackApi.Exceptions;
using RackApi.Models;
using RackApi.test.TestHelpers;
using Swashbuckle.AspNetCore.Swagger;

namespace RackApi.test;

public class ProgramTest : IClassFixture<MockApiFactory>
{
    private readonly MockApiFactory _factory;

    public ProgramTest(MockApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public void AllRegisteredServices_NotNull()
    {
        using var scope = _factory.Services.CreateScope();

        Assert.NotNull(scope.ServiceProvider.GetService(typeof(ProductDbContext)));
        Assert.NotNull(
            scope.ServiceProvider.GetService(typeof(IApiDescriptionGroupCollectionProvider))
        );
        Assert.NotNull(scope.ServiceProvider.GetService(typeof(ISwaggerProvider)));
        Assert.NotNull(scope.ServiceProvider.GetService(typeof(IProductService)));
    }

    [Fact]
    public async Task ProductsGet_OnSuccess_ReturnsAllProducts()
    {
        _factory
            .ProductServiceMock.GetAllProductsAsync()
            .Returns(
                new List<Product>
                {
                    new FakeProductBuilder().WithId(FirstFakeProduct.ID).Build(),
                    new FakeProductBuilder().WithId(SecondFakeProduct.ID).Build(),
                }
            );
        var client = _factory.CreateClient();

        var response = await client.GetAsync(ApiPaths.PRODUCTS);

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        var products = await response.Content.ReadFromJsonAsync<List<Product>>();
        Assert.NotNull(products);
        Assert.Equal(2, products.Count);
        Assert.NotNull(products[0]);
        Assert.NotNull(products[1]);
        Assert.Equal(FirstFakeProduct.ID, products[0].Id);
        Assert.Equal(SecondFakeProduct.ID, products[1].Id);
    }

    [Fact]
    public async Task ProductsGet_OnDbError_ReturnsInternalServerError()
    {
        _factory
            .ProductServiceMock.GetAllProductsAsync()
            .Throws(new CommonDatabaseException(new FakeDatabaseException()));
        var client = _factory.CreateClient();

        var response = await client.GetAsync(ApiPaths.PRODUCTS);

        Assert.Equal(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);
        // Once an error response DTO is implemented, we can check its fields here.
    }
}
