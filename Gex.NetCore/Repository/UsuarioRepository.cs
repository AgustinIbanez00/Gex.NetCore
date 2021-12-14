using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gex.Models;
using Gex.Models.Enums;
using Gex.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Gex.Repository;
public class UsuarioRepository : IUsuarioRepository
{
    private readonly GexContext _context;

    public UsuarioRepository(GexContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateUsuarioAsync(Usuario usuario)
    {
        usuario.Id = 0;
        await _context.Usuarios.AddAsync(usuario);
        return await Save();
    }

    public async Task<bool> ExistsUsuarioAsync(int id) => await _context.Usuarios.AnyAsync(u => u.Id == id);
    public async Task<bool> ExistsUsuarioByEmailAsync(string email) => await _context.Usuarios.AnyAsync(u => u.Email == email);
    public async Task<bool> ExistsUsuarioByUserNameAsync(string userName) => await _context.Usuarios.AnyAsync(u => u.UserName == userName);
    public async Task<Usuario> GetUsuarioAsync(int id) => await _context.Usuarios.FindAsync(id);
    public async Task<ICollection<Usuario>> GetUsuariosAsync() => await _context.Usuarios.ToListAsync();
    public async Task<Usuario> GetUsuarioByEmailAsync(string email) => await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
    public async Task<Usuario> GetUsuarioByUserNameAsync(string userName) => await _context.Usuarios.FirstOrDefaultAsync(u => u.UserName == userName);
    public async Task<ICollection<Usuario>> GetUsuariosByTipoAsync(UsuarioTipo tipo) => await _context.Usuarios.Where(u => u.Tipo == tipo).ToListAsync();
    public async Task<bool> Save() => await _context.SaveChangesAsync() >= 0;

    public async Task<bool> LockUsuarioAsync(int id)
    {
        Usuario usuario = await GetUsuarioAsync(id);
        if (usuario == null)
            return false;

        usuario.LockoutEnabled = true;

        return await Save();
    }

    public async Task<ICollection<Usuario>> GetUsuariosByEmailAsync(string email)
    {
        var usuario = await GetUsuarioByEmailAsync(email);

        if (usuario == null)
            return null;

        return await _context.Usuarios.Where(u => u.Tipo <= usuario.Tipo && u.LockoutEnabled == false).ToListAsync();
    }
}
