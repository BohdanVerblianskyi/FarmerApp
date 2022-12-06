using System.ComponentModel.DataAnnotations;

namespace FarmerApp.Api.Validations;

public class NumberOnlyAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is string line)
        {
            if (!line.All(char.IsNumber))
            {
                ErrorMessage = "the must is a number";
                return false;
            }

            return true;
        }

        return true;
    }
}