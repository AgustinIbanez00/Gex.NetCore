using System.Threading.Tasks;
using Gex.Repository.Interface;
using Gex.Services.Interface;
using Gex.Utils;
using Gex.ViewModels.Response;

namespace Gex.Services;
public class UsuarioService : IUsuariosService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }


    public Task<GexResult<UsuarioResponse>> GetUsuarioAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<GexResult<UsuarioResponse>> GetUsuarioByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<GexResult<UsuarioResponse>> GetUsuarioByUserName(string userName)
    {
        throw new NotImplementedException();
    }
}
