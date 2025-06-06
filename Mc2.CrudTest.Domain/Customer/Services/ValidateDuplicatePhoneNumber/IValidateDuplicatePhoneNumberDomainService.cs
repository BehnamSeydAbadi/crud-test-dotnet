﻿namespace Mc2.CrudTest.Domain.Customer.Services.ValidateDuplicatePhoneNumber;

public interface IValidateDuplicatePhoneNumberDomainService
{
    void Validate(string phoneNumber);
    void Validate(Guid aggregateId, string phoneNumber);
}