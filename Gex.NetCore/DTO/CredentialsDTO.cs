using System.ComponentModel.DataAnnotations;

namespace Gex.NetCore.DTO
{
    public class CredentialsDTO
    {
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
