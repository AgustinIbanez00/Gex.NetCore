using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Gex.Helpers;

namespace Gex.Models;
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

public partial class Examen : Auditory
{
    [Key]
    public long Id { get; set; }

    public virtual Materia Materia { get; set; }
    public virtual ExamenTipo Tipo { get; set; }
    [Range(0, 10)]
    public int NotaRegular { get; set; }
    [Range(0, 10)]
    public int NotaPromo { get; set; }
    public bool Recuperatorio { get; set; }
    public virtual ICollection<Pregunta> Preguntas { get; set; }

    public static GexResponseOptions Options => new GexResponseOptions()
    {
        Entity = "exámen",
        Gender = Gender.MALE
    };
}
