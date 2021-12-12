namespace Gex.Models.Enums;
public enum Estado
{
    /// <summary>
    /// Cuando una entidad se encuentra en este estado no se podrá listar, modificar ni eliminar.
    /// </summary>
    BAJA = 0,
    /// <summary>
    /// El objeto se encuentra en su estado default.
    /// </summary>
    NORMAL = 1,
    /// <summary>
    /// Características similares al estado de BAJA.
    /// </summary>
    SUSPENDIDO = -1
}
