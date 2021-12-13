using System.Threading.Tasks;
using Gex.Models;

namespace Gex.Repository.Interface;
public interface IUsuarioRepository
{
    public Task<Usuario> GetUsuarioAsync(int id);
    public Task<Usuario> GetUsuarioByEmail(string email);
    public Task<Usuario> GetUsuarioByUserName(string userName);

    public Task<bool> ExistsUsuarioAsync(int id);
    public Task<bool> ExistsUsuarioByEmail(string email);
    public Task<bool> ExistsUsuarioByUserName(string userName);


}
