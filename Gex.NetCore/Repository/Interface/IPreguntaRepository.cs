using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Models;

namespace Gex.Repository.Interface;

public interface IPreguntaRepository
{
    Task<ICollection<Pregunta>> GetPreguntasAsync();
    Task<ICollection<Pregunta>> GetPreguntasByExamenIdAsync(long examenId);
    Task<ICollection<Pregunta>> GetPreguntasByMateriaIdAsync(long materiaId);
    Task<Pregunta> GetPreguntaAsync(long id);
    Task<bool> ExistsPreguntaAsync(long id);
    Task<bool> CreatePreguntaAsync(Pregunta pregunta);
    Task<bool> DeletePreguntaAsync(Pregunta pregunta);
    Task<bool> DeletePreguntaAsync(long id);
    Task<bool> UpdatePreguntaAsync(Pregunta pregunta);

    Task<bool> Save();
}
