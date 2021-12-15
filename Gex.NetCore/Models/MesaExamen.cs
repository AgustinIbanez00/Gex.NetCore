using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Gex.Attributes;
using Humanizer;

namespace Gex.Models;
[GexDescription("mesa de exámen", GrammaticalGender.Feminine)]
public partial class MesaExamen : Auditory
{
    [Key]
    public long Id { get; set; }

    /// <summary>
    /// Fecha en la que se rendirá el exámen.
    /// </summary>
    public DateTime? Fecha { get; set; }

    /// <summary>
    /// Cuando le profesor corrigió todos los exámenes este valor cambiará a True.
    /// Cuando el valor sea False los alumnos no podrán ver las respuestas correctas del exámen.
    /// </summary>
    [DefaultValue("0")]
    public bool MostrarRespuestas { get; set; }
    /// <summary>
    /// Cantidad de tiempo que durará el exámen.
    /// </summary>
    public int Duracion { get; set; }

    /// <summary>
    /// Navégación de exámen.
    /// </summary>
    public Examen Examen { get; set; }

    /// <summary>
    /// Navegación de profesor.
    /// </summary>
    public Usuario Profesor { get; set; }
}
