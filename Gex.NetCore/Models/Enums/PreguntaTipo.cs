namespace Gex.Models.Enums;
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
    MULTIPLECHOICE,
    /// <summary>
    /// Indica que se puede responder con muchas respuestas.
    /// </summary>
    MULTIPLESELECTION
}
