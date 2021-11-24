using System.ComponentModel.DataAnnotations;

namespace Gex.NetCore.Models;
public class Tema
{
    [Key]
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public Materia Materia { get; set; }
}
