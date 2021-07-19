using System.Collections.Generic;
using System.Threading.Tasks;

using Gex.NetCore.ViewModels;
using Gex.NetCore.ViewModels.Curso;

namespace Gex.NetCore.Services.Interface
{
    public interface ICursoService
    {
        public Task<long> CreateAsync(CursoDTO curso);
        public Task<CursoDTO> ModifyAsync(long id, CursoDTO curso);

        public Task<CursoDTO> DeleteAsync(long curso);

        public Task<List<CursoCreationDTO>> AllAsync();

        public Task<CursoDTO> FindAync(long id);

        public bool Exists(long id);

    }
}
