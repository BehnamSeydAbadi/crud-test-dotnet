using Mc2.CrudTest.Application.Customer.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Presentation.Server.Endpoints;

public class CustomerEndpoints
{
    public static void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("api/customers", async (IMediator mediator, [FromBody] CreateCustomerCommand command) =>
        {
            var customerId = await mediator.Send(command);
            return Results.Ok(customerId);
        });
    }
}