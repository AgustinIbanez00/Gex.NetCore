using System.ComponentModel.DataAnnotations;

namespace Gex.NetCore.ViewModels
{
    public class CredentialsViewModel
    {
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
