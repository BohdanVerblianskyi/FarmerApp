using System.ComponentModel.DataAnnotations;

namespace FarmerApp.Api.Models;

public class WarehouseReception
{
    public int Id { get; set; }

    [MaxLength(50)] public string Invoice { get; set; }

    public DateTime Date { get; set; }
    
    [Required] public float Quantity { get; set; }

    [Required] public float Price { get; set; }
    
    [Required,MaxLength(50)] public string ProductName { get; set; }

    [Required,MaxLength(50)] public string MeasurementUnitName { get; set; }
}