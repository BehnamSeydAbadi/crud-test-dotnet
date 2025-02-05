using Mc2.CrudTest.Application.Customer.Command;
using Mc2.CrudTest.Application.Customer.Command.CreateCustomer;
using Mc2.CrudTest.Application.Customer.Command.DeleteCustomer;
using Mc2.CrudTest.Application.Customer.Command.UpdateCustomer;
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

        endpoints.MapPut("api/customers/{id}",
            async (IMediator mediator, Guid id, [FromBody] UpdateCustomerCommand command) =>
            {
                command.Id = id;
                await mediator.Send(command);
                return Results.Ok();
            });

        endpoints.MapDelete("api/customers/{id}",
            async (IMediator mediator, Guid id) =>
            {
                await mediator.Send(new DeleteCustomerCommand(id));
                return Results.Ok();
            });
    }
}