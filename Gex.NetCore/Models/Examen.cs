using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gex.NetCore.Models;
public enum ExamenTipo
{
    [Description("Final")]
    FINAL,
    [Description("Parcial")]
    PARCIAL,
    [Description("Global")]
    GLOBAL,
    TEST
}

public partial class Examen
{
    [Key]
    public long Id { get; set; }
    public virtual Materia Materia { get; set; }
    public virtual ExamenTipo Tipo { get; set; }
    public DateTime? FechaCreacion { get; set; }
    [Range(0,10)]
    public int NotaRegular { get; set; }
    [Range(0, 10)]
    public int NotaPromo { get; set; }
    public bool Recuperatorio { get; set; }
}
