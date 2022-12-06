using System.ComponentModel.DataAnnotations;

namespace FarmerApp.Api.Models;

public class OwnResource
{
    [Required] public int Id { get; set; }
    [Required, MaxLength(50)] public string Name { get; set; }
}