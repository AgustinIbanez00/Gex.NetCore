using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Gex.NetCore.ViewModels.Validations;
using ServiceStack.FluentValidation.Attributes;

namespace Gex.NetCore.ViewModels
{
    [Validator(typeof(CredentialsViewModelValidator))]
    public class CredentialsViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
