using System.ComponentModel.DataAnnotations;
using FarmerApp.Api.Validations;

namespace FarmerApp.Api.ViewModels.WarehouseReception;

public class WarehouseReceptionWithNewVM
{
    [MaxLength(50)] public string? Invoice { get; set; }

    [Required] public int ProductTypeId { get; set; }

    [Required, MaxLength(50)] public string ProductName { get; set; }

    [Required] public int MeasurementUnitId { get; set; }

    [Required, NotZeroAndNotLessZero] public float Quantity { get; set; }

    [Required, NotZeroAndNotLessZero] public float Price { get; set; }
}