using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Models;

namespace Gex.Repository.Interface;

public interface IInscripcionMesaRepository
{
    Task<ICollection<InscripcionMesa>> GetInscripcionMesasAsync();
    Task<InscripcionMesa> GetInscripcionMesaAsync(long id);
    Task<bool> ExistsInscripcionMesaAsync(long id);
    Task<bool> CreateInscripcionMesaAsync(InscripcionMesa inscripcionmesa);
    Task<bool> DeleteInscripcionMesaAsync(InscripcionMesa inscripcionmesa);
    Task<bool> DeleteInscripcionMesaAsync(long id);
    Task<bool> UpdateInscripcionMesaAsync(InscripcionMesa inscripcionmesa);

    Task<bool> Save();
}
