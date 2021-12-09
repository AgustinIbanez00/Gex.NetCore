using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Utils;
using Gex.ViewModels.Request;

namespace Gex.Services.Interface;
public interface IPreguntaService
{
    Task<GexResponse<ICollection<PreguntaRequest>>> GetPreguntasAsync();
    Task<GexResponse<PreguntaRequest>> GetPreguntaAsync(int id);
    Task<GexResponse<PreguntaRequest>> CreatePreguntaAsync(PreguntaRequest preguntaDto);
    Task<GexResponse<PreguntaRequest>> UpdatePreguntaAsync(PreguntaRequest preguntaDto);
    Task<GexResponse<PreguntaRequest>> DeletePreguntaAsync(int id);
}
