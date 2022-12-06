using FarmerApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Api;

public class FarmerDbContext : DbContext
{
    public DbSet<Location> Locations { get; set; }
    public DbSet<Spend> Spends { get; set; }
    public DbSet<SpendType> SpendTypes { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<MeasurementUnit> MeasurementUnits { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<WarehouseReception> WarehouseReceptions { get; set; }
    public DbSet<WithdrawalFromWarehouse> WithdrawalFromWarehouses { get; set; }
    
    public DbSet<User> Users { get; set; }
    public DbSet<OwnResource> OwnResources { get; set; }

    public FarmerDbContext(DbContextOptions<FarmerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<MeasurementUnit>().HasData(new MeasurementUnit { Id = 1, Name = "Кілограми" });
        builder.Entity<MeasurementUnit>().HasData(new MeasurementUnit { Id = 2, Name = "Літри" });
        builder.Entity<MeasurementUnit>().HasData(new MeasurementUnit { Id = 3, Name = "Тони" });

        builder.Entity<ProductType>().HasData(new ProductType { Id = 1, Name = "Мінеральні добрива" });
        builder.Entity<ProductType>().HasData(new ProductType { Id = 2, Name = "Засоби захисту" });
        builder.Entity<ProductType>().HasData(new ProductType { Id = 3, Name = "Насіння" });
        builder.Entity<ProductType>().HasData(new ProductType { Id = 4, Name = "Паливо" });
        builder.Entity<ProductType>().HasData(new ProductType { Id = 5, Name = "інше" });

        builder.Entity<OwnResource>().HasData(new OwnResource { Id = 1, Name = "Вода" });
        builder.Entity<OwnResource>().HasData(new OwnResource { Id = 2, Name = "Зерно" });

        builder.Entity<SpendType>().HasData(new SpendType { Id = 1, Name = "Продукти із складу" });
        builder.Entity<SpendType>().HasData(new SpendType { Id = 2, Name = "Власні русурси" });
        builder.Entity<SpendType>().HasData(new SpendType { Id = 3, Name = "Зарплати працівникам" });
    }
}