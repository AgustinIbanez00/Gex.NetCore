using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Models;
using Gex.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Gex.Repository;

public class ComisionRepository : IComisionRepository
{
    private readonly GexContext _context;

    public ComisionRepository(GexContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateComisionAsync(Comision comision)
    {
        comision.CreatedAt = DateTime.Now;
        comision.Id = 0;
        await _context.Comisiones.AddAsync(comision);
        return await Save();
    }

    public async Task<bool> DeleteComisionAsync(int id)
    {
        Comision comision = await GetComisionAsync(id);
        return await DeleteComisionAsync(comision);
    }

    public async Task<bool> DeleteComisionAsync(Comision comision)
    {
        if (comision == null)
            return false;

        if (comision.Estado == Estado.BAJA)
            return false;

        comision.Estado = Estado.BAJA;
        return await Save();
    }

    public async Task<bool> ExistsComisionAsync(int id) => await _context.Comisiones.FindAsync(id) != null;

    public async Task<bool> ExistsComisionAsync(string nombre) => await _context.Comisiones.AnyAsync(c => c.Nombre == nombre);

    public async Task<Comision> GetComisionAsync(int id) => await _context.Comisiones.FindAsync(id);

    public async Task<Comision> GetComisionAsync(string nombre)
    {
        return await _context.Comisiones.FirstOrDefaultAsync(a => a.Nombre == nombre);
    }

    public async Task<ICollection<Comision>> GetComisionsAsync() => await _context.Comisiones.ToListAsync();

    public async Task<bool> Save() => await _context.SaveChangesAsync() >= 0;

    public async Task<bool> UpdateComisionAsync(Comision comision)
    {
        if (comision == null)
            return false;

        _context.Comisiones.Update(comision);
        comision.UpdatedAt = DateTime.Now;
        return await Save();
    }
}
