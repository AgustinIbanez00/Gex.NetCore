using Microsoft.AspNetCore.Identity;

namespace Gex.NetCore.Models;
public partial class Usuario : IdentityUser
{
    public DateTimeOffset? EmailVerifiedAt { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public string RememberToken { get; set; }
    public string ProfilePhotoPath { get; set; }
    public long? Dni { get; set; }
    public string LastName { get; set; }
    public int? Type { get; set; }
    public Estado State { get; set; }
}