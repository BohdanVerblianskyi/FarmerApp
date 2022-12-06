using System.ComponentModel.DataAnnotations;
namespace FarmerApp.Api.Models;

public class Spend 
{
    public int Id { get; set; }

    [Required, MaxLength(150)] public string Description { get; set; }

    [Required] public Location Location { get; set; }

    [Required] public int LocationId { get; set; }

    [Required] public SpendType SpendType { get; set; }

    [Required] public int SpendTypeId { get; set; }

    [Required] public float Price { get; set; }

    public DateTime Date;

    public WithdrawalFromWarehouse? WithdrawalFromWarehouse { get; set; }

    public int? WithdrawalFromWarehouseId { get; set; }
}