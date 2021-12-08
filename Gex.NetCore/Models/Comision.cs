using System.ComponentModel.DataAnnotations;
using Gex.Helpers;

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

    public static GexResponseOptions Options => new GexResponseOptions()
    {
        Entity = "comisión",
        Gender = Gender.FEMALE
    };
}

