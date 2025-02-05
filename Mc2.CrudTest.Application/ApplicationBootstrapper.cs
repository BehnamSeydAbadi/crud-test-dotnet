using Mc2.CrudTest.Application.Customer.Command;
using Mc2.CrudTest.Application.Customer.Command.Behavior;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.Application;

public class ApplicationBootstrapper
{
    public static void Run(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient(
            typeof(IPipelineBehavior<CreateCustomerCommand, Guid>),
            typeof(ValidateDuplicatePhoneNumberBehavior)
        );
        
        serviceCollection.AddTransient(
            typeof(IPipelineBehavior<CreateCustomerCommand, Guid>),
            typeof(ValidateDuplicateCustomerBehavior)
        );

        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCustomerCommand).Assembly));
    }
}