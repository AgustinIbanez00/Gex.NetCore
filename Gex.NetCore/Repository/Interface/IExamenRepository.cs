using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Models;

namespace Gex.Repository.Interface;

public interface IExamenRepository
{
    Task<ICollection<Examen>> GetExamenesAsync();
    Task<Examen> GetExamenAsync(long id);
    Task<bool> ExistsExamenAsync(long id);
    Task<bool> CreateExamenAsync(Examen Examen);
    Task<bool> DeleteExamenAsync(Examen Examen);
    Task<bool> DeleteExamenAsync(long id);
    Task<bool> UpdateExamenAsync(Examen Examen);
    Task<bool> Save();
}
