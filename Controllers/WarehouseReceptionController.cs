using System.Data.Common;
using FarmerApp.Api.DTO;
using FarmerApp.Api.Services;
using FarmerApp.Api.ViewModels.WarehouseReception;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WarehouseReceptionController : ControllerBase
{
    private readonly WarehouseReceptionService _warehouseReceptionService;

    public WarehouseReceptionController(WarehouseReceptionService warehouseReceptionService)
    {
        _warehouseReceptionService = warehouseReceptionService;
    }

    [HttpPost("theSameProduct")]
    public async Task<IActionResult> AddTheSame([FromBody] WarehouseReceptionWithTheSameVM reception)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest();
        }

        var warehouse = await _warehouseReceptionService.ProcessTheSameProductAsync(reception);

        return CreatedAtAction(nameof(AddTheSame), warehouse);
    }

    [HttpPost("newProduct")]
    public async Task<IActionResult> AddNew([FromBody] WarehouseReceptionWithNewVM reception)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest();
        }

        try
        {
            var warehouseReception = await _warehouseReceptionService.ProcessingNetProductAsync(reception);
            return CreatedAtAction(nameof(AddNew), warehouseReception);
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WarehouseReceptionDto>>> Get()
    {
        try
        {
            var warehouseReceptionDtos = await _warehouseReceptionService.GetAllAsync();
            return Ok(warehouseReceptionDtos);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet("page")]
    public async Task<ActionResult<IEnumerable<WarehouseReceptionDto>>> Get(int pageNumber, int pageSize)
    {
        try
        {
            var warehouseReceptionDtos = await _warehouseReceptionService.GetAllAsync(pageNumber, pageSize);
            return Ok(warehouseReceptionDtos);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}