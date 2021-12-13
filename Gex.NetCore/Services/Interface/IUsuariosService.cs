using System.Threading.Tasks;
using Gex.Utils;
using Gex.ViewModels.Response;

namespace Gex.Services.Interface;

public interface IUsuariosService
{ 
    public Task<GexResult<UsuarioResponse>> GetUsuarioAsync(int id);
    public Task<GexResult<UsuarioResponse>> GetUsuarioByEmail(string email);
    public Task<GexResult<UsuarioResponse>> GetUsuarioByUserName(string userName);

}
