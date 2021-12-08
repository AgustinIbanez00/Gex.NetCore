using System.ComponentModel.DataAnnotations;
namespace Gex.Models;
public class Comision : Auditory
{
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Nombre de la comisión.
    /// </summary>
    [MaxLength(100)]
    public string Nombre { get; set; }
    public int CicloLectivo { get; set; }
}

