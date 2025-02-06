
# CRUD Test .NET

A modern .NET application demonstrating CRUD operations using Clean Architecture, Domain-Driven Design (DDD), and Event Sourcing patterns.

## Technologies & Patterns

- .NET 7
- Entity Framework Core
- Docker
- Clean Architecture
- Domain-Driven Design (DDD)
- Event Sourcing
- CQRS Pattern
- Specification Pattern
- SQL Server
- Behavior-driven development (BDD)

## Features

- Customer management with CRUD operations
- Validation rules for customer data
- Unique constraints handling
- Event sourcing for data changes
- Read and write side separation (CQRS)

## Prerequisites

- Docker Desktop
- Git

## Getting Started

1. Clone the repository:

   ```bash
   git clone https://github.com/BehnamSeydAbadi/crud-test-dotnet.git
   ```

2. Navigate to the project directory:

   ```bash
   crud-test-dotnet\Mc2.CrudTest.Presentation\Server
   ```

3. Start the application using Docker:

   ```bash
   docker-compose up -d --build
   ```

The application will be available at:

- **API**: http://localhost:5000
- **Swagger Documentation**: http://localhost:5000/swagger

## Project Structure

- `Mc2.CrudTest.Application`: Application layer containing commands and queries.
- `Mc2.CrudTest.Domain`: Domain layer with business rules and domain models.
- `Mc2.CrudTest.Infrastructure`: Infrastructure layer handling persistence and external concerns.

## Key Features

- **Customer validation** including:
    - Unique email validation
    - Phone number validation
    - Bank account number validation
    - Duplicate customer check based on name, last name and date of birth
- **Event sourcing** for tracking all changes.
- **Separate read and write models** for optimal performance.