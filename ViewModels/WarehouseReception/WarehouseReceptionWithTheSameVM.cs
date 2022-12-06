using System.ComponentModel.DataAnnotations;
using FarmerApp.Api.Validations;

namespace FarmerApp.Api.ViewModels.WarehouseReception;

public class WarehouseReceptionWithTheSameVM
{
    [MaxLength(50)] public string? Invoice { get; set; }

    [Required] public int ProductId { get; set; }

    [Required, NotZeroAndNotLessZero] public float Quantity { get; set; }

    [Required, NotZeroAndNotLessZero] public float Price { get; set; }
}