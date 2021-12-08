using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gex.Models;

public enum PreguntaTipo
{
    /// <summary>
    /// Sólo se puede contestar con sí o no.
    /// </summary>
    CERRADA,
    /// <summary>
    /// Debe realizar una justificación de la respuesta.
    /// </summary>
    TEXTO,
    /// <summary>
    /// Se podrán responder con múltiples valores.
    /// </summary>
    MULTIPLECHOICE
}
public partial class Pregunta
{
    [Key]
    public long Id { get; set; }
    public string Periodo { get; set; }
    public string Descripcion { get; set; }
    public PreguntaTipo Tipo { get; set; }
    public virtual ICollection<Respuesta> Respuestas { get; set; }
}
