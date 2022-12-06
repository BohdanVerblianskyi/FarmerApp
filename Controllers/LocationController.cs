using System.Data.Common;
using FarmerApp.Api.DTO;
using FarmerApp.Api.Services;
using FarmerApp.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{
    private readonly LocationService _locationService;

    public LocationController(LocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] LocationVM locationVm)
    {
        try
        {
            var location = await _locationService.CreateLocation(locationVm);
            return CreatedAtAction(nameof(Create), location);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LocationDto>>> GetAll()
    {
        try
        {
            return await _locationService.GetAllAsync();
        }
        catch (DbException e)
        {
            return BadRequest(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}