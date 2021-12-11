using System.Collections.Generic;
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

public class ComisionService : IComisionService
{
    private readonly IMapper _mapper;
    private readonly IComisionRepository _repository;

    public ComisionService(IMapper mapper, IComisionRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<GexResult<ComisionResponse>> CreateComisionAsync(ComisionRequest comisionDto)
    {
        try
        {
            if (await _repository.ExistsComisionAsync(comisionDto.Id))
                return Error<Comision, ComisionResponse>(GexErrorMessage.AlreadyExists);

            var comision = _mapper.Map<Comision>(comisionDto);
            if (!await _repository.CreateComisionAsync(comision))
                return Error<Comision, ComisionResponse>(GexErrorMessage.CouldNotCreate);

            var dto = _mapper.Map<ComisionResponse>(comision);
            return Ok<Comision, ComisionResponse>(dto, GexSuccessMessage.Created);
        }
        catch (UniqueConstraintException)
        {
            return Error<Comision, ComisionResponse>(GexErrorMessage.AlreadyExists);
        }
        catch (Exception ex)
        {
            return Error<Comision, ComisionResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<ComisionResponse>> DeleteComisionAsync(long id)
    {
        try
        {
            var comision = await _repository.GetComisionAsync(id);
            if (comision == null)
                return Error<Comision, ComisionResponse>(GexErrorMessage.NotFound);

            if (comision.Estado == Estado.BAJA)
                return Error<Comision, ComisionResponse>(GexErrorMessage.AlreadyDeleted);

            if (!await _repository.DeleteComisionAsync(comision))
                return Error<Comision, ComisionResponse>(GexErrorMessage.CouldNotDelete);

            return Ok<Comision, ComisionResponse>(_mapper.Map<ComisionResponse>(comision), GexSuccessMessage.Deleted);
        }
        catch (Exception ex)
        {
            return Error<Comision, ComisionResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<ComisionResponse>> GetComisionAsync(long id)
    {
        try
        {
            var comision = await _repository.GetComisionAsync(id);

            if (comision == null)
                return Error<Comision, ComisionResponse>(GexErrorMessage.NotFound);

            return Ok(_mapper.Map<ComisionResponse>(comision));
        }
        catch (Exception ex)
        {
            return Error<Comision, ComisionResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }

    public async Task<GexResult<ComisionResponse>> GetComisionAsync(string nombre)
    {
        try
        {
            var comision = await _repository.GetComisionAsync(nombre);

            if (comision == null)
                return Error<Comision, ComisionResponse>(GexErrorMessage.NotFound);

            return Ok(_mapper.Map<ComisionResponse>(comision));
        }
        catch (Exception ex)
        {
            return Error<Comision, ComisionResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }

    public async Task<GexResult<ICollection<ComisionResponse>>> GetComisionsAsync()
    {
        try
        {
            var comisions = await _repository.GetComisionsAsync();

            if (comisions.Count == 0)
                return Error<Comision, ICollection<ComisionResponse>>(GexErrorMessage.NotFound);

            var comisionsDto = new List<ComisionResponse>();

            foreach (var Comision in comisions)
            {
                comisionsDto.Add(_mapper.Map<ComisionResponse>(Comision));
            }
            return Ok<ICollection<ComisionResponse>>(comisionsDto);
        }
        catch (Exception ex)
        {
            return Error<Comision, ICollection<ComisionResponse>>(GexErrorMessage.Generic, ex.Message);
        }
    }

    public async Task<GexResult<ComisionResponse>> UpdateComisionAsync(ComisionRequest comisionDto)
    {
        try
        {
            if (comisionDto.Id == 0)
                return Error<Comision, ComisionResponse>(GexErrorMessage.InvalidId);

            var comision = await _repository.GetComisionAsync(comisionDto.Id);

            if (comision == null)
                return Error<Comision, ComisionResponse>(GexErrorMessage.NotFound);

            comision.Nombre = comisionDto.Nombre;
            comision.CicloLectivo = comisionDto.CicloLectivo;

            if (!await _repository.UpdateComisionAsync(comision))
                return Error<Comision, ComisionResponse>(GexErrorMessage.CouldNotUpdate);

            return Ok<Comision, ComisionResponse>(_mapper.Map<ComisionResponse>(comision), GexSuccessMessage.Modified);
        }
        catch (Exception ex)
        {
            return Error<Comision, ComisionResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
}
