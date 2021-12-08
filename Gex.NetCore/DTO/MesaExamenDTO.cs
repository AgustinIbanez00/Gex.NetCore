using System.ComponentModel.DataAnnotations;

namespace Gex.DTO;

public class MesaExamenDTO
{
    [Required]
    [Display(Name = "identificador")]
    public long Id { get; set; }
    [Display(Name = "fecha")]
    public DateTime? Fecha { get; set; }

    [Display(Name = "mostrar respuestas")]
    public bool MostrarRespuestas { get; set; }
    [Display(Name = "duración")]
    public int Duracion { get; set; }
    [Required]
    [Display(Name = "exámen")]
    public long ExamenId { get; set; }
    [Display(Name = "profesor")]
    public string ProfesorId { get; set; }
}

