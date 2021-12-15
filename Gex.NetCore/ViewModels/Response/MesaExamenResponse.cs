namespace Gex.ViewModels.Response;

public enum InscripcionEstado
{
    Ninguno,
    Inscripto,
    ParaRendir,
    Rendida,
    Ausente
}


public class MesaExamenResponse
{
    public long Id { get; set; }
    public DateTime? Fecha { get; set; }
    public bool MostrarRespuestas { get; set; }
    public int Duracion { get; set; }
    public ExamenResponse Examen { get; set; }
    public UsuarioResponse Profesor { get; set; }

    public InscripcionEstado Estado { get; set; }
}
