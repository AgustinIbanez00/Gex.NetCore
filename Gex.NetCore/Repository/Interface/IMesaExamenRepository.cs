using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Models;

namespace Gex.Repository.Interface;

public interface IMesaExamenRepository
{
    Task<ICollection<MesaExamen>> GetMesasExamenAsync();
    Task<MesaExamen> GetMesaExamenAsync(long id);
    Task<bool> ExistsMesaExamenAsync(long id);
    Task<bool> CreateMesaExamenAsync(MesaExamen mesaExamen);
    Task<bool> DeleteMesaExamenAsync(MesaExamen mesaExamen);
    Task<bool> DeleteMesaExamenAsync(long id);
    Task<bool> UpdateMesaExamenAsync(MesaExamen mesaExamen);

    Task<bool> Save();
}
