using System.ComponentModel.DataAnnotations;

namespace Gex.ViewModels.Request;
public class RegistroRequest
{
    [Required, Display(Name = "correo electrónico"), EmailAddress]    
    public string Email { get; set; }

    [Required, Display(Name = "contraseña"), DataType(DataType.Password), StringLength(255, MinimumLength = 6)]
    public string Password { get; set; }

    [Required, Display(Name = "confirmar contraseña"), DataType(DataType.Password), StringLength(255, MinimumLength = 6), Compare("Password")]
    public string ConfirmPassword { get; set; }

    [Required, Display(Name = "nombre"), StringLength(100)]
    public string FirstName { get; set; }

    [Required, Display(Name = "apellido"), StringLength(100)]
    public string LastName { get; set; }

    [Required, Display(Name = "ubicación"), StringLength(150)]
    public string Location { get; set; }
}
