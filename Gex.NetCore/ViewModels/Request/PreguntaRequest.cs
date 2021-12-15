using System.ComponentModel.DataAnnotations;
using Gex.Models.Enums;

namespace Gex.ViewModels.Request;
public class PreguntaRequest
{
    [Required, Display(Name = "identificador")]
    public long Id { get; set; }

    [Required, Display(Name = "tema"), StringLength(255)]
    public string Tema { get; set; }

    [Required, Display(Name = "descripción")]
    public string Descripcion { get; set; }

    [Required, Display(Name = "tipo")]
    public PreguntaTipo Tipo { get; set; }

    [Display(Name = "materia")]
    public long? MateriaId { get; set; }
    [Display(Name = "exámen")]
    public long? ExamenId { get; set; }

}
