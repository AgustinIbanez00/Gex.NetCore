using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Models;

namespace Gex.Repository.Interface;

public interface IComisionRepository
{
    Task<ICollection<Comision>> GetComisionsAsync();
    Task<Comision> GetComisionAsync(int id);
    Task<Comision> GetComisionAsync(string nombre);
    Task<bool> ExistsComisionAsync(int id);
    Task<bool> CreateComisionAsync(Comision comision);
    Task<bool> DeleteComisionAsync(Comision comision);
    Task<bool> DeleteComisionAsync(int id);
    Task<bool> UpdateComisionAsync(Comision comision);
    Task<bool> ExistsComisionAsync(string nombre);

    Task<bool> Save();
}
