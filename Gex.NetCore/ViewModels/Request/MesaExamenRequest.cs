using System.ComponentModel.DataAnnotations;
using Gex.Validation.Attributes;

namespace Gex.ViewModels.Request;
public class MesaExamenRequest
{
    [Required, Display(Name = "identificador")]
    public long Id { get; set; }
    [Required, Display(Name = "fecha")]
    public DateTime? Fecha { get; set; }
    [Required, Display(Name = "mostrar respuestas")]
    public bool MostrarRespuestas { get; set; }
    [Display(Name = "duración"), Range(5, 10000)]
    public int Duracion { get; set; }
    [Required, Display(Name = "exámen"), GreaterThanZero]
    public long ExamenId { get; set; }
    [Display(Name = "profesor"), GreaterThanZero]
    public int ProfesorId { get; set; }
}

