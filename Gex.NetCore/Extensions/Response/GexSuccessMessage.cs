using System.ComponentModel;

namespace Gex.Extensions.Response;

public enum GexSuccessMessage
{
    [Description("{Entity} {Gender:creado|creada} correctamente.")]
    Created,
    [Description("{Entity} {Gender:eliminado|eliminada} correctamente.")]
    Deleted,
    [Description("{Entity} {Gender:actualizado|actualizada} correctamente.")]
    Modified
}
