using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Utils;
using Gex.ViewModels.Request;
using Gex.ViewModels.Response;

namespace Gex.Services.Interface;
public interface IRespuestaService
{
    Task<GexResult<ICollection<RespuestaResponse>>> GetRespuestasAsync();
    Task<GexResult<RespuestaResponse>> GetRespuestaAsync(long id);
    //Task<GexResult<RespuestaResponse>> PrepareCreateRespuestaAsync(RespuestaCreateRequest request);
    Task<GexResult<RespuestaResponse>> CreateRespuestaAsync(RespuestaRequest respuestaDto);
    Task<GexResult<RespuestaResponse>> UpdateRespuestaAsync(RespuestaRequest respuestaDto);
    Task<GexResult<RespuestaResponse>> DeleteRespuestaAsync(long id);
}
