using System.ComponentModel.DataAnnotations;

namespace Gex.Validation.Attributes
{
    public class GreaterThanZeroAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if(int.TryParse(value.ToString(), out int intParsed))
                return intParsed > 0;
            if (long.TryParse(value.ToString(), out long longParsed))
                return longParsed > 0;
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("El valor de {0} debe ser mayor a cero.", name);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return base.IsValid(value, validationContext);
        }
    }
}
