using System.ComponentModel.DataAnnotations;

namespace Gex.NetCore.DTO;
public class RegistrationDTO
{
    [Display(Name = "correo electrónico")]
    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Display(Name = "contraseña")]
    [DataType(DataType.Password)]
    [StringLength(8, MinimumLength = 6)]
    [Required]
    public string Password { get; set; }

    [Display(Name = "confirmar contraseña")]
    [DataType(DataType.Password)]
    [StringLength(8, MinimumLength = 6)]
    [Compare("Password")]
    [Required]
    public string ConfirmPassword { get; set; }

    [Display(Name = "nombre")]
    [Required]
    [StringLength(100)]
    public string FirstName { get; set; }

    [Display(Name = "apellido")]
    [Required]
    [StringLength(100)]
    public string LastName { get; set; }

    [Display(Name = "ubicación")]
    [Required]
    [StringLength(150)]
    public string Location { get; set; }
}
