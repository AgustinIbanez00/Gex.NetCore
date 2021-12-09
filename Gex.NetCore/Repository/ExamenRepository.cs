using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Models;
using Gex.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Gex.Repository;

public class ExamenRepository : IExamenRepository
{
    private readonly GexContext _context;

    public ExamenRepository(GexContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateExamenAsync(Examen examen)
    {
        examen.CreatedAt = DateTime.Now;
        examen.Id = 0;

        await _context.Examenes.AddAsync(examen);
        return await Save();
    }

    public async Task<bool> DeleteExamenAsync(long id)
    {
        Examen examen = await GetExamenAsync(id);
        return await DeleteExamenAsync(examen);
    }

    public async Task<bool> DeleteExamenAsync(Examen examen)
    {
        if (examen == null)
            return false;

        if (examen.Estado == Estado.BAJA)
            return false;

        examen.Estado = Estado.BAJA;
        return await Save();
    }

    public async Task<bool> ExistsExamenAsync(long id) => await _context.Examenes.FindAsync(id) != null;

    public async Task<Examen> GetExamenAsync(long id)
    {
        return await _context.Examenes.Include(e => e.Materia).FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<ICollection<Examen>> GetExamensAsync() => await _context.Examenes.Include(c => c.Materia).ToListAsync();

    public async Task<bool> Save() => await _context.SaveChangesAsync() >= 0;

    public async Task<bool> UpdateExamenAsync(Examen examen)
    {
        if (examen == null || examen.Estado == Estado.BAJA)
            return false;

        _context.Examenes.Update(examen);
        examen.UpdatedAt = DateTime.Now;
        return await Save();
    }
}
