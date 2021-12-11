using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gex.Attributes;
using Gex.Models.Enums;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace Gex.Models;
[Index(nameof(Nombre), IsUnique = true)]
[GexDescription("materia", GrammaticalGender.Feminine)]
public partial class Materia : Auditory
{
    [Key]
    public long Id { get; set; }
    [Required, MaxLength(255)]
    public string Nombre { get; set; }
    public MateriaTipo Tipo { get; set; }
    public virtual ICollection<Pregunta> Preguntas { get; set; }
}
