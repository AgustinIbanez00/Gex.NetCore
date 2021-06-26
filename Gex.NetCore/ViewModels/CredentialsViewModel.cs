
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
