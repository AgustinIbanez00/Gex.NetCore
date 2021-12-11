using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Utils;
using Gex.ViewModels.Request;
using Gex.ViewModels.Response;

namespace Gex.Services.Interface;
public interface IPreguntaService
{
    Task<GexResult<ICollection<PreguntaResponse>>> GetPreguntasAsync();
    Task<GexResult<PreguntaResponse>> GetPreguntaAsync(int id);
    Task<GexResult<PreguntaResponse>> CreatePreguntaAsync(PreguntaRequest preguntaDto);
    Task<GexResult<PreguntaResponse>> UpdatePreguntaAsync(PreguntaRequest preguntaDto);
    Task<GexResult<PreguntaResponse>> DeletePreguntaAsync(int id);
}
