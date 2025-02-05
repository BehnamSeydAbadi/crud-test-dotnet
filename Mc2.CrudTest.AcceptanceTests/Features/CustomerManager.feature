Feature: Customer Manager

As a an operator I wish to be able to Create, Update, Delete customers and list all customers

    Scenario: Create customer
        When As an operator, I create the customer with the following details:
          | FirstName | LastName  | DateOfBirth | PhoneNumber   | Email           | BankAccountNumber |
          | Behnam    | SeydAbadi | 1997-03-29  | +989004150000 | behnam@mail.com | 123456789         |
        Then the customer should be created successfully

    Scenario: Fail to create customer due to duplicate phone number
        Given there is an existing customer with the phone number "+989004150000"
        When As an operator, I create the customer with the following details:
          | FirstName | LastName  | DateOfBirth | PhoneNumber   | Email           | BankAccountNumber |
          | Behnam    | SeydAbadi | 1997-03-29  | +989004150000 | behnam@mail.com | 123456789         |
        Then an error "Duplicate phone number" should be thrown

    Scenario: Create customer with duplicate name and date of birth
        Given there is an existing customer with the following details:
          | FirstName | LastName  | DateOfBirth | PhoneNumber   | Email             | BankAccountNumber |
          | Behnam    | SeydAbadi | 1997-03-29  | +989009009090 | anotherx@mail.com | 98765432143434    |
        When As an operator, I create the customer with the following details:
          | FirstName | LastName  | DateOfBirth | PhoneNumber   | Email            | BankAccountNumber |
          | Behnam    | SeydAbadi | 1997-03-29  | +989004150001 | another@mail.com | 987654321         |
        Then an error "Duplicate customer" should be thrown

    Scenario: Create customer with duplicate email
        Given there is an existing customer with the following details:
          | FirstName | LastName  | DateOfBirth | PhoneNumber   | Email            | BankAccountNumber |
          | Behnam1   | SeydAbadi | 2003-03-29  | +989009009090 | another@mail.com | 98765432143434    |
        When As an operator, I create the customer with the following details:
          | FirstName | LastName  | DateOfBirth | PhoneNumber   | Email            | BankAccountNumber |
          | Behnam2   | SeydAbadi | 1997-03-29  | +989004150001 | another@mail.com | 987654321         |
        Then an error "Duplicate email" should be thrown

    Scenario: Update customer successfully
        Given there is an existing customer with the following details:
          | FirstName | LastName   | DateOfBirth | PhoneNumber   | Email            | BankAccountNumber |
          | Behnam1   | SeydAbadi1 | 2003-03-29  | +989999999999 | another@mail.com | 98765432143434    |
        When As an operator, I update the customer with the following details:
          | FirstName | LastName   | DateOfBirth | PhoneNumber   | Email                    | BankAccountNumber |
          | Behnam2   | SeydAbadi2 | 1997-03-29  | +989000000000 | anotherXanother@mail.com | 987654321         |
        Then the customer should be updated successfully