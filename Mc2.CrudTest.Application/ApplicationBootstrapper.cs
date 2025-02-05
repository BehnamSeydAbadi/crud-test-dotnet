using Mc2.CrudTest.Application.Customer.Command.CreateCustomer;
using Mc2.CrudTest.Application.Customer.Command.DeleteCustomer;
using Mc2.CrudTest.Application.Customer.Command.UpdateCustomer;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.Application;

public class ApplicationBootstrapper
{
    public static void Run(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient(
            typeof(IPipelineBehavior<UpdateCustomerCommand, Unit>),
            typeof(Customer.Command.UpdateCustomer.Behaviors.ValidateCustomerExistenceBehavior)
        );
        serviceCollection.AddTransient(
            typeof(IPipelineBehavior<DeleteCustomerCommand, Unit>),
            typeof(Customer.Command.DeleteCustomer.Behaviors.ValidateCustomerExistenceBehavior)
        );

        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCustomerCommand).Assembly));
    }
}