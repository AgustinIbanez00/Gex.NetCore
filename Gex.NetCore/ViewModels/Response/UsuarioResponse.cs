using System.ComponentModel.DataAnnotations;
using Gex.Models.Enums;

namespace Gex.ViewModels.Response;

public class UsuarioResponse
{
    public int Id { get; set; }
    public string UserName { get; set; }

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    public UsuarioTipo Tipo { get; set; }
}
