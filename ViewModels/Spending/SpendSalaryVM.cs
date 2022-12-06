using System.ComponentModel.DataAnnotations;
using FarmerApp.Api.Validations;

namespace FarmerApp.Api.ViewModels.Spending;

public class SpendSalaryVM
{
    [Required] public int LocationId { get; set; }
    [Required] public string Employee { get; set; }
    [Required, NotZeroAndNotLessZero] public float Price { get; set; }
}