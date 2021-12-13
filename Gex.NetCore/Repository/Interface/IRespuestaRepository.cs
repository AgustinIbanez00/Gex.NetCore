using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Models;

namespace Gex.Repository.Interface;

public interface IRespuestaRepository
{
    Task<ICollection<Respuesta>> GetRespuestasAsync();
    Task<ICollection<Respuesta>> GetRespuestasByPreguntaIdAsync(long preguntaId);
    Task<bool> DeleteRespuestasByPreguntaIdAsync(long preguntaId, bool autosave = true);
    Task<Respuesta> GetRespuestaAsync(long id);
    Task<bool> ExistsRespuestaAsync(long id);
    Task<bool> CreateRespuestaAsync(Respuesta respuesta, bool autosave = true);
    Task<bool> DeleteRespuestaAsync(Respuesta respuesta, bool autosave = true);
    Task<bool> DeleteRespuestaAsync(long id, bool autosave = true);
    Task<bool> UpdateRespuestaAsync(Respuesta respuesta, bool autosave = true);

    Task<bool> Save();
}
