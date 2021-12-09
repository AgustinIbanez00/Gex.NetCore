using System.Collections.Generic;
using Gex.Models;

namespace Gex.ViewModels.Response;
public class PreguntaResponse
{
    public long Id { get; set; }
    public string Periodo { get; set; }
    public string Descripcion { get; set; }
    public PreguntaTipo Tipo { get; set; }
    public virtual ICollection<RespuestaResponse> Respuestas { get; set; }
}
