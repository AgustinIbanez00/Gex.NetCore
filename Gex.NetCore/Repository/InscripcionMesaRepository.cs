using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Models;
using Gex.Models.Enums;
using Gex.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Gex.Repository;

public class InscripcionMesaRepository : IInscripcionMesaRepository
{
    private readonly GexContext _context;

    public InscripcionMesaRepository(GexContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateInscripcionMesaAsync(InscripcionMesa inscripcionMesa)
    {
        inscripcionMesa.CreatedAt = DateTime.Now;
        inscripcionMesa.Id = 0;
        await _context.InscripcionesMesas.AddAsync(inscripcionMesa);
        return await Save();
    }

    public async Task<bool> DeleteInscripcionMesaAsync(long id)
    {
        InscripcionMesa inscripcionMesa = await GetInscripcionMesaAsync(id);
        return await DeleteInscripcionMesaAsync(inscripcionMesa);
    }

    public async Task<bool> DeleteInscripcionMesaAsync(InscripcionMesa inscripcionMesa)
    {
        if (inscripcionMesa == null)
            return false;

        if (inscripcionMesa.Estado == Estado.BAJA)
            return false;

        inscripcionMesa.Estado = Estado.BAJA;
        return await Save();
    }

    public async Task<bool> ExistsInscripcionMesaAsync(long id) => await GetInscripcionMesaAsync(id) != null;

    public async Task<InscripcionMesa> GetInscripcionMesaAsync(long id) => await _context.InscripcionesMesas.FirstOrDefaultAsync(m => m.Estado == Estado.NORMAL && m.Id == id);

    public async Task<ICollection<InscripcionMesa>> GetInscripcionMesasAsync() => await _context.InscripcionesMesas.Where(m => m.Estado == Estado.NORMAL).ToListAsync();

    public async Task<bool> Save() => await _context.SaveChangesAsync() >= 0;

    public async Task<bool> UpdateInscripcionMesaAsync(InscripcionMesa inscripcionMesa)
    {
        if (inscripcionMesa == null || inscripcionMesa.Estado == Estado.BAJA)
            return false;

        _context.InscripcionesMesas.Update(inscripcionMesa);
        inscripcionMesa.UpdatedAt = DateTime.Now;
        return await Save();
    }
}
