using Gex.Models;

namespace Gex.ViewModels.Response;
public class MateriaResponse
{
    public long Id { get; set; }

    public string Nombre { get; set; }

    public MateriaTipo Tipo { get; set; }
}
