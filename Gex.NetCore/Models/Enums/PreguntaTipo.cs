using System.ComponentModel;

namespace Gex.Models.Enums;
public enum PreguntaTipo
{
    /// <summary>
    /// Debe realizar una justificación de la respuesta.
    /// </summary>
    [Description("Texto")]
    Texto = 0,
    /// <summary>
    /// Sólo se puede contestar con sí o no.
    /// </summary>
    [Description("Verdadero o Falso")]
    VerdaderoOFalso = 1,
    /// <summary>
    /// Existirán varias respuestas con una correcta.
    /// </summary>
    [Description("Multiple Choise")]
    MultipleChoise = 2,
    /// <summary>
    /// Indica que pueden existir muchas respuestas correctas.
    /// </summary>
    [Description("Selección Múltiple")]
    SeleccionMultiple = 3,
}
