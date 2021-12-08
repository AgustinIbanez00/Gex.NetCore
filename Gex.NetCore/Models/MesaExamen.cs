using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gex.Models;
public partial class MesaExamen
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
    public virtual Examen Examen { get; set; }
    public virtual Usuario Profesor { get; set; }
}
