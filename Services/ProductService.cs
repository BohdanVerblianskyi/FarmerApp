using AutoMapper;
using FarmerApp.Api.DTO;
using FarmerApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Api.Services;

public class ProductService
{
    private readonly FarmerDbContext _db;
    private readonly IMapper _mapper;

    public ProductService(FarmerDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetByType(int id)
    {
        var products = await _db.Products.Where(p => p.ProductTypeId == id).ToListAsync();
        return products.Select(GetProductDto).ToList();
    }

    public async Task<IEnumerable<ProductDto>> Get()
    {
        var locations = await _db.Products.ToListAsync();
        return locations.Select(GetProductDto).ToList();
    }

    public async Task<ProductDto> Get(int id)
    {
        var products = await _db.Products.Where(p => p.Id == id).ToListAsync();
        return products.Select(GetProductDto).Single();
    }

    private ProductDto GetProductDto(Product product)
    { 
      return _mapper.Map<ProductDto>(product);
    }
}