using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Models;
using Gex.Models.Enums;
using Gex.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Gex.Repository;

public class RespuestaRepository : IRespuestaRepository
{
    private readonly GexContext _context;

    public RespuestaRepository(GexContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateRespuestaAsync(Respuesta respuesta, bool autosave = true)
    {
        respuesta.Id = 0;
        await _context.Respuestas.AddAsync(respuesta);
        return autosave ? await Save() : true;
    }

    public async Task<bool> DeleteRespuestaAsync(long id, bool autosave = true)
    {
        Respuesta respuesta = await GetRespuestaAsync(id);
        return await DeleteRespuestaAsync(respuesta, autosave);
    }

    public async Task<bool> DeleteRespuestaAsync(Respuesta respuesta, bool autosave = true)
    {
        if (respuesta == null)
            return false;
        _context.Respuestas.Remove(respuesta);

        return autosave ? await Save() : true;
    }

    public async Task<bool> DeleteRespuestasByPreguntaIdAsync(long preguntaId, bool autosave = true)
    {
        var respuestasRelacionadas = await _context.Respuestas.Where(r => r.PreguntaId == preguntaId).ToListAsync();
        _context.Respuestas.RemoveRange(respuestasRelacionadas);
        return autosave ? await Save() : true;
    }   

    public async Task<bool> ExistsRespuestaAsync(long id) => await _context.Respuestas.AnyAsync(r => r.Id == id);

    public async Task<Respuesta> GetRespuestaAsync(long id) => await _context.Respuestas.FindAsync(id);

    public async Task<ICollection<Respuesta>> GetRespuestasAsync() => await _context.Respuestas.ToListAsync();

    public async Task<ICollection<Respuesta>> GetRespuestasByPreguntaIdAsync(long preguntaId) => await _context.Respuestas.Where(m => m.PreguntaId == preguntaId).ToListAsync();

    public async Task<bool> Save() => await _context.SaveChangesAsync() >= 0;

    public async Task<bool> UpdateRespuestaAsync(Respuesta respuesta, bool autosave = true)
    {
        if (respuesta == null)
            return false;

        _context.Respuestas.Update(respuesta);
        return autosave ? await Save() : true;
    }
}
