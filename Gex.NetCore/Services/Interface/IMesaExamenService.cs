using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Utils;
using Gex.ViewModels.Request;

namespace Gex.Services.Interface;
public interface IMesaExamenService
{
    Task<GexResponse<ICollection<MesaExamenRequest>>> GetMesaExamensAsync();
    Task<GexResponse<MesaExamenRequest>> GetMesaExamenAsync(int id);
    Task<GexResponse<MesaExamenRequest>> CreateMesaExamenAsync(MesaExamenRequest mesaExamenDto);
    Task<GexResponse<MesaExamenRequest>> UpdateMesaExamenAsync(MesaExamenRequest mesaExamenDto);
    Task<GexResponse<MesaExamenRequest>> DeleteMesaExamenAsync(int id);
}
