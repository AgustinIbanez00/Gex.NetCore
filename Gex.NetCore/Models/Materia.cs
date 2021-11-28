using System.ComponentModel.DataAnnotations;

namespace Gex.NetCore.Models;
public enum MateriaTipo
{
    ANUAL,
    CUATRIMESTRAL,
    TRIMESTRAL
}
public partial class Materia
{
    [Key]
    public long Id { get; set; }
    public string Nombre { get; set; }
    public MateriaTipo Tipo { get; set; }
}
