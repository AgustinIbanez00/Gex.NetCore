using Gex.Attributes;
using Gex.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace Gex.Models;
[GexDescription("usuario", Humanizer.GrammaticalGender.Masculine)]
public partial class Usuario : IdentityUser<int>
{
    public DateTimeOffset? EmailVerifiedAt { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public string RememberToken { get; set; }
    public string ProfilePhotoPath { get; set; }
    public string FirstName { get; set; }

    public Comision Comision { get; set; }
    public string LastName { get; set; }
    public UsuarioTipo Tipo { get; set; }
    public DateTime BirthDate { get; set; }
}