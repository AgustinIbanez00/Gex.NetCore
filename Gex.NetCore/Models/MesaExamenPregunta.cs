using System.ComponentModel.DataAnnotations;

namespace Gex.NetCore.Models;
public class MesaExamenPregunta
{
    [Key]
    public long Id { get; set; }
    public MesaExamen MesaExamen { get; set; }
    public Pregunta Pregunta { get; set; }
    public int Puntos { get; set; }
}
