using System.ComponentModel.DataAnnotations;

namespace Gex.Models;
public partial class RespuestaAlumno
{
    [Key]
    public long Id { get; set; }
    public virtual Usuario Alumno { get; set; }
    public virtual MesaExamen Examen { get; set; }
    public virtual Respuesta Respuesta { get; set; }
    /// <summary>
    /// Valor que ingresó el alumno.
    /// </summary>
    public string Valor { get; set; }
    public bool? Correcto { get; set; }
}
