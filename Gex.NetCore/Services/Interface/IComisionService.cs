using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Utils;
using Gex.ViewModels.Request;

namespace Gex.Services.Interface;
public interface IComisionService
{
    Task<GexResponse<ICollection<ComisionRequest>>> GetComisionsAsync();
    Task<GexResponse<ComisionRequest>> GetComisionAsync(int id);
    Task<GexResponse<ComisionRequest>> GetComisionAsync(string nombre);
    Task<GexResponse<ComisionRequest>> CreateComisionAsync(ComisionRequest comisionDTO);
    Task<GexResponse<ComisionRequest>> UpdateComisionAsync(ComisionRequest comisionDTO);
    Task<GexResponse<ComisionRequest>> DeleteComisionAsync(int id);
}
