using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Models;

namespace Gex.Repository.Interface;

public interface IMateriaRepository
{
    Task<ICollection<Materia>> GetMateriasAsync();
    Task<Materia> GetMateriaAsync(long id);
    Task<bool> ExistsMateriaAsync(long id);
    Task<bool> CreateMateriaAsync(Materia Materia);
    Task<bool> DeleteMateriaAsync(Materia Materia);
    Task<bool> DeleteMateriaAsync(long id);
    Task<bool> UpdateMateriaAsync(Materia Materia);

    Task<bool> Save();
}
