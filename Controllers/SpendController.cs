using System.Data.Common;
using FarmerApp.Api.DTO;
using FarmerApp.Api.Models;
using FarmerApp.Api.Services;
using FarmerApp.Api.ViewModels.Spending;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SpendController : Controller
{
    private readonly SpendService _spendService;

    public SpendController(SpendService spendService)
    {
        _spendService = spendService;
    }

    [HttpPost("own")]
    public async Task<IActionResult> AddOwn(SpendOwnVM own)
    {
        try
        {
            return Ok(await _spendService.AddAsync(own));
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost("fromWarehouse")]
    public async Task<IActionResult> AddFromWarehouse(SpendFromWarehouseVM fromWarehouse)
    {
        try
        {
            return Ok(await _spendService.AddAsync(fromWarehouse));
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost("salary")]
    public async Task<IActionResult> AddSalary(SpendSalaryVM salary)
    {
        try
        {
            return Ok(await _spendService.AddAsync(salary));
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet("location/{id}")]
    public async Task<ActionResult<LocationSpendDto>> GetSpending(int id)
    {
        try
        {
            return Ok(await _spendService.GetLocationSpendAsync(id));
        }
        catch (Exception e)
        {
            BadRequest(e);
            throw;
        }
    }
}

[ApiController]
[Route("[controller]")]
public class SpendTypeController : Controller
{
    private readonly ModelTypeService _modelTypeService;

    public SpendTypeController(ModelTypeService modelTypeService)
    {
        _modelTypeService = modelTypeService;
    }

    [HttpGet]
    public async Task<ActionResult<ModelTypeDto>> Get()
    {
        try
        {
            return Ok(await _modelTypeService.GetAllAsync<SpendType>());
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
