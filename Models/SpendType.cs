using System.ComponentModel.DataAnnotations;

namespace FarmerApp.Api.Models;

public class SpendType 
{
    public int Id { get; set; }

    [Required, MaxLength(50)] public string Name { get; set; }

    public List<Spend> Spends { get; set; }
}