namespace FarmerApp.Api.DTO;

public class WarehouseDto
{
    public string ProductName { get; set; }

    public string MeasurementUnitName { get; set; }

    public float Quantity { get; set; }

    public float Price { get; set; } 
    
    public float PriceByUnit { get; set; }
}