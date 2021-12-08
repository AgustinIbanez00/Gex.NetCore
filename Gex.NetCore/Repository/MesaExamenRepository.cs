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

    public async Task<bool> CreateMesaExamenAsync(MesaExamen mesaexamen)
    {
        mesaexamen.CreatedAt = DateTime.Now;
        mesaexamen.Id = 0;
        await _context.MesasExamen.AddAsync(mesaexamen);
        return await Save();
    }

    public async Task<bool> DeleteMesaExamenAsync(long id)
    {
        MesaExamen MesaExamen = await GetMesaExamenAsync(id);
        return await DeleteMesaExamenAsync(MesaExamen);
    }

    public async Task<bool> DeleteMesaExamenAsync(MesaExamen mesaexamen)
    {
        if (mesaexamen == null)
            return false;

        if (mesaexamen.Estado == Estado.BAJA)
            return false;

        mesaexamen.Estado = Estado.BAJA;
        return await Save();
    }

    public async Task<bool> ExistsMesaExamenAsync(long id) => await _context.MesasExamen.FindAsync(id) != null;

    public async Task<MesaExamen> GetMesaExamenAsync(long id) => await _context.MesasExamen.FindAsync(id);

    public async Task<ICollection<MesaExamen>> GetMesasExamenAsync() => await _context.MesasExamen.ToListAsync();

    public async Task<bool> Save() => await _context.SaveChangesAsync() >= 0;

    public async Task<bool> UpdateMesaExamenAsync(MesaExamen mesaexamen)
    {
        if (mesaexamen == null)
            return false;

        _context.MesasExamen.Update(mesaexamen);
        mesaexamen.UpdatedAt = DateTime.Now;
        return await Save();
    }
}
