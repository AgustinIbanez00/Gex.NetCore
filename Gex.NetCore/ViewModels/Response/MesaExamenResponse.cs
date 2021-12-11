namespace Gex.ViewModels.Response;
public class MesaExamenResponse
{
    public long Id { get; set; }
    public DateTime? Fecha { get; set; }
    public bool MostrarRespuestas { get; set; }
    public int Duracion { get; set; }
    public long ExamenId { get; set; }
    public int ProfesorId { get; set; }
}
