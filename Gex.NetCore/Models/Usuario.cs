using Microsoft.AspNetCore.Identity;

namespace Gex.Models;
public enum UsuarioTipo
{
    Alumno,
    Profesor,
    Administrador
}
public partial class Usuario : IdentityUser
{
    public DateTimeOffset? EmailVerifiedAt { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public string RememberToken { get; set; }
    public string ProfilePhotoPath { get; set; }
    public long? Dni { get; set; }
    public string LastName { get; set; }
    public UsuarioTipo Tipo { get; set; }
}