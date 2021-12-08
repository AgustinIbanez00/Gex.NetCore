using AutoMapper;
using Gex.DTO;
using Gex.Models;

namespace Gex.Utils;
public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Comision, ComisionDTO>().ReverseMap();
        CreateMap<Examen, ExamenDTO>().ReverseMap();
        CreateMap<Materia, MateriaDTO>().ReverseMap();
    }
}
