using AGInventoryManagement.Domain.Products;
using AGInventoryManagement.Domain.Stocks;

namespace AGInventoryManagement.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }

    DbSet<Stock> Stocks { get; }

    DbSet<StockSnapshot> StockSnapshots { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
