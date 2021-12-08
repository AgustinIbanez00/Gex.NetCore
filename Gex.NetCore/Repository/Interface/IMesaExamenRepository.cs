using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Models;

namespace Gex.Repository.Interface;

public interface IMesaExamenRepository
{
    Task<ICollection<MesaExamen>> GetMesasExamenAsync();
    Task<MesaExamen> GetMesaExamenAsync(long id);
    Task<bool> ExistsMesaExamenAsync(long id);
    Task<bool> CreateMesaExamenAsync(MesaExamen mesaexamen);
    Task<bool> DeleteMesaExamenAsync(MesaExamen mesaexamen);
    Task<bool> DeleteMesaExamenAsync(long id);
    Task<bool> UpdateMesaExamenAsync(MesaExamen mesaexamen);

    Task<bool> Save();
}
