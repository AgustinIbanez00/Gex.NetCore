using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.DTO;
using Gex.Utils;

namespace Gex.Services.Interface;
public interface IMesaExamenService
{
    Task<GexResponse<ICollection<MesaExamenDTO>>> GetMesaExamensAsync();
    Task<GexResponse<MesaExamenDTO>> GetMesaExamenAsync(int id);
    Task<GexResponse<MesaExamenDTO>> CreateMesaExamenAsync(MesaExamenDTO mesaexamenDto);
    Task<GexResponse<MesaExamenDTO>> UpdateMesaExamenAsync(MesaExamenDTO mesaexamenDto);
    Task<GexResponse<MesaExamenDTO>> DeleteMesaExamenAsync(int id);
}
