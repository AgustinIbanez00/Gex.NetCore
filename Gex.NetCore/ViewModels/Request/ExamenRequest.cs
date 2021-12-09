using System.ComponentModel.DataAnnotations;
using Gex.Models;
using Gex.Validation.Attributes;

namespace Gex.ViewModels.Request;
public partial class ExamenRequest
{
    [Required, Display(Name = "identificador")]
    public long Id { get; set; }
    [Required, Display(Name = "tipo de exámen")]
    public virtual ExamenTipo Tipo { get; set; }

    [Required, Display(Name = "nota regular"), Range(0, 10)]
    public int NotaRegular { get; set; }

    [Required, Display(Name = "nota de promoción"), Range(0, 10)]
    public int NotaPromo { get; set; }

    [Display(Name = "recuperatorio")]
    public bool Recuperatorio { get; set; } = false;

    [Required, Display(Name = "materia"), GreaterThanZero]
    public long MateriaId { get; set; }
}

