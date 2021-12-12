using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Models;

namespace Gex.Repository.Interface;

public interface IRespuestaRepository
{
    Task<ICollection<Respuesta>> GetRespuestasAsync();
    Task<ICollection<Respuesta>> GetRespuestasByPreguntaIdAsync(long preguntaId);
    Task<Respuesta> GetRespuestaAsync(long id);
    Task<bool> ExistsRespuestaAsync(long id);
    Task<bool> CreateRespuestaAsync(Respuesta respuesta);
    Task<bool> DeleteRespuestaAsync(Respuesta respuesta);
    Task<bool> DeleteRespuestaAsync(long id);
    Task<bool> UpdateRespuestaAsync(Respuesta respuesta);

    Task<bool> Save();
}
