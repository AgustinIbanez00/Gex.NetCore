using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Utils;
using Gex.ViewModels.Request;
using Gex.ViewModels.Response;

namespace Gex.Services.Interface;
public interface IExamenService
{
    Task<GexResponse<ICollection<ExamenResponse>>> GetExamensAsync();
    Task<GexResponse<ExamenResponse>> GetExamenAsync(int id);
    Task<GexResponse<ExamenRequest>> CreateExamenAsync(ExamenRequest ExamenDTO);
    Task<GexResponse<ExamenRequest>> UpdateExamenAsync(ExamenRequest ExamenDTO);
    Task<GexResponse<ExamenRequest>> DeleteExamenAsync(int id);
}
