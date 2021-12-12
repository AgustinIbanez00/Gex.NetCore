using Gex.Models.Enums;
using Gex.Validation.Attributes;

namespace Gex.ViewModels.Request;
public class RespuestaCreateRequest
{
    public PreguntaTipo Tipo { get; set; }
    public string[] Respuestas { get; set; }
    public int[] RespuestasCorrectas { get; set; }

    [GreaterThanZero]
    public int RespuestaCorrecta { get; set; }

    
    public long PreguntaId { get; set; }
}
