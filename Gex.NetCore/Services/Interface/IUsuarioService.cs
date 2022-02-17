using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Models.Enums;
using Gex.Utils;
using Gex.ViewModels.Request;
using Gex.ViewModels.Response;

namespace Gex.Services.Interface;
public interface IUsuarioService
{
    Task<GexResult<ICollection<UsuarioResponse>>> GetUsuariosAsync();
    Task<GexResult<ICollection<UsuarioResponse>>> GetUsuariosAsync(string email);
    Task<GexResult<ICollection<UsuarioResponse>>> GetUsuariosByTipoAsync(UsuarioTipo tipo);
    Task<GexResult<UsuarioResponse>> GetUsuarioAsync(int id);
    Task<GexResult<UsuarioResponse>> GetUsuarioByEmailAsync(string email);
    Task<GexResult<UsuarioResponse>> GetUsuarioByUserNameAsync(string userName);
    Task<GexResult<UsuarioResponse>> CreateUsuarioAsync(RegistroRequest request, string secret);
    Task<GexResult<UsuarioResponse>> LockUsuarioAsync(int id);
    Task<GexResult<LoginResponse>> LoginUsuarioAsync(LoginRequest request);
}
