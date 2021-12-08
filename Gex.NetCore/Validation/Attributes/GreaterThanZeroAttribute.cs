using System.ComponentModel.DataAnnotations;

namespace Gex.Validation.Attributes
{
    public class GreaterThanZeroAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var x = (int)value;
            return x > 0;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("El valor de {0} debe ser mayor a cero.", name);
        }
    }
}
