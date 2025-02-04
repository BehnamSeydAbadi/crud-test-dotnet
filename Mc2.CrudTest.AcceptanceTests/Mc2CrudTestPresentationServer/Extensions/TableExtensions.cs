using Mc2.CrudTest.Application.Customer.Command;

namespace Mc2.CrudTest.AcceptanceTests.Mc2CrudTestPresentationServer.Extensions;

internal static class TableExtensions
{
    public static CreateCustomerCommand GetCreateCustomerCommand(this Table table)
    {
        var tableRow = table.Rows[0];

        return new CreateCustomerCommand
        {
            FirstName = tableRow[nameof(CreateCustomerCommand.FirstName)],
            LastName = tableRow[nameof(CreateCustomerCommand.LastName)],
            DateOfBirth = tableRow[nameof(CreateCustomerCommand.DateOfBirth)].ToDateTime(),
            PhoneNumber = tableRow[nameof(CreateCustomerCommand.PhoneNumber)],
            Email = tableRow[nameof(CreateCustomerCommand.Email)],
            BankAccountNumber = tableRow[nameof(CreateCustomerCommand.BankAccountNumber)],
        };
    }
}