using System.ComponentModel.DataAnnotations;

namespace FarmerApp.Api.Models;

public class WithdrawalFromWarehouse 
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    [Required] public Product Product { get; set; }

    [Required] public int ProductId { get; set; }

    [Required] public float Quantity { get; set; }

    [Required] public float Price { get; set; }

    public List<Spend> Spendings { get; set; }
}