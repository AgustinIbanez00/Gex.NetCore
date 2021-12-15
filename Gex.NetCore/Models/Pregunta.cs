using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gex.Attributes;
using Gex.Models.Enums;
using Humanizer;

namespace Gex.Models;
[GexDescription("pregunta", GrammaticalGender.Feminine)]
public partial class Pregunta : Auditory
{
    [Key]
    public long Id { get; set; }
    public string Tema { get; set; }
    public string Descripcion { get; set; }
    public PreguntaTipo Tipo { get; set; }
    public virtual ICollection<Respuesta> Respuestas { get; set; }

    //[ForeignKey(nameof(Examen))]
    public long? ExamenId { get; set; }
    //[ForeignKey(nameof(Materia))]
    public long? MateriaId { get; set; }

}
