namespace Gex.ViewModels.Response;
public class MesaExamenResponse
{
    public long Id { get; set; }
    public DateTime? Fecha { get; set; }
    public bool MostrarRespuestas { get; set; }
    public int Duracion { get; set; }
    public ExamenResponse Examen { get; set; }
    public UsuarioResponse Profesor { get; set; }
}
