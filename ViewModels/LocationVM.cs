using System.ComponentModel.DataAnnotations;
using FarmerApp.Api.Validations;

namespace FarmerApp.Api.ViewModels;

public class LocationVM
{
    [Required] public string Name { get; set; }
    [Required, NotZeroAndNotLessZero] public float Size { get; set; }

    [Required, MinLength(4), MaxLength(4), NumberOnly]
    public string Seson { get; set; }
}