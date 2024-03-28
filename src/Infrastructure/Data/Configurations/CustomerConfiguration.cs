using AGInventoryManagement.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AGInventoryManagement.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.FirstName)
            .HasMaxLength(50);

        builder.Property(c => c.LastName)
            .HasMaxLength(50);

        builder.Property(c => c.PhoneNumber)
            .HasMaxLength(25);

        builder.Property(c => c.Email)
            .HasMaxLength(255);

        builder.HasQueryFilter(c => !c.IsDeleted);

        builder.HasIndex(c => c.IsDeleted)
            .HasFilter("IsDeleted = 0");
    }
}
