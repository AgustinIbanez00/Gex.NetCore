using System.ComponentModel.DataAnnotations;
using Gex.Models.Enums;
using Gex.Validation.Attributes;

namespace Gex.ViewModels.Request;
public class RespuestaCreateRequest
{
    /// <summary>
    /// Pregunta a la que estará relacionada la respuesta.
    /// </summary>
    [Required, Display(Name = "pregunta")]
    public long PreguntaId { get; set; }

    public RespuestaBorrableRequest[] Respuestas { get; set; }
}

public class RespuestaBorrableRequest
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
    [Display(Name = "correcto")]
    public bool Correcto { get; set; }

    [Display(Name = "estado")]
    public bool Borrar { get; set; }
}
