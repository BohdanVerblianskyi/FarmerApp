using AutoMapper;
using FarmerApp.Api.DTO;
using FarmerApp.Api.Models;
using FarmerApp.Api.ViewModels.WarehouseReception;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Api.Services;

public class WarehouseReceptionService
{
    private readonly FarmerDbContext _db;
    private readonly IMapper _mapper;

    public WarehouseReceptionService(FarmerDbContext db,IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<List<WarehouseReceptionDto>> GetAllAsync()
    {
        var warehouseReceptions = await _db.WarehouseReceptions
            .OrderBy(w => w.Date)
            .ToListAsync();

        return warehouseReceptions.Select(w => _mapper.Map<WarehouseReceptionDto>(w)).Reverse().ToList();
    }

    public async Task<List<WarehouseReceptionDto>> GetAllAsync(int pageNumber, int pageSize)
    {
        var warehouseReceptions = await _db.WarehouseReceptions
            .OrderBy(w => w.Date)
            .Skip(pageNumber * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return warehouseReceptions.Select(w => _mapper.Map<WarehouseReceptionDto>(w)).Reverse().ToList();
    }

    public async Task<WarehouseReceptionDto> ProcessingNetProductAsync(WarehouseReceptionWithNewVM reception)
    {
        var theSameProduct = await _db.Products
            .Include(p => p.MeasurementUnit)
            .FirstOrDefaultAsync(p => p.Name == reception.ProductName);

        if (theSameProduct != null)
        {
            if (theSameProduct.MeasurementUnit.Id != reception.MeasurementUnitId)
            {
                throw new DirectoryNotFoundException();
            }

            return await ProcessTheSameProductAsync(new WarehouseReceptionWithTheSameVM
            {
                Invoice = reception.Invoice,
                Price = reception.Price,
                Quantity = reception.Quantity,
                ProductId = theSameProduct.Id
            });
        }
        
        var measurementUnit = await _db.MeasurementUnits.FirstOrDefaultAsync(m => m.Id == reception.MeasurementUnitId);
        
        var newProduct = await _db.Products.AddAsync(new Product
        {
            Name = reception.ProductName,
            Price = (float)Math.Round(reception.Price / reception.Quantity, 2),
            MeasurementUnitId = reception.MeasurementUnitId,
            ProductTypeId = reception.ProductTypeId,
        });

        await _db.SaveChangesAsync();

        var warehouseReception =  await _db.WarehouseReceptions
            .AddAsync(new WarehouseReception
            {
                Date = DateTime.UtcNow,
                Invoice = reception.Invoice,
                Price = reception.Price,
                Quantity = reception.Quantity,
                MeasurementUnitName = measurementUnit.Name,
                ProductName = newProduct.Entity.Name
            });

        await _db.SaveChangesAsync();

        await _db.Warehouses.AddAsync(new Warehouse
        {
            ProductId = newProduct.Entity.Id,
            Quantity = reception.Quantity
        });
        
        await _db.SaveChangesAsync();
        
        return _mapper.Map<WarehouseReceptionDto>(warehouseReception.Entity);
    }

    public async Task<WarehouseReceptionDto> ProcessTheSameProductAsync(WarehouseReceptionWithTheSameVM reception)
    {
        var currentProduct = await _db.Products.Include(p => p.MeasurementUnit)
            .FirstOrDefaultAsync(p => p.Id == reception.ProductId);

        if (currentProduct == null)
        {
            throw new ArgumentNullException(nameof(_db.Products));
        }

        var currentWarehouse = await _db.Warehouses.FirstOrDefaultAsync(w => w.ProductId == currentProduct.Id);

        var actualPrice = reception.Price;

        if (currentWarehouse == null)
        {
            await _db.Warehouses.AddAsync(new Warehouse
            {
                ProductId = reception.ProductId,
                Quantity = reception.Quantity
            });

            await _db.SaveChangesAsync();
        }
        else
        {
            var allQuantity = currentWarehouse.Quantity + reception.Quantity;
            var allPrice = (currentProduct.Price * currentWarehouse.Quantity) + reception.Price ;

            actualPrice = (float)Math.Round(allPrice / allQuantity, 2);

            currentWarehouse.Quantity = allQuantity;

            _db.Warehouses.Update(currentWarehouse);

            await _db.SaveChangesAsync();
        }

        currentProduct.Price = actualPrice;
        _db.Products.Update(currentProduct);

        var warehouseReception = await _db.WarehouseReceptions.AddAsync(new WarehouseReception
        {
            Date = DateTime.UtcNow,
            Invoice = reception.Invoice,
            Price = reception.Price,
            ProductName = currentProduct.Name,
            MeasurementUnitName = currentProduct.MeasurementUnit.Name,
            Quantity = reception.Quantity,
        });

        await _db.SaveChangesAsync();
        return _mapper.Map<WarehouseReceptionDto>(warehouseReception.Entity);
    }
}