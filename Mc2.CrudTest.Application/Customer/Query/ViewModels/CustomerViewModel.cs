﻿using Mc2.CrudTest.Domain.Customer;

namespace Mc2.CrudTest.Application.Customer.Query.ViewModels;

public class CustomerViewModel : ICustomerBaseModel
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
    public required string BankAccountNumber { get; set; }
}