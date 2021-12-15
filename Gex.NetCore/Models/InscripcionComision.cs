using System.ComponentModel.DataAnnotations;

namespace Gex.Models;
public class InscripcionComision
{
    [Key]
    public long Id { get; set; }
    public DateTime Fecha { get; set; }
    public Usuario Alumno { get; set; }
    public Comision Comision { get; set; }
}
