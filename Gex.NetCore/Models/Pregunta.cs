using System.ComponentModel.DataAnnotations;

namespace Gex.NetCore.Models;

public enum PreguntaTipo
{
    /// <summary>
    /// Sólo se puede contestar con sí o no.
    /// </summary>
    CERRADA,
    /// <summary>
    /// Debe realizar una justificación de la respuesta.
    /// </summary>
    ABIERTA
}
public partial class Pregunta
{
    [Key]
    public long Id { get; set; }
    public Tema Tema { get; set; }
    public string Descripcion { get; set; }
    public PreguntaTipo Tipo { get; set; }
}
