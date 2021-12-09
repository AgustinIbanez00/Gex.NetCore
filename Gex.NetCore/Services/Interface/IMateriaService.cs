using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Utils;
using Gex.ViewModels.Request;

namespace Gex.Services.Interface;
public interface IMateriaService
{
    Task<GexResponse<ICollection<MateriaRequest>>> GetMateriasAsync();
    Task<GexResponse<MateriaRequest>> GetMateriaAsync(int id);
    Task<GexResponse<MateriaRequest>> CreateMateriaAsync(MateriaRequest materiaDto);
    Task<GexResponse<MateriaRequest>> UpdateMateriaAsync(MateriaRequest materiaDto);
    Task<GexResponse<MateriaRequest>> DeleteMateriaAsync(int id);
}
