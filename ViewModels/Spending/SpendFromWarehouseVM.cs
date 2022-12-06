using System.ComponentModel.DataAnnotations;
using FarmerApp.Api.Validations;

namespace FarmerApp.Api.ViewModels.Spending;

public class SpendFromWarehouseVM
{
    [Required] public int LocationId { get; set; }
    [Required] public int ProductId { get; set; }
    [Required, NotZeroAndNotLessZero] public float Quantity { get; set; }
}