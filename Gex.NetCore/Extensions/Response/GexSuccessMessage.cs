using System.ComponentModel;

namespace Gex.Extensions.Response;

public enum GexSuccessMessage
{
    [Description("{Gender:El|La} {Entity} se creó correctamente.")]
    Created,
    [Description("{Gender:El|La} {Entity} se eliminó correctamente.")]
    Deleted,
    [Description("{Gender:El|La} {Entity} se actualizó correctamente.")]
    Modified,
    [Description("No se encontraron datos.")]
    NoDataFound
}
