using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gex.NetCore.Validation.Attributes
{
    public class DateAttribute : RangeAttribute
    {
     
        
        public DateAttribute()
          : base(typeof(int), DateTime.Now.AddYears(-20).Year.ToString(), DateTime.Now.AddYears(1).Year.ToString()) { }

    }
}
