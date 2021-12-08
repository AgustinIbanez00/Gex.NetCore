using System.ComponentModel.DataAnnotations;
using Gex.Models;

namespace Gex.DTO;
public class MateriaDTO
{
    [Display(Name = "identificador")]
    public long Id { get; set; }

    [Display(Name = "nombre")]
    public string Nombre { get; set; }

    [Display(Name = "tipo")]
    public MateriaTipo Tipo { get; set; }
}
