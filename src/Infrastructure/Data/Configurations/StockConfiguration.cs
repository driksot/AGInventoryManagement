using AGInventoryManagement.Domain.Products;
using AGInventoryManagement.Domain.Stocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AGInventoryManagement.Infrastructure.Data.Configurations;
public class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.HasKey(stock => stock.Id);

        builder.HasOne<Product>()
            .WithOne(product => product.Stock)
            .HasForeignKey<Stock>(stock => stock.ProductId);

        builder.HasMany(stock => stock.Snapshots)
            .WithOne()
            .HasForeignKey(snapshot => snapshot.StockId);
    }
}
