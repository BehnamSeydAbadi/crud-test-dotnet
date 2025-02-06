using Mc2.CrudTest.Application.Customer.Command.CreateCustomer;
using Mc2.CrudTest.Application.Customer.Command.DeleteCustomer;
using Mc2.CrudTest.Application.Customer.Command.UpdateCustomer;
using Mc2.CrudTest.Application.Customer.Query.GetAll;
using Mc2.CrudTest.Application.Customer.Query.GetById;
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

        endpoints.MapGet("api/customers", async (IMediator mediator) =>
        {
            var viewModels = await mediator.Send(new GetAllCustomersQuery());
            return Results.Ok(viewModels);
        });

        endpoints.MapGet("api/customers/{id:Guid}", async (IMediator mediator, Guid id) =>
        {
            var viewModel = await mediator.Send(new GetCustomerByIdQuery(id));

            if (viewModel is null) return Results.NoContent();
            else return Results.Ok(viewModel);
        });

        endpoints.MapPut("api/customers/{id:Guid}",
            async (IMediator mediator, Guid id, [FromBody] UpdateCustomerCommand command) =>
            {
                command.Id = id;
                await mediator.Send(command);
                return Results.Ok();
            });

        endpoints.MapDelete("api/customers/{id:Guid}",
            async (IMediator mediator, Guid id) =>
            {
                await mediator.Send(new DeleteCustomerCommand(id));
                return Results.Ok();
            });
    }
}