using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using AutoMapper;
using EntityFramework.Exceptions.Common;
using Gex.Extensions.Response;
using Gex.Models;
using Gex.Models.Enums;
using Gex.Repository.Interface;
using Gex.Services.Interface;
using Gex.Utils;
using Gex.ViewModels.Request;
using Gex.ViewModels.Response;

namespace Gex.Services;
using static GexResponse;

public class PreguntaService : IPreguntaService
{
    private readonly IMapper _mapper;
    private readonly IPreguntaRepository _repository;
    private readonly IExamenRepository _examenRepository;
    private readonly IMateriaRepository _materiaRepository;

    public PreguntaService(IMapper mapper, IPreguntaRepository repository, IExamenRepository examenRepository, IMateriaRepository materiaRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _examenRepository = examenRepository;
        _materiaRepository = materiaRepository;
    }

    public async Task<GexResult<PreguntaResponse>> CreatePreguntaAsync(PreguntaRequest preguntaDto)
    {
        try
        {
            if (await _repository.ExistsPreguntaAsync(preguntaDto.Id))
                return Error<Pregunta, PreguntaResponse>(GexErrorMessage.AlreadyExists);

            var pregunta = _mapper.Map<Pregunta>(preguntaDto);

            if(preguntaDto.MateriaId.HasValue && await _materiaRepository.ExistsMateriaAsync(preguntaDto.MateriaId.Value))
                pregunta.MateriaId = preguntaDto.MateriaId;

            if (preguntaDto.ExamenId.HasValue && await _examenRepository.ExistsExamenAsync(preguntaDto.ExamenId.Value))
                pregunta.ExamenId = preguntaDto.ExamenId;

            if (!preguntaDto.ExamenId.HasValue && !preguntaDto.MateriaId.HasValue)
                return Error<Pregunta, PreguntaResponse>(GexErrorMessage.CouldNotCreate, "Se debe seleccionar un exámen o una materia.");

            if (!await _repository.CreatePreguntaAsync(pregunta))
                return Error<Pregunta, PreguntaResponse>(GexErrorMessage.CouldNotCreate);

            var dto = _mapper.Map<PreguntaResponse>(pregunta);
            return Ok<Pregunta, PreguntaResponse>(dto, GexSuccessMessage.Created);
        }
        catch (UniqueConstraintException)
        {
            return Error<Pregunta, PreguntaResponse>(GexErrorMessage.AlreadyExists);
        }
        catch (Exception ex)
        {
            return Error<Pregunta, PreguntaResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<PreguntaResponse>> DeletePreguntaAsync(int id)
    {
        try
        {
            var pregunta = await _repository.GetPreguntaAsync(id);
            if (pregunta == null)
                return Error<Pregunta, PreguntaResponse>(GexErrorMessage.NotFound);

            if (pregunta.Estado == Estado.BAJA)
                return Error<Pregunta, PreguntaResponse>(GexErrorMessage.AlreadyDeleted);

            if (!await _repository.DeletePreguntaAsync(pregunta))
                return Error<Pregunta, PreguntaResponse>(GexErrorMessage.CouldNotDelete);

            return Ok<Pregunta, PreguntaResponse>(_mapper.Map<PreguntaResponse>(pregunta), GexSuccessMessage.Deleted);
        }
        catch (Exception ex)
        {
            return Error<Pregunta, PreguntaResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<PreguntaResponse>> GetPreguntaAsync(int id)
    {
        try
        {
            var pregunta = await _repository.GetPreguntaAsync(id);

            if (pregunta == null)
                return Error<Pregunta, PreguntaResponse>(GexErrorMessage.NotFound);

            return Ok(_mapper.Map<PreguntaResponse>(pregunta));
        }
        catch (Exception ex)
        {
            return Error<Pregunta, PreguntaResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<ICollection<PreguntaResponse>>> GetPreguntasAsync()
    {
        try
        {
            var preguntas = await _repository.GetPreguntasAsync();

            var preguntasDto = new List<PreguntaResponse>();

            foreach (var Pregunta in preguntas)
            {
                preguntasDto.Add(_mapper.Map<PreguntaResponse>(Pregunta));
            }
            return Ok<ICollection<PreguntaResponse>>(preguntasDto);
        }
        catch (Exception ex)
        {
            return Error<Pregunta, ICollection<PreguntaResponse>>(GexErrorMessage.Generic, ex.Message);
        }
    }

    public async Task<GexResult<ICollection<PreguntaResponse>>> GetPreguntasByExamenIdAsync(long examenId)
    {
        try
        {
            var preguntas = await _repository.GetPreguntasByExamenIdAsync(examenId);

            var preguntasDto = new List<PreguntaResponse>();

            foreach (var Pregunta in preguntas)
            {
                preguntasDto.Add(_mapper.Map<PreguntaResponse>(Pregunta));
            }
            return Ok<ICollection<PreguntaResponse>>(preguntasDto);
        }
        catch (Exception ex)
        {
            return Error<Pregunta, ICollection<PreguntaResponse>>(GexErrorMessage.Generic, ex.Message);
        }
    }

    public async Task<GexResult<ICollection<PreguntaResponse>>> GetPreguntasByMateriaIdAsync(long materiaId)
    {
        try
        {
            var preguntas = await _repository.GetPreguntasByMateriaIdAsync(materiaId);

            var preguntasDto = new List<PreguntaResponse>();

            foreach (var Pregunta in preguntas)
            {
                preguntasDto.Add(_mapper.Map<PreguntaResponse>(Pregunta));
            }
            return Ok<ICollection<PreguntaResponse>>(preguntasDto);
        }
        catch (Exception ex)
        {
            return Error<Pregunta, ICollection<PreguntaResponse>>(GexErrorMessage.Generic, ex.Message);
        }
    }

    public async Task<GexResult<PreguntaResponse>> UpdatePreguntaAsync(PreguntaRequest preguntaDto)
    {
        try
        {
            if (preguntaDto.Id == 0)
                return Error<Pregunta, PreguntaResponse>(GexErrorMessage.InvalidId);

            var pregunta = await _repository.GetPreguntaAsync(preguntaDto.Id);

            if (pregunta == null)
                return Error<Pregunta, PreguntaResponse>(GexErrorMessage.NotFound);

            pregunta.Tema = preguntaDto.Tema;
            pregunta.Descripcion = preguntaDto.Descripcion;
            pregunta.Tipo = preguntaDto.Tipo;

            if (!await _repository.UpdatePreguntaAsync(pregunta))
                return Error<Pregunta, PreguntaResponse>(GexErrorMessage.CouldNotUpdate);

            return Ok<Pregunta, PreguntaResponse>(_mapper.Map<PreguntaResponse>(pregunta), GexSuccessMessage.Modified);
        }
        catch (Exception ex)
        {
            return Error<Pregunta, PreguntaResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
}
