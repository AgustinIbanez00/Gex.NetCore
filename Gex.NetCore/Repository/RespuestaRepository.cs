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

    public async Task<bool> CreateRespuestaAsync(Respuesta respuesta)
    {
        respuesta.CreatedAt = DateTime.Now;
        respuesta.Id = 0;
        await _context.Respuestas.AddAsync(respuesta);
        return await Save();
    }

    public async Task<bool> DeleteRespuestaAsync(long id)
    {
        Respuesta respuesta = await GetRespuestaAsync(id);
        return await DeleteRespuestaAsync(respuesta);
    }

    public async Task<bool> DeleteRespuestaAsync(Respuesta respuesta)
    {
        if (respuesta == null)
            return false;

        if (respuesta.Estado == Estado.BAJA)
            return false;

        respuesta.Estado = Estado.BAJA;
        return await Save();
    }

    public async Task<bool> ExistsRespuestaAsync(long id) => await GetRespuestaAsync(id) != null;

    public async Task<Respuesta> GetRespuestaAsync(long id) => await _context.Respuestas.FirstOrDefaultAsync(m => m.Estado == Estado.NORMAL && m.Id == id);

    public async Task<ICollection<Respuesta>> GetRespuestasAsync() => await _context.Respuestas.Where(m => m.Estado == Estado.NORMAL).ToListAsync();

    public async Task<bool> Save() => await _context.SaveChangesAsync() >= 0;

    public async Task<bool> UpdateRespuestaAsync(Respuesta respuesta)
    {
        if (respuesta == null || respuesta.Estado == Estado.BAJA)
            return false;

        _context.Respuestas.Update(respuesta);
        respuesta.UpdatedAt = DateTime.Now;
        return await Save();
    }
}
