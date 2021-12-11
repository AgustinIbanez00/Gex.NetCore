using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Utils;
using Gex.ViewModels.Request;
using Gex.ViewModels.Response;

namespace Gex.Services.Interface;
public interface IComisionService
{
    Task<GexResult<ICollection<ComisionResponse>>> GetComisionsAsync();
    Task<GexResult<ComisionResponse>> GetComisionAsync(long id);
    Task<GexResult<ComisionResponse>> GetComisionAsync(string nombre);
    Task<GexResult<ComisionResponse>> CreateComisionAsync(ComisionRequest comisionDto);
    Task<GexResult<ComisionResponse>> UpdateComisionAsync(ComisionRequest comisionDto);
    Task<GexResult<ComisionResponse>> DeleteComisionAsync(long id);
}
