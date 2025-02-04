using EventStore.Client;
using Mc2.CrudTest.Domain.Customer;
using Mc2.CrudTest.Infrastructure.EventStore.Customer;
using Mc2.CrudTest.Infrastructure.EventStore.Repository;
using Mc2.CrudTest.Infrastructure.ReadSide;
using Mc2.CrudTest.Infrastructure.ReadSide.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.Infrastructure;

public class InfrastructureBootstrapper
{
    public static void Run(IServiceCollection servicesCollection, IConfiguration configuration)
    {
        servicesCollection.AddScoped<IEventStoreRepository, EventStoreRepository>();
        servicesCollection.AddScoped<ICustomerRepository, CustomerRepository>();

        servicesCollection.AddScoped(_ =>
        {
            var eventStoreConnectionString = configuration.GetConnectionString("EventStore")!;
            var settings = EventStoreClientSettings.Create(eventStoreConnectionString);
            return new EventStoreClient(settings);
        });

        servicesCollection.AddDbContext<Mc2CrudTestDbContext>(
            options =>
            {
                var sqlServerConnectionString = configuration.GetConnectionString("SqlServer")!;
                options.UseSqlServer(sqlServerConnectionString);
            }
        );

        servicesCollection.AddMediatR(
            cfg => cfg.RegisterServicesFromAssembly(typeof(CustomerCreatedEventHandler).Assembly)
        );
    }
}