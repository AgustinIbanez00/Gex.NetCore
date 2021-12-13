using System.ComponentModel.DataAnnotations;

namespace Gex.ViewModels.Request;

public class UsuarioRequest
{
    public int Id { get; set; }
    public string UserName { get; set; }

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

}
