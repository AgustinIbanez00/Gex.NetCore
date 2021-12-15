using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Models;

namespace Gex.Repository.Interface;

public interface IMateriaRepository
{
    Task<ICollection<Materia>> GetMateriasAsync();
    Task<Materia> GetMateriaAsync(long id);
    Task<bool> ExistsMateriaAsync(long id);
    Task<bool> CreateMateriaAsync(Materia materia);
    Task<bool> DeleteMateriaAsync(Materia materia);
    Task<bool> DeleteMateriaAsync(long id);
    Task<bool> UpdateMateriaAsync(Materia materia);

    Task<bool> Save();
}
