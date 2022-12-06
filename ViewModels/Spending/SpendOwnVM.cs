using System.ComponentModel.DataAnnotations;
using FarmerApp.Api.Validations;

namespace FarmerApp.Api.ViewModels.Spending;

public class SpendOwnVM
{
    [Required] public int LocationId { get; set; }
    [Required] public string OwnResourceName { get; set; }
    [Required, NotZeroAndNotLessZero] public float Price { get; set; }
}