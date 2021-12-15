namespace Gex.Models;
public class Cursada
{
    public int Id { get; set; }
    public Comision Comision { get; set; }
    public Materia Materia { get; set; }
    public DateTime? Fecha { get; set; }
}
