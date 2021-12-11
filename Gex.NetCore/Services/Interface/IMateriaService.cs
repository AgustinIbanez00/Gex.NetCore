using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Utils;
using Gex.ViewModels.Request;
using Gex.ViewModels.Response;

namespace Gex.Services.Interface;
public interface IMateriaService
{
    Task<GexResult<ICollection<MateriaResponse>>> GetMateriasAsync();
    Task<GexResult<MateriaResponse>> GetMateriaAsync(long id);
    Task<GexResult<MateriaResponse>> CreateMateriaAsync(MateriaRequest materiaDto);
    Task<GexResult<MateriaResponse>> UpdateMateriaAsync(MateriaRequest materiaDto);
    Task<GexResult<MateriaResponse>> DeleteMateriaAsync(long id);
}
