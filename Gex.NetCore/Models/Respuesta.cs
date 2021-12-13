using System.ComponentModel.DataAnnotations;
using Gex.Attributes;

namespace Gex.Models;
[GexDescription("respuesta", Humanizer.GrammaticalGender.Feminine)]
public partial class Respuesta
{
    [Key]
    public long Id { get; set; }
    //public virtual Pregunta Pregunta { get; set; }
    /// <summary>
    /// Valor de la respuesta.
    /// </summary>
    public string Valor { get; set; }

    /// <summary>
    /// Indica si la respuesta es verdadera.
    /// </summary>
    public bool? Correcto { get; set; }

    public long PreguntaId { get; set; }
}
