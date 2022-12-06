using System.Data.Common;
using FarmerApp.Api.DTO;
using FarmerApp.Api.Models;
using FarmerApp.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MeasurementUnitsController : ControllerBase
{
    private readonly ModelTypeService _modelTypeService;
    private readonly FarmerDbContext _db;

    public MeasurementUnitsController(ModelTypeService modelTypeService, FarmerDbContext db)
    {
        _modelTypeService = modelTypeService;
        _db = db;
    }
    
    [HttpGet]
    public async Task<ActionResult<ModelTypeDto>> Get()
    {
        try
        {
            return Ok(await _modelTypeService.GetAllAsync<MeasurementUnit>());
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