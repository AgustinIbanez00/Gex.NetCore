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
        CreateMap<Comision, ComisionResponse>().ReverseMap();
        CreateMap<Examen, ExamenRequest>().ReverseMap();
        CreateMap<Examen, ExamenResponse>().ReverseMap();
        CreateMap<Materia, MateriaRequest>().ReverseMap();
        CreateMap<Materia, MateriaResponse>().ReverseMap();
        CreateMap<MesaExamen, MesaExamenRequest>().ReverseMap();
        CreateMap<MesaExamen, MesaExamenResponse>().ReverseMap();
        CreateMap<Pregunta, PreguntaRequest>().ReverseMap();
        CreateMap<Pregunta, PreguntaResponse>().ReverseMap();
        CreateMap<Respuesta, RespuestaCreateRequest>().ReverseMap();
        CreateMap<Respuesta, RespuestaRequest>().ReverseMap();
        CreateMap<Respuesta, RespuestaResponse>().ReverseMap();
    }
}
