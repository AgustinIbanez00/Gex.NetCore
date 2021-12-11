using System.ComponentModel.DataAnnotations;
using Gex.Models.Enums;

namespace Gex.Models;
public partial class InscripcionMesas
{
    [Key]
    public long Id { get; set; }

    /// <summary>
    /// Si la nota es menor a 0 signifca que el alumno no se presentó.
    /// </summary>
    [Range(-1, 10)]
    public int Nota { get; set; }
    public InscripcionCondicion Condicion { get; set; }
    public virtual Usuario Alumno { get; set; }
    public virtual MesaExamen MesaExamen { get; set; }
}
