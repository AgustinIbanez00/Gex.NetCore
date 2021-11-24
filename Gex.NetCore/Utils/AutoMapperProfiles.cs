using AutoMapper;
using Gex.NetCore.Models;
using Gex.NetCore.ViewModels;

namespace Gex.NetCore.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Comision, ComisionDTO>().ReverseMap();
        }

    }
}
