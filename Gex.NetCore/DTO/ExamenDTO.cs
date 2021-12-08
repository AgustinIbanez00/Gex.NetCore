using System.ComponentModel.DataAnnotations;
using Gex.Models;

namespace Gex.DTO;
public partial class ExamenDTO
{
    [Required]
    [Display(Name = "identificador")]
    public long Id { get; set; }

    [Required]
    [Display(Name = "tipo de exámen")]
    public virtual ExamenTipo Tipo { get; set; }

    [Required]
    [Range(0, 10)]
    [Display(Name = "nota regular")]
    public int NotaRegular { get; set; }

    [Required]
    [Range(0, 10)]
    [Display(Name = "nota de promoción")]
    public int NotaPromo { get; set; }

    [Display(Name = "recuperatorio")]
    public bool Recuperatorio { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    [Display(Name = "materia")]
    public int? MateriaId { get; set; }
}

