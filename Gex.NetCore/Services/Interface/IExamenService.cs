using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.DTO;
using Gex.Utils;

namespace Gex.Services.Interface;
public interface IExamenService
{
    Task<GexResponse<ICollection<ExamenDTO>>> GetExamensAsync();
    Task<GexResponse<ExamenDTO>> GetExamenAsync(int id);
    Task<GexResponse<ExamenDTO>> CreateExamenAsync(ExamenDTO ExamenDTO);
    Task<GexResponse<ExamenDTO>> UpdateExamenAsync(ExamenDTO ExamenDTO);
    Task<GexResponse<ExamenDTO>> DeleteExamenAsync(int id);
}
