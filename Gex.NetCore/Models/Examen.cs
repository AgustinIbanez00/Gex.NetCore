using System.ComponentModel.DataAnnotations;
using MySql.Data.Types;

namespace Gex.NetCore.Models;
public partial class Examen
{
    [Key]
    public long Id { get; set; }
    public virtual Materia Materia { get; set; }
    public virtual TipoExamen Tipo { get; set; }
    public virtual ModalidadExamen Modalidad { get; set; }
    public DateTime? FechaCreacion { get; set; }    
    [MaxLength(255)]
    public string Descripcion { get; set; }
    [Range(0,10)]
    public int NotaRegular { get; set; }
    [Range(0, 10)]
    public int NotaPromo { get; set; }
    public int Duracion { get; set; }
}
