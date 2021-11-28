using System.ComponentModel.DataAnnotations;

namespace Gex.NetCore.Models;
public partial class RespuestaAlumno
{
    [Key]
    public long Id { get; set; }
    public virtual Usuario Alumno { get; set; }
    public virtual Examen Examen { get; set; }
    public virtual Respuesta Respuesta { get; set; }
    /// <summary>
    /// Valor que ingresó el alumno.
    /// </summary>
    public string Valor { get; set; }
    public bool? Correcto { get; set; }
}
