namespace Gex.Models.Enums;
public enum PreguntaTipo
{
    /// <summary>
    /// Debe realizar una justificación de la respuesta.
    /// </summary>
    TEXTO = 0,
    /// <summary>
    /// Sólo se puede contestar con sí o no.
    /// </summary>
    CERRADA = 1,
    /// <summary>
    /// Se podrán responder con múltiples valores.
    /// </summary>
    MULTIPLECHOICE = 3,
    /// <summary>
    /// Indica que se puede responder con muchas respuestas.
    /// </summary>
    MULTIPLESELECTION = 4,
}
