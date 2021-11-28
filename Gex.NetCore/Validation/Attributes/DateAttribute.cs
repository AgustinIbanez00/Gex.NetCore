using System.ComponentModel.DataAnnotations;

namespace Gex.NetCore.Validation.Attributes;
public class DateAttribute : RangeAttribute
{
    public DateAttribute()
      : base(typeof(int), DateTime.Now.AddYears(-20).Year.ToString(), DateTime.Now.AddYears(1).Year.ToString()) { }

    public override string FormatErrorMessage(string name)
    {
        return String.Format("El campo {0} debe estar entre {1} y {2}.", name, Minimum, Maximum);
    }
}
