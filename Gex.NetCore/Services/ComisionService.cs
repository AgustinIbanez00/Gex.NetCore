using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using AutoMapper;
using EntityFramework.Exceptions.Common;
using Gex.DTO;
using Gex.Helpers;
using Gex.Models;
using Gex.Repository.Interface;
using Gex.Services.Interface;
using Gex.Utils;

namespace Gex.Services;
public class ComisionService : IComisionService, IGexResponse<ComisionDTO>
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
    public GexResponse<ICollection<ComisionDTO>> Collection(GexErrorMessage message) => GexResponse<ICollection<ComisionDTO>>.ErrorF(message, _options);
    public GexResponse<ComisionDTO> Success(GexSuccessMessage message) => GexResponse<ComisionDTO>.Ok(message, _options);
    public GexResponse<ComisionDTO> Error(GexErrorMessage error, [Optional] string message) => GexResponse<ComisionDTO>.ErrorF(error, _options, message);
    public GexResponse<ICollection<ComisionDTO>> CollectionMessage(GexErrorMessage error, [Optional] string message) => GexResponse<ICollection<ComisionDTO>>.ErrorF(error, _options, message);
    public GexResponse<ComisionDTO> Data(ComisionDTO data, GexSuccessMessage gexSuccess) => GexResponse<ComisionDTO>.Ok(data, gexSuccess, _options);
    public GexResponse<ComisionDTO> Data(ComisionDTO data) => GexResponse<ComisionDTO>.Ok(data);
    public GexResponse<ICollection<ComisionDTO>> OkCollection(ICollection<ComisionDTO> data) => GexResponse<ICollection<ComisionDTO>>.Ok(data);

    //***************************************//
    public async Task<GexResponse<ComisionDTO>> CreateComisionAsync(ComisionDTO comisionDTO)
    {
        try
        {
            if (await _repository.ExistsComisionAsync(comisionDTO.Id))
                return Error(GexErrorMessage.AlreadyExists);

            var comision = _mapper.Map<Comision>(comisionDTO);
            if (!await _repository.CreateComisionAsync(comision))
                return Error(GexErrorMessage.CouldNotCreate);

            var dto = _mapper.Map<ComisionDTO>(comision);
            return GexResponse<ComisionDTO>.Ok(dto, GexSuccessMessage.Created, _options);
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
    public async Task<GexResponse<ComisionDTO>> DeleteComisionAsync(int id)
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

            return Data(_mapper.Map<ComisionDTO>(comision), GexSuccessMessage.Deleted);
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, $"Detalles: {ex.Message}");
        }
    }
    public async Task<GexResponse<ComisionDTO>> GetComisionAsync(int id)
    {
        try
        {
            var comision = await _repository.GetComisionAsync(id);

            if (comision == null)
                return Error(GexErrorMessage.NotFound);

            return GexResponse<ComisionDTO>.Ok(_mapper.Map<ComisionDTO>(comision));
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<ComisionDTO>> GetComisionAsync(string nombre)
    {
        try
        {
            var comision = await _repository.GetComisionAsync(nombre);

            if (comision == null)
                return Error(GexErrorMessage.NotFound);

            return GexResponse<ComisionDTO>.Ok(_mapper.Map<ComisionDTO>(comision));
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<ICollection<ComisionDTO>>> GetComisionsAsync()
    {
        try
        {
            var comisiones = await _repository.GetComisionsAsync();

            if (comisiones.Count == 0)
                return CollectionMessage(GexErrorMessage.NotFound);

            var comisiontesDTO = new List<ComisionDTO>();

            foreach (var comision in comisiones)
            {
                comisiontesDTO.Add(_mapper.Map<ComisionDTO>(comision));
            }
            return OkCollection(comisiontesDTO);
        }
        catch (Exception ex)
        {
            return CollectionMessage(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<ComisionDTO>> UpdateComisionAsync(ComisionDTO comisionDTO)
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

            return Data(_mapper.Map<ComisionDTO>(comision), GexSuccessMessage.Modified);
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }

}
