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
public class MateriaService : IMateriaService, IGexResponse<MateriaRequest>
{
    private readonly IMapper _mapper;
    private readonly IMateriaRepository _repository;
    private readonly GexResponseOptions _options = Materia.Options;

    public MateriaService(IMapper mapper, IMateriaRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    //*********** HANDLING ERRORS ***********//
    public GexResponse<ICollection<MateriaRequest>> Collection(GexErrorMessage message) => GexResponse<ICollection<MateriaRequest>>.ErrorF(message, _options);
    public GexResponse<MateriaRequest> Success(GexSuccessMessage message) => GexResponse<MateriaRequest>.Ok(message, _options);
    public GexResponse<MateriaRequest> Error(GexErrorMessage error, [Optional] string message) => GexResponse<MateriaRequest>.ErrorF(error, _options, message);
    public GexResponse<ICollection<MateriaRequest>> CollectionMessage(GexErrorMessage error, [Optional] string message) => GexResponse<ICollection<MateriaRequest>>.ErrorF(error, _options, message);
    public GexResponse<MateriaRequest> Data(MateriaRequest data, GexSuccessMessage gexSuccess) => GexResponse<MateriaRequest>.Ok(data, gexSuccess, _options);
    public GexResponse<MateriaRequest> Data(MateriaRequest data) => GexResponse<MateriaRequest>.Ok(data);
    public GexResponse<ICollection<MateriaRequest>> OkCollection(ICollection<MateriaRequest> data) => GexResponse<ICollection<MateriaRequest>>.Ok(data);

    //***************************************//
    public async Task<GexResponse<MateriaRequest>> CreateMateriaAsync(MateriaRequest materiaDto)
    {
        try
        {
            if (await _repository.ExistsMateriaAsync(materiaDto.Id))
                return Error(GexErrorMessage.AlreadyExists);

            var materia = _mapper.Map<Materia>(materiaDto);
            if (!await _repository.CreateMateriaAsync(materia))
                return Error(GexErrorMessage.CouldNotCreate);

            var dto = _mapper.Map<MateriaRequest>(materia);
            return GexResponse<MateriaRequest>.Ok(dto, GexSuccessMessage.Created, _options);
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
    public async Task<GexResponse<MateriaRequest>> DeleteMateriaAsync(int id)
    {
        try
        {
            var materia = await _repository.GetMateriaAsync(id);
            if (materia == null)
                return Error(GexErrorMessage.NotFound);

            if (materia.Estado == Estado.BAJA)
                return Error(GexErrorMessage.AlreadyDeleted);

            if (!await _repository.DeleteMateriaAsync(materia))
                return Error(GexErrorMessage.CouldNotDelete);

            return Data(_mapper.Map<MateriaRequest>(materia), GexSuccessMessage.Deleted);
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<MateriaRequest>> GetMateriaAsync(int id)
    {
        try
        {
            var materia = await _repository.GetMateriaAsync(id);

            if (materia == null)
                return Error(GexErrorMessage.NotFound);

            return GexResponse<MateriaRequest>.Ok(_mapper.Map<MateriaRequest>(materia));
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<ICollection<MateriaRequest>>> GetMateriasAsync()
    {
        try
        {
            var Materiaes = await _repository.GetMateriasAsync();

            if (Materiaes.Count == 0)
                return CollectionMessage(GexErrorMessage.NotFound);

            var MateriatesDTO = new List<MateriaRequest>();

            foreach (var Materia in Materiaes)
            {
                MateriatesDTO.Add(_mapper.Map<MateriaRequest>(Materia));
            }
            return OkCollection(MateriatesDTO);
        }
        catch (Exception ex)
        {
            return CollectionMessage(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<MateriaRequest>> UpdateMateriaAsync(MateriaRequest materiaDto)
    {
        try
        {
            if (materiaDto.Id == 0)
                return Error(GexErrorMessage.InvalidId);

            var materia = await _repository.GetMateriaAsync(materiaDto.Id);

            if (materia == null)
                return Error(GexErrorMessage.NotFound);

            materia.Nombre = materiaDto.Nombre;
            materia.Tipo = materiaDto.Tipo;

            if (!await _repository.UpdateMateriaAsync(materia))
                return Error(GexErrorMessage.CouldNotUpdate);

            return Data(_mapper.Map<MateriaRequest>(materia), GexSuccessMessage.Modified);
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
}
