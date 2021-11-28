namespace Gex.NetCore.Models;
public class Auditory
{
    //public Usuario LastUpdateBy { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    //public Usuario CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public Estado Estado { get; set; } = Estado.NORMAL;
}
