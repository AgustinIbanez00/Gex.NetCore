using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gex.NetCore.Models;
public partial class MesaExamen
{
    [Key]
    public long Id { get; set; }
    public DateTime? Fecha { get; set; }

    [DefaultValue("0")]
    public bool MostrarRespuestas { get; set; }

    public virtual Examen Examen { get; set; }
    public virtual Usuario Profesor { get; set; }
}
