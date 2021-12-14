using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Utils;
using Gex.ViewModels.Request;
using Gex.ViewModels.Response;

namespace Gex.Services.Interface;
public interface IInscripcionMesaService
{
    Task<GexResult<ICollection<InscripcionMesaResponse>>> GetInscripcionMesasAsync();
    Task<GexResult<InscripcionMesaResponse>> GetInscripcionMesaAsync(long id);
    Task<GexResult<InscripcionMesaResponse>> CreateInscripcionMesaAsync(InscripcionMesaRequest inscripcionMesaDto);
    Task<GexResult<InscripcionMesaResponse>> UpdateInscripcionMesaAsync(InscripcionMesaRequest inscripcionMesaDto);
    Task<GexResult<InscripcionMesaResponse>> DeleteInscripcionMesaAsync(long id);
}
