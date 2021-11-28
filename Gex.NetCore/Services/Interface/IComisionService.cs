using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.NetCore.DTO;
using Gex.NetCore.Utils;

namespace Gex.NetCore.Services.Interface;
public interface IComisionService
{
    Task<GexResponse<ICollection<ComisionDTO>>> GetComisionsAsync();
    Task<GexResponse<ComisionDTO>> GetComisionAsync(int id);
    Task<GexResponse<ComisionDTO>> GetComisionAsync(string nombre);
    Task<GexResponse<ComisionDTO>> CreateComisionAsync(ComisionDTO comisionDTO);
    Task<GexResponse<ComisionDTO>> UpdateComisionAsync(ComisionDTO comisionDTO);
    Task<GexResponse<ComisionDTO>> DeleteComisionAsync(int id);
}
