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

public class ExamenService : IExamenService
{
    private readonly IMapper _mapper;
    private readonly IExamenRepository _examenRepository;
    private readonly IMateriaRepository _materiaRepository;
    private readonly IMesaExamenRepository _mesaExamenRepository;
    private readonly IPreguntaRepository _preguntaRepository;
    private readonly IRespuestaRepository _respuestaRepository;

    public ExamenService
    (
        IMapper mapper,
        IExamenRepository examenRepository,
        IMateriaRepository materiaRepository,
        IMesaExamenRepository mesaExamenRepository,
        IPreguntaRepository preguntaRepository,
        IRespuestaRepository respuestaRepository
    )
    {
        _mapper = mapper;
        _examenRepository = examenRepository;
        _materiaRepository = materiaRepository;
        _mesaExamenRepository = mesaExamenRepository;
        _preguntaRepository = preguntaRepository;
        _respuestaRepository = respuestaRepository;
    }

    public async Task<GexResult<ExamenResponse>> CreateExamenAsync(ExamenRequest examenDto)
    {
        try
        {
            if (await _examenRepository.ExistsExamenAsync(examenDto.Id))
                return Error<Examen, ExamenResponse>(GexErrorMessage.AlreadyExists);

            var examen = _mapper.Map<Examen>(examenDto);

            examen.Materia = await _materiaRepository.GetMateriaAsync(examenDto.MateriaId);

            if (examen.Materia == null)
                return KeyError<Materia, ExamenResponse>(nameof(examenDto.MateriaId), GexErrorMessage.NotFound);

            if (!await _examenRepository.CreateExamenAsync(examen))
                return Error<Examen, ExamenResponse>(GexErrorMessage.CouldNotCreate);

            var dto = _mapper.Map<ExamenResponse>(examen);
            return Ok<Examen, ExamenResponse>(dto, GexSuccessMessage.Created);
        }
        catch (UniqueConstraintException)
        {
            return Error<Examen, ExamenResponse>(GexErrorMessage.AlreadyExists);
        }
        catch (Exception ex)
        {
            return Error<Examen, ExamenResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<ExamenResponse>> DeleteExamenAsync(long id)
    {
        try
        {
            var examen = await _examenRepository.GetExamenAsync(id);
            if (examen == null)
                return Error<Examen, ExamenResponse>(GexErrorMessage.NotFound);

            if (examen.Estado == Estado.BAJA)
                return Error<Examen, ExamenResponse>(GexErrorMessage.AlreadyDeleted);

            if (!await _examenRepository.DeleteExamenAsync(examen))
                return Error<Examen, ExamenResponse>(GexErrorMessage.CouldNotDelete);

            return Ok<Examen, ExamenResponse>(_mapper.Map<ExamenResponse>(examen), GexSuccessMessage.Deleted);
        }
        catch (Exception ex)
        {
            return Error<Examen, ExamenResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<ExamenResponse>> GetExamenAsync(long id)
    {
        try
        {
            var examen = await _examenRepository.GetExamenAsync(id);

            if (examen == null)
                return Error<Examen, ExamenResponse>(GexErrorMessage.NotFound);

            return Ok(_mapper.Map<ExamenResponse>(examen));
        }
        catch (Exception ex)
        {
            return Error<Examen, ExamenResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<ICollection<ExamenResponse>>> GetExamenesAsync()
    {
        try
        {
            var examens = await _examenRepository.GetExamenesAsync();

            var examensDto = new List<ExamenResponse>();

            foreach (var Examen in examens)
            {
                examensDto.Add(_mapper.Map<ExamenResponse>(Examen));
            }
            return Ok<ICollection<ExamenResponse>>(examensDto);
        }
        catch (Exception ex)
        {
            return Error<Examen, ICollection<ExamenResponse>>(GexErrorMessage.Generic, ex.Message);
        }
    }

    public async Task<GexResult<ExamenResponse>> RendirExamenAsync(RendirExamenRequest request)
    {
        
        try
        {
            /*
            var mesaExamen = await _mesaExamenRepository.GetMesaExamenAsync(request.MesaExamenId);
            if (mesaExamen == null)
                return Error<MesaExamen, ExamenResponse>(GexErrorMessage.NotFound);

            bool esperaProfesor = false;
            int preguntasCorrectas = 0;

            foreach (var preguntaRequest in request.Preguntas)
            {
                var pregunta = await _preguntaRepository.GetPreguntaAsync(preguntaRequest.PreguntaId);
                if (pregunta == null)
                    return Error<Pregunta, ExamenResponse>(GexErrorMessage.NotFound);

                if(pregunta.Tipo == PreguntaTipo.Texto)
                {
                    esperaProfesor = true;
                    continue;
                }

                foreach (var respuesta in pregunta.Respuestas)
                {
                    foreach (var respuestaRequest in preguntaRequest.Respuestas)
                    {
                        if (respuesta.PreguntaId != pregunta.Id)
                            return Error<ExamenResponse>("Una respuesta no está vinculada con la pregunta.");

                        if (respuesta.Id != respuestaRequest.Id)
                            return Error<ExamenResponse>("Las respuestas no coinciden con su id.");

                        if(pregunta.Tipo == PreguntaTipo.VerdaderoOFalso)
                        {
                            if(respuesta.Correcto = respuestaRequest.Correcto)
                            {

                            }
                        }
                    }
                }





            }

            */
            return Error<Examen, ExamenResponse>(GexErrorMessage.Generic, "");
        }
        catch (Exception ex)
        {
            return Error<Examen, ExamenResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }

    public async Task<GexResult<ExamenResponse>> UpdateExamenAsync(ExamenRequest examenDto)
    {
        try
        {
            if (examenDto.Id == 0)
                return Error<Examen, ExamenResponse>(GexErrorMessage.InvalidId);

            var examen = await _examenRepository.GetExamenAsync(examenDto.Id);

            if (examen == null)
                return Error<Examen, ExamenResponse>(GexErrorMessage.NotFound);

            examen.Tipo = examenDto.Tipo;
            examen.NotaRegular = examenDto.NotaRegular;
            examen.NotaPromo = examenDto.NotaPromo;
            examen.Recuperatorio = examenDto.Recuperatorio;

            if (!await _examenRepository.UpdateExamenAsync(examen))
                return Error<Examen, ExamenResponse>(GexErrorMessage.CouldNotUpdate);

            return Ok<Examen, ExamenResponse>(_mapper.Map<ExamenResponse>(examen), GexSuccessMessage.Modified);
        }
        catch (Exception ex)
        {
            return Error<Examen, ExamenResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
}
