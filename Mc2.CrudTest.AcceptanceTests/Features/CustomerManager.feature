Feature: Customer Manager

As a an operator I wish to be able to Create, Update, Delete customers and list all customers

    Scenario: Create customer
        When As a an operator, I create the customer with the following details:
          | FirstName | LastName  | DateOfBirth | PhoneNumber   | Email           | BankAccountNumber |
          | Behnam    | SeydAbadi | 1997-03-29  | +989004150000 | behnam@mail.com | 123456789         |
        Then the customer should be created successfully