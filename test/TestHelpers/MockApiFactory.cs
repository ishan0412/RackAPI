using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using RackApi.Constants;

namespace RackApi.test.TestHelpers;

public class MockApiFactory : WebApplicationFactory<Program>
{
    public IProductService ProductServiceMock { get; } = Substitute.For<IProductService>();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(AppDeploymentEnvironments.DEVELOPMENT);
        builder.ConfigureServices(services =>
        {
            var serviceDescriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(IProductService)
            );
            if (serviceDescriptor is not null)
            {
                services.Remove(serviceDescriptor);
            }
            services.AddScoped(_ => ProductServiceMock);
        });
    }
}
