using System.Reflection;
using AGInventoryManagement.Application.Common.Interfaces;
using AGInventoryManagement.Domain.Customers;
using AGInventoryManagement.Domain.Products;
using AGInventoryManagement.Domain.Stocks;
using AGInventoryManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AGInventoryManagement.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
{
    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Stock> Stocks => Set<Stock>();

    public DbSet<StockSnapshot> StockSnapshots => Set<StockSnapshot>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
