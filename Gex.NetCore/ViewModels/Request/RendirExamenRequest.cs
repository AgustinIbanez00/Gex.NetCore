using System.ComponentModel.DataAnnotations;
using Gex.Validation.Attributes;

namespace Gex.ViewModels.Request;
public class RendirExamenRequest
{
    [Required, Display(Name = "mesa de exámen"), GreaterThanZero]
    public long MesaExamenId { get; set; }

    [Required, Display(Name = "respuestas")]
    public RendirPreguntasRequest[] Preguntas { get; set; }
}

public class RendirPreguntasRequest
{
    [Required, Display(Name = "pregunta"), GreaterThanZero]
    public long PreguntaId { get; set; }

    [Required, Display(Name = "respuestas")]
    public RendirRespuestaRequest[] Respuestas { get; set; }
}

public class RendirRespuestaRequest
{
    [Required, Display(Name = "respuesta"), GreaterThanZero]
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
}
