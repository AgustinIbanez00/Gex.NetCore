using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Gex.NetCore.Models;

[Index(nameof(Nombre), IsUnique = true)]
public class Comision : Auditory
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Nombre { get; set; }

    public int CicloLectivo { get; set; }
}
