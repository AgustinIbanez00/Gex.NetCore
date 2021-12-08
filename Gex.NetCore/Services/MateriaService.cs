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
public class MateriaService : IMateriaService, IGexResponse<MateriaDTO>
{
    private readonly IMapper _mapper;
    private readonly IMateriaRepository _repository;
    private readonly GexResponseOptions _options;

    public MateriaService(IMapper mapper, IMateriaRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
        _options = new GexResponseOptions()
        {
            Entity = "materia",
            Gender = Gender.FEMALE
        };
    }

    //*********** HANDLING ERRORS ***********//
    public GexResponse<ICollection<MateriaDTO>> Collection(GexErrorMessage message) => GexResponse<ICollection<MateriaDTO>>.ErrorF(message, _options);
    public GexResponse<MateriaDTO> Success(GexSuccessMessage message) => GexResponse<MateriaDTO>.Ok(message, _options);
    public GexResponse<MateriaDTO> Error(GexErrorMessage error, [Optional] string message) => GexResponse<MateriaDTO>.ErrorF(error, _options, message);
    public GexResponse<ICollection<MateriaDTO>> CollectionMessage(GexErrorMessage error, [Optional] string message) => GexResponse<ICollection<MateriaDTO>>.ErrorF(error, _options, message);
    public GexResponse<MateriaDTO> Data(MateriaDTO data, GexSuccessMessage gexSuccess) => GexResponse<MateriaDTO>.Ok(data, gexSuccess, _options);
    public GexResponse<MateriaDTO> Data(MateriaDTO data) => GexResponse<MateriaDTO>.Ok(data);
    public GexResponse<ICollection<MateriaDTO>> OkCollection(ICollection<MateriaDTO> data) => GexResponse<ICollection<MateriaDTO>>.Ok(data);

    //***************************************//
    public async Task<GexResponse<MateriaDTO>> CreateMateriaAsync(MateriaDTO materiaDto)
    {
        try
        {
            if (await _repository.ExistsMateriaAsync(materiaDto.Id))
                return Error(GexErrorMessage.AlreadyExists);

            var materia = _mapper.Map<Materia>(materiaDto);
            if (!await _repository.CreateMateriaAsync(materia))
                return Error(GexErrorMessage.CouldNotCreate);

            var dto = _mapper.Map<MateriaDTO>(materia);
            return GexResponse<MateriaDTO>.Ok(dto, GexSuccessMessage.Created, _options);
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
    public async Task<GexResponse<MateriaDTO>> DeleteMateriaAsync(int id)
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

            return Data(_mapper.Map<MateriaDTO>(materia), GexSuccessMessage.Deleted);
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<MateriaDTO>> GetMateriaAsync(int id)
    {
        try
        {
            var materia = await _repository.GetMateriaAsync(id);

            if (materia == null)
                return Error(GexErrorMessage.NotFound);

            return GexResponse<MateriaDTO>.Ok(_mapper.Map<MateriaDTO>(materia));
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<ICollection<MateriaDTO>>> GetMateriasAsync()
    {
        try
        {
            var Materiaes = await _repository.GetMateriasAsync();

            if (Materiaes.Count == 0)
                return CollectionMessage(GexErrorMessage.NotFound);

            var MateriatesDTO = new List<MateriaDTO>();

            foreach (var Materia in Materiaes)
            {
                MateriatesDTO.Add(_mapper.Map<MateriaDTO>(Materia));
            }
            return OkCollection(MateriatesDTO);
        }
        catch (Exception ex)
        {
            return CollectionMessage(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<MateriaDTO>> UpdateMateriaAsync(MateriaDTO materiaDto)
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

            return Data(_mapper.Map<MateriaDTO>(materia), GexSuccessMessage.Modified);
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
}
