using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Utils;
using Gex.ViewModels.Request;
using Gex.ViewModels.Response;

namespace Gex.Services.Interface;
public interface IMesaExamenService
{
    Task<GexResult<ICollection<MesaExamenResponse>>> GetMesasExamenesAsync();
    Task<GexResult<ICollection<MesaExamenResponse>>> GetMesasExamenesByEmailAsync(string email);
    Task<GexResult<MesaExamenResponse>> GetMesaExamenAsync(long id);
    Task<GexResult<MesaExamenResponse>> CreateMesaExamenAsync(MesaExamenRequest mesaExamenDto);
    Task<GexResult<MesaExamenResponse>> UpdateMesaExamenAsync(MesaExamenRequest mesaExamenDto);
    Task<GexResult<MesaExamenResponse>> DeleteMesaExamenAsync(long id);
}
