using Mc2.CrudTest.Domain.Customer.Services;
using Mc2.CrudTest.Domain.Customer.Services.ValidateDuplicateCustomer;
using Mc2.CrudTest.Domain.Customer.Services.ValidateDuplicateEmail;
using Mc2.CrudTest.Domain.Customer.Services.ValidateDuplicatePhoneNumber;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.Domain;

public class DomainBootstrapper
{
    public static void Run(IServiceCollection services)
    {
        services.AddScoped<IValidateDuplicateCustomerDomainService, ValidateDuplicateCustomerDomainService>();
        services.AddScoped<IValidateDuplicateEmail, ValidateDuplicateEmail>();
        services.AddScoped<IValidateDuplicatePhoneNumberDomainService, ValidateDuplicatePhoneNumberDomainService>();
    }
}