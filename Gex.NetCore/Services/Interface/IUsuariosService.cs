using System.Threading.Tasks;
using Gex.Utils;
using Gex.ViewModels.Request;
using Gex.ViewModels.Response;

namespace Gex.Services.Interface;
public interface IUsuariosService
{ 
    Task<GexResult<UsuarioResponse>> GetUsuarioAsync(int id);
    Task<GexResult<UsuarioResponse>> GetUsuarioByEmail(string email);
    Task<GexResult<UsuarioResponse>> GetUsuarioByUserName(string userName);
    Task<GexResult<UsuarioResponse>> CreateUsuarioAsync(UsuarioRequest usuario);
}
