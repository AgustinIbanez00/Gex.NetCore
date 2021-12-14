using System.ComponentModel.DataAnnotations;
using Gex.Models.Enums;

namespace Gex.ViewModels.Request;
public class RegistroRequest
{
    [Required, Display(Name = "nombre de usuario"), StringLength(100, MinimumLength = 3), RegularExpression("^[A-Za-z][A-Za-z0-9_]{7,29}$")]
    public string UserName { get; set; }

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

    [Display(Name = "ubicación"), StringLength(150)]
    public string Location { get; set; }
    [Required, Display(Name = "dni"), StringLength(150)]
    public string Dni { get; set; }

    [Display(Name = "rol")]
    public UsuarioTipo Tipo { get; set; } = UsuarioTipo.Alumno;

    [Required, Display(Name = "fecha de nacimiento"), DataType(DataType.Date)]
    public DateTime FechaNacimiento { get; set; }
}
