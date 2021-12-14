using System.Threading.Tasks;
using AutoMapper;
using Gex.Extensions.Response;
using Gex.Models;
using Gex.Repository.Interface;
using Gex.Services.Interface;
using Gex.Utils;
using Gex.ViewModels.Request;
using Gex.ViewModels.Response;

namespace Gex.Services;

using static GexResponse;

public class UsuarioService : IUsuariosService
{
    private readonly IMapper _mapper;
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IMapper mapper, IUsuarioRepository usuarioRepository)
    {
        _mapper = mapper;
        _usuarioRepository = usuarioRepository;
    }

    public Task<GexResult<UsuarioResponse>> CreateUsuarioAsync(UsuarioRequest usuario)
    {
        throw new NotImplementedException();
    }

    public async Task<GexResult<UsuarioResponse>> GetUsuarioAsync(int id)
    {
        try
        {
            var usuario = await _usuarioRepository.GetUsuarioAsync(id);

            if (usuario == null)
                return Error<Usuario, UsuarioResponse>(GexErrorMessage.NotFound);

            return Ok(_mapper.Map<UsuarioResponse>(usuario));
        }
        catch (Exception ex)
        {
            return Error<Usuario, UsuarioResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<UsuarioResponse>> GetUsuarioByEmail(string email)
    {
        try
        {
            var usuario = await _usuarioRepository.GetUsuarioByEmailAsync(email);

            if (usuario == null)
                return Error<Usuario, UsuarioResponse>(GexErrorMessage.NotFound);

            return Ok(_mapper.Map<UsuarioResponse>(usuario));
        }
        catch (Exception ex)
        {
            return Error<Usuario, UsuarioResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<UsuarioResponse>> GetUsuarioByUserName(string userName)
    {
        try
        {
            var usuario = await _usuarioRepository.GetUsuarioByUserNameAsync(userName);

            if (usuario == null)
                return Error<Usuario, UsuarioResponse>(GexErrorMessage.NotFound);

            return Ok(_mapper.Map<UsuarioResponse>(usuario));
        }
        catch (Exception ex)
        {
            return Error<Usuario, UsuarioResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
}
