using System.Data.Common;
using FarmerApp.Api.DTO;
using FarmerApp.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WarehouseController : ControllerBase
{
    private readonly WarehouseService _warehouseService;

    public WarehouseController(WarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WarehouseDto>>> Get()
    {
        try
        {
            return Ok(await _warehouseService.GetAllAsync());
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