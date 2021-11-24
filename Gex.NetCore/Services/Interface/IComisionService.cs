using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.NetCore.Utils;
using Gex.NetCore.ViewModels;

namespace Gex.NetCore.Services.Interface;
public interface IComisionService
{
    Task<ServiceResponse<ICollection<ComisionDTO>>> GetComisionsAsync();
    Task<ServiceResponse<ComisionDTO>> GetComisionAsync(int id);
    Task<ServiceResponse<ComisionDTO>> GetComisionAsync(string nombre);
    Task<ServiceResponse<ComisionDTO>> CreateComisionAsync(ComisionDTO comisionDTO);
    Task<ServiceResponse<ComisionDTO>> UpdateComisionAsync(ComisionDTO comisionDTO);
    Task<ServiceResponse<ComisionDTO>> DeleteComisionAsync(int id);
}
