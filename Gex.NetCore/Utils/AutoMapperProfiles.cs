using AutoMapper;
using Gex.NetCore.DTO;
using Gex.NetCore.Models;

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
