using System.ComponentModel.DataAnnotations;

namespace Gex.ViewModels.Request;
public class RespuestaRequest
{
    public long Id { get; set; }
    /// <summary>
    /// Valor de la respuesta.
    /// </summary>
    [Required, Display(Name = "valor")]
    public string Valor { get; set; }

    /// <summary>
    /// Indica si la respuesta es verdadera.
    /// </summary>
    [Required, Display(Name = "correcto")]
    public bool Correcto { get; set; }
    /// <summary>
    /// Pregunta a la que estará relacionada la respuesta.
    /// </summary>
    [Required, Display(Name = "pregunta")]
    public long PreguntaId { get; set; }

}
