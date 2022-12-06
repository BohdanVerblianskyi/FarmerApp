using System.ComponentModel.DataAnnotations;

namespace FarmerApp.Api.Validations;

public class NotZeroAndNotLessZeroAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is float and (0 or < 0))
        {
            ErrorMessage = "Incorrect inout";
            return false;
        }

        return true;
    }
}