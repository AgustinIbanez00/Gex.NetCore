using System.Collections.Generic;
using Gex.Models.Enums;

namespace Gex.ViewModels.Response;
public class PreguntaResponse
{
    public long Id { get; set; }
    public string Tema { get; set; }
    public string Descripcion { get; set; }
    public PreguntaTipo Tipo { get; set; }
    public long? ExamenId { get; set; }
    public long? MateriaId { get; set; }
}
