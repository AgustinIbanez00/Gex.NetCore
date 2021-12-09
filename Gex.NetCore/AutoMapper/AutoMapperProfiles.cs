using AutoMapper;
using Gex.Models;
using Gex.ViewModels.Request;
using Gex.ViewModels.Response;

namespace Gex.Utils;
public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Comision, ComisionRequest>().ReverseMap();
        CreateMap<Examen, ExamenRequest>().ReverseMap();
        CreateMap<Examen, ExamenResponse>().ReverseMap();
        CreateMap<Materia, MateriaRequest>().ReverseMap();
        CreateMap<Materia, MateriaResponse>().ReverseMap();
        CreateMap<MesaExamen, MesaExamenRequest>().ReverseMap();
        CreateMap<Pregunta, PreguntaRequest>().ReverseMap();
        CreateMap<Pregunta, PreguntaResponse>().ReverseMap();
    }
}
