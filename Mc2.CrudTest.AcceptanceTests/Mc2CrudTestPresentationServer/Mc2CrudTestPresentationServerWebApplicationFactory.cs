using Mc2.CrudTest.AcceptanceTests.Mc2CrudTestPresentationServer.InMemoryRepository;
using Mc2.CrudTest.Infrastructure.EventStore.Repository;
using Mc2.CrudTest.Infrastructure.ReadSide;
using Mc2.CrudTest.Presentation.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.AcceptanceTests.Mc2CrudTestPresentationServer;

public class Mc2CrudTestPresentationServerWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.UseEnvironment("Development");

        builder.ConfigureServices(serviceCollection =>
        {
            ConfigureInMemoryDbContext(serviceCollection);

            ConfigureInMemoryEventStoreRepository(serviceCollection);
        });
    }

    private void ConfigureInMemoryEventStoreRepository(IServiceCollection serviceCollection)
    {
        var descriptorEventStore = serviceCollection.SingleOrDefault(
            sd => sd.ServiceType == typeof(IEventStoreRepository)
        );

        if (descriptorEventStore != null) serviceCollection.Remove(descriptorEventStore);

        serviceCollection.AddSingleton<IEventStoreRepository, InMemoryEventStoreRepository>();
    }

    private void ConfigureInMemoryDbContext(IServiceCollection serviceCollection)
    {
        var descriptorDbContext = serviceCollection.SingleOrDefault(
            sd => sd.ServiceType == typeof(DbContextOptions<Mc2CrudTestDbContext>)
        );

        if (descriptorDbContext != null) serviceCollection.Remove(descriptorDbContext);

        serviceCollection.AddDbContext<Mc2CrudTestDbContext>(
            options => options.UseInMemoryDatabase($"Mc2CrudTestDb_{Guid.NewGuid()}"),
            ServiceLifetime.Singleton
        );
    }
}