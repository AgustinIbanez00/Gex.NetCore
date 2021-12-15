using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gex.Attributes;
using Gex.Models.Enums;
using Humanizer;

namespace Gex.Models;

[GexDescription("exámen", GrammaticalGender.Masculine)]
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
}
