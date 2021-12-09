using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Models;
using Gex.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Gex.Repository;

public class MateriaRepository : IMateriaRepository
{
    private readonly GexContext _context;

    public MateriaRepository(GexContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateMateriaAsync(Materia materia)
    {
        materia.CreatedAt = DateTime.Now;
        materia.Id = 0;
        await _context.Materias.AddAsync(materia);
        return await Save();
    }

    public async Task<bool> DeleteMateriaAsync(long id)
    {
        Materia materia = await GetMateriaAsync(id);
        return await DeleteMateriaAsync(materia);
    }

    public async Task<bool> DeleteMateriaAsync(Materia materia)
    {
        if (materia == null)
            return false;

        if (materia.Estado == Estado.BAJA)
            return false;

        materia.Estado = Estado.BAJA;
        return await Save();
    }

    public async Task<bool> ExistsMateriaAsync(long id) => await _context.Materias.FindAsync(id) != null;

    public async Task<Materia> GetMateriaAsync(long id) => await _context.Materias.FindAsync(id);

    public async Task<ICollection<Materia>> GetMateriasAsync() => await _context.Materias.ToListAsync();

    public async Task<bool> Save() => await _context.SaveChangesAsync() >= 0;

    public async Task<bool> UpdateMateriaAsync(Materia materia)
    {
        if (materia == null || materia.Estado == Estado.BAJA)
            return false;

        _context.Materias.Update(materia);
        materia.UpdatedAt = DateTime.Now;
        return await Save();
    }
}
