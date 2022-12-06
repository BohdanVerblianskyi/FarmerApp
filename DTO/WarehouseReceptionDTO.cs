namespace FarmerApp.Api.DTO;

public class WarehouseReceptionDto
{
    public int Id { get; set; }
    
    public string Invoice { get; set; }

    public string ProductName { get; set; }

    public string MeasurementUnitName { get; set; }

    public float Quantity { get; set; }

    public float Price { get; set; }

    public DateTime Date { get; set; }
}