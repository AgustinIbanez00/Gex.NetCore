using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gex.ViewModels.Request;
public class LoginRequest
{
    [Required, Display(Name = "correo electrónico"), Description("Dirección de correo eletrónico válida. Por ejemplo: user@example.com"), EmailAddress]
    public string Email { get; set; }

    [Required, Display(Name = "contraseña"), Description("Contraseña"), DataType(DataType.Password), StringLength(255, MinimumLength = 6)]
    public string Password { get; set; }
}
