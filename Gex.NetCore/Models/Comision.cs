using System.ComponentModel.DataAnnotations;
using Gex.Attributes;
using Humanizer;

namespace Gex.Models;
[GexDescription("comisión", GrammaticalGender.Feminine)]
public class Comision : Auditory
{
    [Key]
    public long Id { get; set; }

    /// <summary>
    /// Nombre de la comisión.
    /// </summary>
    [MaxLength(100)]
    public string Nombre { get; set; }
    public int CicloLectivo { get; set; }
}

