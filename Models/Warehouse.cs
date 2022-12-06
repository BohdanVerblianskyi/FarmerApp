using System.ComponentModel.DataAnnotations;

namespace FarmerApp.Api.Models;

public class Warehouse 
{
    public int Id { get; set; }

    [Required] public Product Product { get; set; }

    [Required] public int ProductId { get; set; }

    [Required] public float Quantity { get; set; }

    public bool TrySubtract(float quantity)
    {
        if (Quantity < quantity)
        {
            return false;
        }

        Quantity -= quantity;
        return true;
    }
}