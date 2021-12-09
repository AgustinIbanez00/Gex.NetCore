using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using AutoMapper;
using EntityFramework.Exceptions.Common;
using Gex.Helpers;
using Gex.Models;
using Gex.Repository.Interface;
using Gex.Services.Interface;
using Gex.Utils;
using Gex.ViewModels.Request;

namespace Gex.Services;
public class PreguntaService : IPreguntaService, IGexResponse<PreguntaRequest>
{
    private readonly IMapper _mapper;
    private readonly IPreguntaRepository _repository;
    private readonly GexResponseOptions _options = Pregunta.Options;

    public PreguntaService(IMapper mapper, IPreguntaRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    //*********** HANDLING ERRORS ***********//
    public GexResponse<ICollection<PreguntaRequest>> Collection(GexErrorMessage message) => GexResponse<ICollection<PreguntaRequest>>.ErrorF(message, _options);
    public GexResponse<PreguntaRequest> Success(GexSuccessMessage message) => GexResponse<PreguntaRequest>.Ok(message, _options);
    public GexResponse<PreguntaRequest> Error(GexErrorMessage error, [Optional] string message) => GexResponse<PreguntaRequest>.ErrorF(error, _options, message);
    public GexResponse<ICollection<PreguntaRequest>> CollectionMessage(GexErrorMessage error, [Optional] string message) => GexResponse<ICollection<PreguntaRequest>>.ErrorF(error, _options, message);
    public GexResponse<PreguntaRequest> Data(PreguntaRequest data, GexSuccessMessage gexSuccess) => GexResponse<PreguntaRequest>.Ok(data, gexSuccess, _options);
    public GexResponse<PreguntaRequest> Data(PreguntaRequest data) => GexResponse<PreguntaRequest>.Ok(data);
    public GexResponse<ICollection<PreguntaRequest>> OkCollection(ICollection<PreguntaRequest> data) => GexResponse<ICollection<PreguntaRequest>>.Ok(data);

    //***************************************//
    public async Task<GexResponse<PreguntaRequest>> CreatePreguntaAsync(PreguntaRequest preguntaDto)
    {
        try
        {
            if (await _repository.ExistsPreguntaAsync(preguntaDto.Id))
                return Error(GexErrorMessage.AlreadyExists);

            var pregunta = _mapper.Map<Pregunta>(preguntaDto);
            if (!await _repository.CreatePreguntaAsync(pregunta))
                return Error(GexErrorMessage.CouldNotCreate);

            var dto = _mapper.Map<PreguntaRequest>(pregunta);
            return GexResponse<PreguntaRequest>.Ok(dto, GexSuccessMessage.Created, _options);
        }
        catch (UniqueConstraintException)
        {
            return Error(GexErrorMessage.AlreadyExists);
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<PreguntaRequest>> DeletePreguntaAsync(int id)
    {
        try
        {
            var pregunta = await _repository.GetPreguntaAsync(id);
            if (pregunta == null)
                return Error(GexErrorMessage.NotFound);

            if (pregunta.Estado == Estado.BAJA)
                return Error(GexErrorMessage.AlreadyDeleted);

            if (!await _repository.DeletePreguntaAsync(pregunta))
                return Error(GexErrorMessage.CouldNotDelete);

            return Data(_mapper.Map<PreguntaRequest>(pregunta), GexSuccessMessage.Deleted);
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<PreguntaRequest>> GetPreguntaAsync(int id)
    {
        try
        {
            var pregunta = await _repository.GetPreguntaAsync(id);

            if (pregunta == null)
                return Error(GexErrorMessage.NotFound);

            return GexResponse<PreguntaRequest>.Ok(_mapper.Map<PreguntaRequest>(pregunta));
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<ICollection<PreguntaRequest>>> GetPreguntasAsync()
    {
        try
        {
            var Preguntaes = await _repository.GetPreguntasAsync();

            if (Preguntaes.Count == 0)
                return CollectionMessage(GexErrorMessage.NotFound);

            var PreguntatesDTO = new List<PreguntaRequest>();

            foreach (var Pregunta in Preguntaes)
            {
                PreguntatesDTO.Add(_mapper.Map<PreguntaRequest>(Pregunta));
            }
            return OkCollection(PreguntatesDTO);
        }
        catch (Exception ex)
        {
            return CollectionMessage(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<PreguntaRequest>> UpdatePreguntaAsync(PreguntaRequest preguntaDto)
    {
        try
        {
            if (preguntaDto.Id == 0)
                return Error(GexErrorMessage.InvalidId);

            var pregunta = await _repository.GetPreguntaAsync(preguntaDto.Id);

            if (pregunta == null)
                return Error(GexErrorMessage.NotFound);

            pregunta.Periodo = preguntaDto.Periodo;
            pregunta.Descripcion = preguntaDto.Descripcion;
            pregunta.Tipo = preguntaDto.Tipo;

            if (!await _repository.UpdatePreguntaAsync(pregunta))
                return Error(GexErrorMessage.CouldNotUpdate);

            return Data(_mapper.Map<PreguntaRequest>(pregunta), GexSuccessMessage.Modified);
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
}
