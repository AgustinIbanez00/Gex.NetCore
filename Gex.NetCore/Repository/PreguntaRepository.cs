using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Models;
using Gex.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Gex.Repository;

public class PreguntaRepository : IPreguntaRepository
{
    private readonly GexContext _context;

    public PreguntaRepository(GexContext context)
    {
        _context = context;
    }

    public async Task<bool> CreatePreguntaAsync(Pregunta pregunta)
    {
        pregunta.CreatedAt = DateTime.Now;
        pregunta.Id = 0;
        await _context.Preguntas.AddAsync(pregunta);
        return await Save();
    }

    public async Task<bool> DeletePreguntaAsync(long id)
    {
        Pregunta pregunta = await GetPreguntaAsync(id);
        return await DeletePreguntaAsync(pregunta);
    }

    public async Task<bool> DeletePreguntaAsync(Pregunta pregunta)
    {
        if (pregunta == null)
            return false;

        if (pregunta.Estado == Estado.BAJA)
            return false;

        pregunta.Estado = Estado.BAJA;
        return await Save();
    }

    public async Task<bool> ExistsPreguntaAsync(long id) => await _context.Preguntas.FindAsync(id) != null;

    public async Task<Pregunta> GetPreguntaAsync(long id) => await _context.Preguntas.FindAsync(id);

    public async Task<ICollection<Pregunta>> GetPreguntasAsync() => await _context.Preguntas.ToListAsync();

    public async Task<bool> Save() => await _context.SaveChangesAsync() >= 0;

    public async Task<bool> UpdatePreguntaAsync(Pregunta pregunta)
    {
        if (pregunta == null || pregunta.Estado == Estado.BAJA)
            return false;

        _context.Preguntas.Update(pregunta);
        pregunta.UpdatedAt = DateTime.Now;
        return await Save();
    }
}
