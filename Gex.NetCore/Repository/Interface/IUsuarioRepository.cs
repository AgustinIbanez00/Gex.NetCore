using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Models;
using Gex.Models.Enums;

namespace Gex.Repository.Interface;
public interface IUsuarioRepository
{
    Task<ICollection<Usuario>> GetUsuariosAsync();
    Task<Usuario> GetUsuarioAsync(int id);
    Task<ICollection<Usuario>> GetUsuariosByEmailAsync(string userName);
    Task<Usuario> GetUsuarioByEmailAsync(string email);
    Task<Usuario> GetUsuarioByUserNameAsync(string userName);
    Task<ICollection<Usuario>> GetUsuariosByTipoAsync(UsuarioTipo tipo);
    Task<bool> ExistsUsuarioAsync(int id);
    Task<bool> ExistsUsuarioByEmailAsync(string email);
    Task<bool> ExistsUsuarioByUserNameAsync(string userName);
    Task<bool> CreateUsuarioAsync(Usuario usuario);
    Task<bool> LockUsuarioAsync(int id);
    Task<bool> Save();
}
