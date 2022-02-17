using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using EntityFramework.Exceptions.Common;
using Gex.Extensions;
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

public class RespuestaService : IRespuestaService
{
    private readonly IMapper _mapper;
    private readonly IRespuestaRepository _respuestaRepository;
    private readonly IPreguntaRepository _preguntaRepository;

    public RespuestaService(IMapper mapper, IRespuestaRepository respuestaRepository, IPreguntaRepository preguntaRepository)
    {
        _mapper = mapper;
        _respuestaRepository = respuestaRepository;
        _preguntaRepository = preguntaRepository;
    }

    public async Task<GexResult<RespuestaResponse>> CreateRespuestaAsync(RespuestaRequest respuestaDto)
    {
        try
        {
            if (await _respuestaRepository.ExistsRespuestaAsync(respuestaDto.Id))
                return Error<Respuesta, RespuestaResponse>(GexErrorMessage.AlreadyExists);

            var respuesta = _mapper.Map<Respuesta>(respuestaDto);

            if (!await _preguntaRepository.ExistsPreguntaAsync(respuestaDto.PreguntaId))
                return KeyError<Pregunta, RespuestaResponse>(nameof(respuestaDto.PreguntaId), GexErrorMessage.NotFound);

            respuesta.PreguntaId = respuestaDto.PreguntaId;

            if (!await _respuestaRepository.CreateRespuestaAsync(respuesta))
                return Error<Respuesta, RespuestaResponse>(GexErrorMessage.CouldNotCreate);

            var dto = _mapper.Map<RespuestaResponse>(respuesta);
            return Ok<Respuesta, RespuestaResponse>(dto, GexSuccessMessage.Created);
        }
        catch (UniqueConstraintException)
        {
            return Error<Respuesta, RespuestaResponse>(GexErrorMessage.AlreadyExists);
        }
        catch (Exception ex)
        {
            return Error<Respuesta, RespuestaResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<RespuestaResponse>> DeleteRespuestaAsync(long id)
    {
        try
        {
            var respuesta = await _respuestaRepository.GetRespuestaAsync(id);
            if (respuesta == null)
                return Error<Respuesta, RespuestaResponse>(GexErrorMessage.NotFound);

            if (!await _respuestaRepository.DeleteRespuestaAsync(respuesta))
                return Error<Respuesta, RespuestaResponse>(GexErrorMessage.CouldNotDelete);

            return Ok<Respuesta, RespuestaResponse>(_mapper.Map<RespuestaResponse>(respuesta), GexSuccessMessage.Deleted);
        }
        catch (Exception ex)
        {
            return Error<Respuesta, RespuestaResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<RespuestaResponse>> GetRespuestaAsync(long id)
    {
        try
        {
            var respuesta = await _respuestaRepository.GetRespuestaAsync(id);

            if (respuesta == null)
                return Error<Respuesta, RespuestaResponse>(GexErrorMessage.NotFound);

            return Ok(_mapper.Map<RespuestaResponse>(respuesta));
        }
        catch (Exception ex)
        {
            return Error<Respuesta, RespuestaResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<ICollection<RespuestaResponse>>> GetRespuestasAsync()
    {
        try
        {
            var respuestas = await _respuestaRepository.GetRespuestasAsync();

            var respuestasDto = new List<RespuestaResponse>();

            foreach (var Respuesta in respuestas)
            {
                respuestasDto.Add(_mapper.Map<RespuestaResponse>(Respuesta));
            }
            return Ok<ICollection<RespuestaResponse>>(respuestasDto);
        }
        catch (Exception ex)
        {
            return Error<Respuesta, ICollection<RespuestaResponse>>(GexErrorMessage.Generic, ex.Message);
        }
    }

    /*
public Task<GexResult<RespuestaResponse>> PrepareCreateRespuestaAsync(RespuestaCreateRequest request)
{
    return Ok<RespuestaResponse>();
    if (!await _preguntaRepository.ExistsPreguntaAsync(request.PreguntaId))
        return KeyError<Pregunta, RespuestaResponse>(nameof(request.PreguntaId), GexErrorMessage.NotFound);

    switch (request.Tipo)
    {
        case PreguntaTipo.TEXTO:
            return Ok<RespuestaResponse>();
        case PreguntaTipo.CERRADA:
            if(request.RespuestaCorrecta != 0 && request.RespuestaCorrecta != 1)
                return KeyError<RespuestaResponse>(nameof(request.RespuestaCorrecta), "El valor debe estar entre 0 y 1.");
            return await CreateRespuestaAsync(new RespuestaRequest() { Correcto = request.RespuestaCorrecta == 1 ? true : false, Valor = "", PreguntaId = request.PreguntaId });
        case PreguntaTipo.MULTIPLECHOICE:
            if (request.RespuestaCorrecta <= 0 || request.RespuestaCorrecta < request.Respuestas.Length)
                return KeyError<RespuestaResponse>(nameof(request.RespuestaCorrecta), "No se encontró el índice en la lista de respuestas.");

            foreach (var (respuesta, index) in request.Respuestas.WithIndex())
            {
                var result = await CreateRespuestaAsync(new RespuestaRequest()
                {
                    Correcto = request.RespuestaCorrecta == index,
                    Valor = respuesta,
                    PreguntaId = request.PreguntaId
                });

                if (!result.Success)
                    return result;
            }
            return Ok<RespuestaResponse>();
        case PreguntaTipo.MULTIPLESELECTION:
            if (request.RespuestaCorrecta <= 0 || request.RespuestaCorrecta < request.Respuestas.Length)
                return KeyError<RespuestaResponse>(nameof(request.RespuestaCorrecta), "No se encontró el índice en la lista de respuestas.");

            if(request.RespuestasCorrectas.Any(r => r <= 0 || r >= request.Respuestas.Length))
                return KeyError<RespuestaResponse>(nameof(request.RespuestaCorrecta), "Existe un valor que no existe dentro de las respuestas.");

            foreach (var (respuesta, index) in request.Respuestas.WithIndex())
            {
                var result = await CreateRespuestaAsync(new RespuestaRequest()
                {
                    Correcto = request.RespuestasCorrectas.Contains(index),
                    Valor = respuesta,
                    PreguntaId = request.PreguntaId
                });

                if (!result.Success)
                    return result;
            }
            break;
        default:
            return Error<Respuesta, RespuestaResponse>(GexErrorMessage.Generic);
    }
    return Error<Respuesta, RespuestaResponse>(GexErrorMessage.Generic);
}
    */

    public async Task<GexResult<RespuestaResponse>> CreateOrUpdateRespuestaAsync(RespuestaCreateRequest respuestaDto)
    {
        try
        {
            if (respuestaDto.Respuestas.Length == 0)
                return KeyOk<Pregunta, RespuestaResponse>(nameof(respuestaDto.Respuestas), GexSuccessMessage.NoDataFound);

            var pregunta = await _preguntaRepository.GetPreguntaAsync(respuestaDto.PreguntaId);

            if (pregunta == null)
                return KeyError<Pregunta, RespuestaResponse>(nameof(respuestaDto.PreguntaId), GexErrorMessage.NotFound);

            if (pregunta.Tipo == PreguntaTipo.Texto)
                return Ok<RespuestaResponse>();

            if (pregunta.Tipo == PreguntaTipo.VerdaderoOFalso)
            {
                if (respuestaDto.Respuestas.Count(r => r.Correcto == true && !r.Borrar) > 2)
                    return KeyError<RespuestaResponse>(nameof(respuestaDto.Respuestas), $"No se acepta más de dos respuestas cuando la pregunta es de tipo {pregunta.Tipo.GetDescription()}");

                if (respuestaDto.Respuestas.Count(r => r.Id <= 0 && !r.Borrar) >= 2)
                {
                    var preguntas = await _respuestaRepository.GetRespuestasByPreguntaIdAsync(respuestaDto.PreguntaId);
                    if (preguntas.Count >= 2)
                    {
                        return KeyError<RespuestaResponse>(nameof(respuestaDto.Respuestas), $"No se pueden crear más de dos respuestas cuando la pregunta es de tipo {pregunta.Tipo.GetDescription()}");
                    }
                }
            }

            if (pregunta.Tipo == PreguntaTipo.VerdaderoOFalso || pregunta.Tipo == PreguntaTipo.MultipleChoise)
            {
                if (respuestaDto.Respuestas.Count(r => r.Correcto == true && !r.Borrar) > 1)
                    return KeyError<RespuestaResponse>(nameof(respuestaDto.Respuestas), $"No se acepta más de una respuesta correcta cuando la pregunta es de tipo {pregunta.Tipo.GetDescription()}");
            }
            if (pregunta.Tipo == PreguntaTipo.VerdaderoOFalso || pregunta.Tipo == PreguntaTipo.MultipleChoise || pregunta.Tipo == PreguntaTipo.SeleccionMultiple)
            {
                if (respuestaDto.Respuestas.Count(r => r.Correcto == true && !r.Borrar) == 0)
                    return KeyError<RespuestaResponse>(nameof(respuestaDto.Respuestas), $"No se encontró ninguna respuesta correcta.");
                if (respuestaDto.Respuestas.Count(r => r.Correcto == false && !r.Borrar) == 0)
                    return KeyError<RespuestaResponse>(nameof(respuestaDto.Respuestas), $"Al menos una respuesta debe ser incorrecta.");
            }

            foreach (var rtaDto in respuestaDto.Respuestas)
            {
                var respuesta = _mapper.Map<Respuesta>(rtaDto);
                respuesta.PreguntaId = respuestaDto.PreguntaId;

                if (rtaDto.Id > 0)
                {
                    if (await _respuestaRepository.ExistsRespuestaAsync(rtaDto.Id))
                    {
                        if (rtaDto.Borrar && !await _respuestaRepository.DeleteRespuestaAsync(rtaDto.Id, autosave: false))
                            return KeyError<Respuesta, RespuestaResponse>(nameof(rtaDto.Id), GexErrorMessage.CouldNotDelete, _mapper.Map<RespuestaResponse>(rtaDto));
                        else if (!rtaDto.Borrar && !await _respuestaRepository.UpdateRespuestaAsync(respuesta, autosave: false))
                            return KeyError<Respuesta, RespuestaResponse>(nameof(rtaDto.Id), GexErrorMessage.CouldNotUpdate, _mapper.Map<RespuestaResponse>(rtaDto));
                    }
                    else
                        return KeyError<Respuesta, RespuestaResponse>(nameof(rtaDto.Id), GexErrorMessage.NotFound, _mapper.Map<RespuestaResponse>(rtaDto));
                }
                else
                {
                    if (!await _respuestaRepository.CreateRespuestaAsync(respuesta, autosave: false))
                        return KeyError<Respuesta, RespuestaResponse>(nameof(rtaDto.Id), GexErrorMessage.CouldNotCreate, _mapper.Map<RespuestaResponse>(rtaDto));
                }
            }
            return await _respuestaRepository.Save() ? Ok<Respuesta, RespuestaResponse>(GexSuccessMessage.Created) : Error<Respuesta, RespuestaResponse>(GexErrorMessage.Generic);
            //return Ok<Respuesta, RespuestaResponse>(GexSuccessMessage.Created);
        }
        catch (UniqueConstraintException)
        {
            return Error<Respuesta, RespuestaResponse>(GexErrorMessage.AlreadyExists);
        }
        catch (Exception ex)
        {
            return Error<Respuesta, RespuestaResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }

    public async Task<GexResult<RespuestaResponse>> UpdateRespuestaAsync(RespuestaRequest respuestaDto)
    {
        try
        {
            if (respuestaDto.Id == 0)
                return Error<Respuesta, RespuestaResponse>(GexErrorMessage.InvalidId);

            var respuesta = await _respuestaRepository.GetRespuestaAsync(respuestaDto.Id);

            if (respuesta == null)
                return Error<Respuesta, RespuestaResponse>(GexErrorMessage.NotFound);

            respuesta.Correcto = respuestaDto.Correcto;
            respuesta.Valor = respuestaDto.Valor;

            if (!await _respuestaRepository.UpdateRespuestaAsync(respuesta))
                return Error<Respuesta, RespuestaResponse>(GexErrorMessage.CouldNotUpdate);

            return Ok<Respuesta, RespuestaResponse>(_mapper.Map<RespuestaResponse>(respuesta), GexSuccessMessage.Modified);
        }
        catch (Exception ex)
        {
            return Error<Respuesta, RespuestaResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }

    public async Task<GexResult<ICollection<RespuestaResponse>>> GetRespuestasByPreguntaIdAsync(long preguntaId)
    {
        try
        {
            var respuestas = await _respuestaRepository.GetRespuestasByPreguntaIdAsync(preguntaId);

            var respuestasDto = new List<RespuestaResponse>();

            foreach (var Respuesta in respuestas)
            {
                respuestasDto.Add(_mapper.Map<RespuestaResponse>(Respuesta));
            }
            return Ok<ICollection<RespuestaResponse>>(respuestasDto);
        }
        catch (Exception ex)
        {
            return Error<Respuesta, ICollection<RespuestaResponse>>(GexErrorMessage.Generic, ex.Message);
        }
    }

    public async Task<GexResult<RespuestaResponse>> DeleteRespuestasByPreguntaIdAsync(long preguntaId)
    {
        try
        {
            if (!await _respuestaRepository.DeleteRespuestasByPreguntaIdAsync(preguntaId))
                return Error<Respuesta, RespuestaResponse>(GexErrorMessage.CouldNotDelete);
            return Ok<Respuesta, RespuestaResponse>(GexSuccessMessage.Deleted);
        }
        catch (Exception ex)
        {
            return Error<Respuesta, RespuestaResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
}
