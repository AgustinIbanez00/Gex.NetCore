using System.ComponentModel.DataAnnotations;
using Gex.Models.Enums;
using Gex.Validation.Attributes;

namespace Gex.ViewModels.Request;
public class InscripcionMesaRequest
{
    [Required, Display(Name = "identificador")]
    public long Id { get; set; }

    [Required, Display(Name = "condición")]
    public InscripcionCondicion Condicion { get; set; }

    [Required, Display(Name = "alumno"), GreaterThanZero]
    public int UsuarioId { get; set; }

    [Required, Display(Name = "exámen"), GreaterThanZero]
    public long MesaExamenId { get; set; }
}
