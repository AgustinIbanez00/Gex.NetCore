using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Models;

namespace Gex.Repository.Interface;

public interface IComisionRepository
{
    Task<ICollection<Comision>> GetComisionesAsync();
    Task<Comision> GetComisionAsync(long id);
    Task<Comision> GetComisionAsync(string nombre);
    Task<bool> ExistsComisionAsync(long id);
    Task<bool> CreateComisionAsync(Comision comision);
    Task<bool> DeleteComisionAsync(Comision comision);
    Task<bool> DeleteComisionAsync(long id);
    Task<bool> UpdateComisionAsync(Comision comision);
    Task<bool> ExistsComisionAsync(string nombre);

    Task<bool> Save();
}
