using System.ComponentModel.DataAnnotations;
using Gex.Models;

namespace Gex.ViewModels.Request;
public class PreguntaRequest
{
    [Required, Display(Name = "identificador")]
    public long Id { get; set; }
    [Required, Display(Name = "periodo"), StringLength(255)]
    public string Periodo { get; set; }
    [Required, Display(Name = "descripción")]
    public string Descripcion { get; set; }
    [Required, Display(Name = "tipo")]
    public PreguntaTipo Tipo { get; set; }
}
