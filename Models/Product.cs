using System.ComponentModel.DataAnnotations;

namespace FarmerApp.Api.Models;

public class Product 
{
    public int Id { get; set; }

    [Required, MaxLength(50)] public string Name { get; set; }

    [Required] public int ProductTypeId { get; set; }

    [Required] public ProductType ProductType { get; set; }

    [Required] public int MeasurementUnitId { get; set; }

    [Required] public MeasurementUnit MeasurementUnit { get; set; }

    [Required] public float Price { get; set; }

    public List<WithdrawalFromWarehouse> WithdrawalFromWarehouses { get; set; }

    public List<WarehouseReception> WarehouseReceptions { get; set; }

    public List<Warehouse> Warehousess { get; set; }


    public float GetPrice(float quantity)
    {
        return (float)Math.Round(Price * quantity);
    }
}