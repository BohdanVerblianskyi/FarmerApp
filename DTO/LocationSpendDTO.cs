namespace FarmerApp.Api.DTO;

public class LocationSpendDto
{
    public IEnumerable<SpendDto> Spendings { get; set; }

    public float Sum { get; set; }
}