using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Models;
using Gex.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Gex.Repository;

public class MesaExamenRepository : IMesaExamenRepository
{
    private readonly GexContext _context;

    public MesaExamenRepository(GexContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateMesaExamenAsync(MesaExamen mesaExamen)
    {
        mesaExamen.CreatedAt = DateTime.Now;
        mesaExamen.Id = 0;
        await _context.MesasExamen.AddAsync(mesaExamen);
        return await Save();
    }

    public async Task<bool> DeleteMesaExamenAsync(long id)
    {
        MesaExamen MesaExamen = await GetMesaExamenAsync(id);
        return await DeleteMesaExamenAsync(MesaExamen);
    }

    public async Task<bool> DeleteMesaExamenAsync(MesaExamen mesaExamen)
    {
        if (mesaExamen == null)
            return false;

        if (mesaExamen.Estado == Estado.BAJA)
            return false;

        mesaExamen.Estado = Estado.BAJA;
        return await Save();
    }

    public async Task<bool> ExistsMesaExamenAsync(long id) => await _context.MesasExamen.FindAsync(id) != null;

    public async Task<MesaExamen> GetMesaExamenAsync(long id) => await _context.MesasExamen.FindAsync(id);

    public async Task<ICollection<MesaExamen>> GetMesasExamenAsync() => await _context.MesasExamen.ToListAsync();

    public async Task<bool> Save() => await _context.SaveChangesAsync() >= 0;

    public async Task<bool> UpdateMesaExamenAsync(MesaExamen mesaExamen)
    {
        if (mesaExamen == null || mesaExamen.Estado == Estado.BAJA)
            return false;

        _context.MesasExamen.Update(mesaExamen);
        mesaExamen.UpdatedAt = DateTime.Now;
        return await Save();
    }
}
