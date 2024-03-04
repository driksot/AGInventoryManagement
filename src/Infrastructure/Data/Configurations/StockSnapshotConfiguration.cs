using AGInventoryManagement.Domain.Products;
using AGInventoryManagement.Domain.Stocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AGInventoryManagement.Infrastructure.Data.Configurations;
public class StockSnapshotConfiguration : IEntityTypeConfiguration<StockSnapshot>
{
    public void Configure(EntityTypeBuilder<StockSnapshot> builder)
    {
        builder.HasKey(snapshot => snapshot.Id);

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(snapshot => snapshot.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
