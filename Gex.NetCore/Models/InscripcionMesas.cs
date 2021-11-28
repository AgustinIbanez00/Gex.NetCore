using System.ComponentModel.DataAnnotations;

namespace Gex.NetCore.Models;
public enum Condicion
{
    Regular,
    Libre,
    Equivalencia
}

public partial class InscripcionMesas
{
    [Key]
    public long Id { get; set; }

    /// <summary>
    /// Si la nota es menor a 0 signifca que el alumno no se presentó.
    /// </summary>
    [Range(-1, 10)]
    public int Nota { get; set; }
    public Condicion Condicion { get; set; }
    public virtual Usuario Alumno { get; set; }
    public virtual MesaExamen MesaExamen { get; set; }
}
