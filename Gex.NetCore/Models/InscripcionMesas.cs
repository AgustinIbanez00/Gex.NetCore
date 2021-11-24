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

    [Range(0, 10)]
    public int Nota { get; set; }

    public Condicion Condicion { get; set; }

    public virtual Usuario Alumno { get; set; }
    public virtual MesaExamen Mesa { get; set; }
}
