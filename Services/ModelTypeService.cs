using AutoMapper;
using FarmerApp.Api.DTO;
using FarmerApp.Api.Models;

using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Api.Services;

public class ModelTypeService
{
    private readonly FarmerDbContext _db;
    private readonly IMapper _mapper;

    public ModelTypeService(FarmerDbContext db,IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    private async Task<List<ModelTypeDto>> GetAllAsync<TModel>(DbSet<TModel> models) where TModel : class
    {
        var model = await models.ToListAsync();
        return model.Select(m => _mapper.Map<ModelTypeDto>(m)).ToList();
    }
    
    public async Task<List<ModelTypeDto>> GetAllAsync<TModel>()
    {
        if (typeof(TModel) == typeof(MeasurementUnit))
        {
            return await GetAllAsync(_db.MeasurementUnits);
        }

        if (typeof(TModel) == typeof(ProductType))
        {
            return await GetAllAsync(_db.ProductTypes);
        }

        if (typeof(TModel) == typeof(OwnResource))
        {
            return await GetAllAsync(_db.OwnResources);
        }

        if (typeof(TModel) == typeof(Product))
        {
            return await GetAllAsync(_db.Products);
        } 
        
        if (typeof(TModel) == typeof(SpendType))
        {
            return await GetAllAsync(_db.SpendTypes);
        }

        throw new NotImplementedException();
    }
}