using System.Threading.Tasks;
using Gex.Models;

namespace Gex.Repository.Interface;
public interface IUsuarioRepository
{
    Task<Usuario> GetUsuarioAsync(int id);
    Task<Usuario> GetUsuarioByEmailAsync(string email);
    Task<Usuario> GetUsuarioByUserNameAsync(string userName);
    Task<bool> ExistsUsuarioAsync(int id);
    Task<bool> ExistsUsuarioByEmailAsync(string email);
    Task<bool> ExistsUsuarioByUserNameAsync(string userName);
    Task<bool> CreateUsuarioAsync(Usuario usuario);
    Task<bool> Save();
}
