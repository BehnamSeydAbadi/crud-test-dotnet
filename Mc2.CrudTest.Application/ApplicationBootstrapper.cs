using Mc2.CrudTest.Application.Customer.Command;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.Application;

public class ApplicationBootstrapper
{
    public static void Run(IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCustomerCommand).Assembly));
    }
}