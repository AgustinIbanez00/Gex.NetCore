using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace Gex.Helpers;
public enum Gender
{
    MALE,
    FEMALE
}
public enum GexErrorMessage
{
    [Description("Se produjo un error interno.")]
    Generic,
    [Description("No existe ninguna cuenta asociada con ese correo electrónico.")]
    InvalidEmail,
    [Description("La contraseña ingresada es incorrecta.")]
    InvalidPassword,
    [Description("El identificador es inválido.")]
    InvalidId,
    [Description("No se puede crear {Gender:un|una} {Entity} con el mismo identificador.")]
    DuplicatedEntity,
    [Description("No se encontró {Gender:ningún|ninguna} {Entity}.")]
    NotFound,
    [Description("Existe {Gender:un|una} {Entity} con esos datos.")]
    AlreadyExists,
    [Description("{Gender:El|La} {Entity} ya se encuentra {Gender:eliminado|eliminada}.")]
    AlreadyDeleted,
    [Description("No se pudo crear {Gender:ese|esa} {Entity}.")]
    CouldNotCreate,
    [Description("No se pudo eliminar {Gender:ese|esa} {Entity}.")]
    CouldNotDelete,
    [Description("No se pudo modificar {Gender:ese|esa} {Entity}.")]
    CouldNotUpdate
}
public enum GexSuccessMessage
{
    [Description("{Gender:El|La} {Entity} se creó correctamente.")]
    Created,
    [Description("{Gender:El|La} {Entity} se eliminó correctamente.")]
    Deleted,
    [Description("{Gender:El|La} {Entity} se actualizó correctamente.")]
    Modified
}
public class GexResponseOptions
{
    public string Entity { get; set; }
    public Gender Gender { get; set; }

}
public static class ResponseHelper
{
    public static int GetHttpError(GexErrorMessage gexError)
    {
        return gexError switch
        {
            GexErrorMessage.NotFound => StatusCodes.Status404NotFound,
            GexErrorMessage.Generic or GexErrorMessage.CouldNotDelete or GexErrorMessage.CouldNotDelete or GexErrorMessage.CouldNotUpdate => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }
}
