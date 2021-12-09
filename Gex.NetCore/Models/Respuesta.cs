using System.ComponentModel.DataAnnotations;

namespace Gex.Models;
public partial class Respuesta : Auditory
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
}
