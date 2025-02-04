using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc2.CrudTest.Infrastructure.ReadSide.Customer;

public class CustomerReadModelConfiguration : IEntityTypeConfiguration<CustomerReadModel>
{
    public void Configure(EntityTypeBuilder<CustomerReadModel> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.FirstName).HasMaxLength(32).IsRequired();
        builder.Property(c => c.LastName).HasMaxLength(32).IsRequired();
        builder.Property(c => c.PhoneNumber).HasMaxLength(32).IsRequired();
        builder.Property(c => c.Email).HasMaxLength(64).IsRequired();
        builder.Property(c => c.BankAccountNumber).HasMaxLength(64).IsRequired();
        builder.Property(c => c.DateOfBirth).IsRequired();

        builder.HasIndex(
            nameof(CustomerReadModel.FirstName),
            nameof(CustomerReadModel.LastName),
            nameof(CustomerReadModel.DateOfBirth)
        ).IsUnique();

        builder.HasIndex(c => c.Email).IsUnique();
    }
}