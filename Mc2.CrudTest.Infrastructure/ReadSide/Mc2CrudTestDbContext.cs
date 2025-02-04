using Mc2.CrudTest.Infrastructure.ReadSide.Customer;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Infrastructure.ReadSide;

public class Mc2CrudTestDbContext : DbContext
{
    public Mc2CrudTestDbContext(DbContextOptions<Mc2CrudTestDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerReadModelConfiguration).Assembly);
    }
}