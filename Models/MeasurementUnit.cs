using System.ComponentModel.DataAnnotations;

namespace FarmerApp.Api.Models;

public class MeasurementUnit 
{
    public int Id { get; set; }

    [Required, MaxLength(30)] public string Name { get; set; }

    public List<Product> Products { get; set; }
    
}