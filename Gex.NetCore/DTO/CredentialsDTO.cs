using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gex.NetCore.DTO;
public class CredentialsDTO
{
    [Required]
    [EmailAddress]
    [Description("Dirección de correo eletrónico válida. Por ejemplo: user@example.com")]
    [Display(Name = "correo electrónico")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Description("Contraseña")]
    public string Password { get; set; }
}
