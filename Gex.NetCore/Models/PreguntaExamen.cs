using System.ComponentModel.DataAnnotations;

namespace Gex.NetCore.Models;
public class PreguntaExamen
{
    [Key]
    public long Id { get; set; }
    public Examen Examen { get; set; }
    public Pregunta Pregunta { get; set; }
}
