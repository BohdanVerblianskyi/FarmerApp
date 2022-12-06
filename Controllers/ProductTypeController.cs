using System.Data.Common;
using FarmerApp.Api.DTO;
using FarmerApp.Api.Models;
using FarmerApp.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductTypeController : ControllerBase
{
    private readonly ModelTypeService _modelTypeService;

    public ProductTypeController(ModelTypeService modelTypeService)
    {
        _modelTypeService = modelTypeService;
    }

    [HttpGet]
    public async Task<ActionResult<ModelTypeDto>> Get()
    {
        try
        {
            return Ok(await _modelTypeService.GetAllAsync<ProductType>());
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