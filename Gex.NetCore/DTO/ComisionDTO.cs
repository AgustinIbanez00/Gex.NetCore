using System.ComponentModel.DataAnnotations;
using Gex.NetCore.Validation.Attributes;

namespace Gex.NetCore.DTO;
public class ComisionDTO
{
    [Required]
    [Display(Name = "Identificador")]
    public int Id { get; set; }

    [Required]
    [Display(Name = "comisión")]
    [StringLength(50, MinimumLength = 5)]
    public string Nombre { get; set; }

    [Required]
    [Display(Name = "ciclo lectivo")]
    //[Range(2010, 2050)]
    [Date]
    public int CicloLectivo { get; set; }
}
