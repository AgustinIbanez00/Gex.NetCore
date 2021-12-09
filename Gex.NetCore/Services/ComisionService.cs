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
public class ComisionService : IComisionService, IGexResponse<ComisionRequest>
{
    private readonly IMapper _mapper;
    private readonly IComisionRepository _repository;
    private readonly GexResponseOptions _options = Comision.Options;

    public ComisionService(IMapper mapper, IComisionRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    //*********** HANDLING ERRORS ***********//
    public GexResponse<ICollection<ComisionRequest>> Collection(GexErrorMessage message) => GexResponse<ICollection<ComisionRequest>>.ErrorF(message, _options);
    public GexResponse<ComisionRequest> Success(GexSuccessMessage message) => GexResponse<ComisionRequest>.Ok(message, _options);
    public GexResponse<ComisionRequest> Error(GexErrorMessage error, [Optional] string message) => GexResponse<ComisionRequest>.ErrorF(error, _options, message);
    public GexResponse<ICollection<ComisionRequest>> CollectionMessage(GexErrorMessage error, [Optional] string message) => GexResponse<ICollection<ComisionRequest>>.ErrorF(error, _options, message);
    public GexResponse<ComisionRequest> Data(ComisionRequest data, GexSuccessMessage gexSuccess) => GexResponse<ComisionRequest>.Ok(data, gexSuccess, _options);
    public GexResponse<ComisionRequest> Data(ComisionRequest data) => GexResponse<ComisionRequest>.Ok(data);
    public GexResponse<ICollection<ComisionRequest>> OkCollection(ICollection<ComisionRequest> data) => GexResponse<ICollection<ComisionRequest>>.Ok(data);

    //***************************************//
    public async Task<GexResponse<ComisionRequest>> CreateComisionAsync(ComisionRequest comisionDTO)
    {
        try
        {
            if (await _repository.ExistsComisionAsync(comisionDTO.Id))
                return Error(GexErrorMessage.AlreadyExists);

            var comision = _mapper.Map<Comision>(comisionDTO);
            if (!await _repository.CreateComisionAsync(comision))
                return Error(GexErrorMessage.CouldNotCreate);

            var dto = _mapper.Map<ComisionRequest>(comision);
            return GexResponse<ComisionRequest>.Ok(dto, GexSuccessMessage.Created, _options);
        }
        catch (UniqueConstraintException)
        {
            return Error(GexErrorMessage.AlreadyExists);
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, $"Detalles: {ex.Message}");
        }
    }
    public async Task<GexResponse<ComisionRequest>> DeleteComisionAsync(int id)
    {
        try
        {
            var comision = await _repository.GetComisionAsync(id);
            if (comision == null)
                return Error(GexErrorMessage.NotFound);

            if (comision.Estado == Estado.BAJA)
                return Error(GexErrorMessage.AlreadyDeleted);

            if (!await _repository.DeleteComisionAsync(comision))
                return Error(GexErrorMessage.CouldNotDelete);

            return Data(_mapper.Map<ComisionRequest>(comision), GexSuccessMessage.Deleted);
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, $"Detalles: {ex.Message}");
        }
    }
    public async Task<GexResponse<ComisionRequest>> GetComisionAsync(int id)
    {
        try
        {
            var comision = await _repository.GetComisionAsync(id);

            if (comision == null)
                return Error(GexErrorMessage.NotFound);

            return GexResponse<ComisionRequest>.Ok(_mapper.Map<ComisionRequest>(comision));
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<ComisionRequest>> GetComisionAsync(string nombre)
    {
        try
        {
            var comision = await _repository.GetComisionAsync(nombre);

            if (comision == null)
                return Error(GexErrorMessage.NotFound);

            return GexResponse<ComisionRequest>.Ok(_mapper.Map<ComisionRequest>(comision));
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<ICollection<ComisionRequest>>> GetComisionsAsync()
    {
        try
        {
            var comisiones = await _repository.GetComisionsAsync();

            if (comisiones.Count == 0)
                return CollectionMessage(GexErrorMessage.NotFound);

            var comisiontesDTO = new List<ComisionRequest>();

            foreach (var comision in comisiones)
            {
                comisiontesDTO.Add(_mapper.Map<ComisionRequest>(comision));
            }
            return OkCollection(comisiontesDTO);
        }
        catch (Exception ex)
        {
            return CollectionMessage(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<ComisionRequest>> UpdateComisionAsync(ComisionRequest comisionDTO)
    {
        try
        {
            if (comisionDTO.Id == 0)
                return Error(GexErrorMessage.InvalidId);

            var comision = await _repository.GetComisionAsync(comisionDTO.Id);

            if (comision == null)
                return Error(GexErrorMessage.NotFound);

            comision.Nombre = comisionDTO.Nombre;
            comision.CicloLectivo = comisionDTO.CicloLectivo;

            if (!await _repository.UpdateComisionAsync(comision))
                return Error(GexErrorMessage.CouldNotUpdate);

            return Data(_mapper.Map<ComisionRequest>(comision), GexSuccessMessage.Modified);
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }

}
