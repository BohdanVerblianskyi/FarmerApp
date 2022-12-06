using AutoMapper;
using FarmerApp.Api.DTO;
using FarmerApp.Api.Models;
using FarmerApp.Api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Api.Services;

public class LocationService
{
    private readonly FarmerDbContext _db;
    private readonly IMapper _mapper;

    public LocationService(FarmerDbContext db,IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<List<LocationDto>> GetAllAsync()
    {
        var locations = await _db.Locations.ToListAsync();
        return locations.Select(l => _mapper.Map<LocationDto>(l)).ToList();
    }

    public async Task<LocationDto> CreateLocation(LocationVM locationVm)
    {
        var location = await _db.Locations.AddAsync(_mapper.Map<Location>(locationVm));
        await _db.SaveChangesAsync();
        return _mapper.Map<LocationDto>( location.Entity);
    }
}