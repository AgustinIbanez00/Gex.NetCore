using System.ComponentModel.DataAnnotations;

namespace Gex.NetCore.ViewModels;
public class ComisionDTO
{
    [Required]
    [Display(Name = "Identificador")]
    public int Id { get; set; }

    [Required]
    [Display(Name = "comisión")]
    [StringLength(100)]
    public string Nombre { get; set; }

    [Required]
    [Display(Name = "ciclo lectivo")]
    [Range(2010, 2050)]
    public int CicloLectivo { get; set; }
}
