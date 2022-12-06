using FarmerApp.Api.DTO;
using FarmerApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Api.Services;

public class WarehouseService
{
    private readonly FarmerDbContext _db;

    public WarehouseService(FarmerDbContext db)
    {
        _db = db;
    }

    public async Task<List<WarehouseDto>> GetAllAsync()
    {
        var locations = await _db.Warehouses
            .Include(w => w.Product)
            .ThenInclude(p => p.MeasurementUnit)
            .ToListAsync();
        return locations.Select(ToWarehouseDte).ToList();
    }
    
    private WarehouseDto ToWarehouseDte(Warehouse warehouse)
    {
        return new WarehouseDto
        {
            Price = warehouse.Product.GetPrice(warehouse.Quantity),
            Quantity = warehouse.Quantity,
            ProductName = warehouse.Product.Name,
            MeasurementUnitName = warehouse.Product.MeasurementUnit.Name,
            PriceByUnit = warehouse.Product.Price
        };
    }
}