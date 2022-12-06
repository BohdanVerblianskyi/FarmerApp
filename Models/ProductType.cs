using System.ComponentModel.DataAnnotations;

namespace FarmerApp.Api.Models;

public class ProductType
{
    public int Id { get; set; }

    [Required, MaxLength(50)] public string Name { get; set; }

    public List<Product> Products { get; set; }
}