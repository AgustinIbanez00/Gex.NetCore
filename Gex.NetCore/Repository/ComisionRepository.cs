using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gex.Models;
using Gex.Models.Enums;
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

    public async Task<bool> DeleteComisionAsync(long id)
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

    public async Task<bool> ExistsComisionAsync(long id) => await GetComisionAsync(id) != null;

    public async Task<bool> ExistsComisionAsync(string nombre) => await GetComisionAsync(nombre) != null;

    public async Task<Comision> GetComisionAsync(long id) => await _context.Comisiones.FirstOrDefaultAsync(m => m.Estado == Estado.NORMAL && m.Id == id);

    public async Task<Comision> GetComisionAsync(string nombre) => await _context.Comisiones.FirstOrDefaultAsync(a => a.Estado == Estado.NORMAL && a.Nombre == nombre);

    public async Task<ICollection<Comision>> GetComisionesAsync() => await _context.Comisiones.Where(m => m.Estado == Estado.NORMAL).ToListAsync();

    public async Task<bool> Save() => await _context.SaveChangesAsync() >= 0;

    public async Task<bool> UpdateComisionAsync(Comision comision)
    {
        if (comision == null || comision.Estado == Estado.BAJA)
            return false;

        _context.Comisiones.Update(comision);
        comision.UpdatedAt = DateTime.Now;
        return await Save();
    }
}
