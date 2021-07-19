using AutoMapper;  
using Gex.NetCore.Models;
using Gex.NetCore.ViewModels;
using Gex.NetCore.ViewModels.Curso;

namespace Gex.NetCore.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Curso, CursoDTO>().ReverseMap();
            CreateMap<Curso, CursoCreationDTO>().ReverseMap();
        }

    }
}
