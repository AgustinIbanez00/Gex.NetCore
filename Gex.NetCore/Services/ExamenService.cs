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
public class ExamenService : IExamenService, IGexResponse<ExamenDTO>
{
    private readonly IMapper _mapper;
    private readonly IExamenRepository _examenRepository;
    private readonly IMateriaRepository _materiaRepository;
    private readonly GexResponseOptions _options;

    public ExamenService(IMapper mapper, IExamenRepository examenRepository, IMateriaRepository materiaRepository)
    {
        _mapper = mapper;
        _examenRepository = examenRepository;
        _materiaRepository = materiaRepository;
        _options = new GexResponseOptions()
        {
            Entity = "exámen",
            Gender = Gender.MALE
        };
    }

    //*********** HANDLING ERRORS ***********//
    public GexResponse<ICollection<ExamenDTO>> Collection(GexErrorMessage message) => GexResponse<ICollection<ExamenDTO>>.ErrorF(message, _options);
    public GexResponse<ExamenDTO> Success(GexSuccessMessage message) => GexResponse<ExamenDTO>.Ok(message, _options);
    public GexResponse<ExamenDTO> Error(GexErrorMessage error, [Optional] string message) => GexResponse<ExamenDTO>.ErrorF(error, _options, message);
    public GexResponse<ICollection<ExamenDTO>> CollectionMessage(GexErrorMessage error, [Optional] string message) => GexResponse<ICollection<ExamenDTO>>.ErrorF(error, _options, message);
    public GexResponse<ExamenDTO> Data(ExamenDTO data, GexSuccessMessage gexSuccess) => GexResponse<ExamenDTO>.Ok(data, gexSuccess, _options);
    public GexResponse<ExamenDTO> Data(ExamenDTO data) => GexResponse<ExamenDTO>.Ok(data);
    public GexResponse<ICollection<ExamenDTO>> OkCollection(ICollection<ExamenDTO> data) => GexResponse<ICollection<ExamenDTO>>.Ok(data);

    //***************************************//
    public async Task<GexResponse<ExamenDTO>> CreateExamenAsync(ExamenDTO examenDto)
    {
        try
        {
            if (await _examenRepository.ExistsExamenAsync(examenDto.Id))
                return Error(GexErrorMessage.AlreadyExists);

            var examen = _mapper.Map<Examen>(examenDto);
            if (!await _examenRepository.CreateExamenAsync(examen))
                return Error(GexErrorMessage.CouldNotCreate);

            var dto = _mapper.Map<ExamenDTO>(examen);
            return GexResponse<ExamenDTO>.Ok(dto, GexSuccessMessage.Created, _options);
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
    public async Task<GexResponse<ExamenDTO>> DeleteExamenAsync(int id)
    {
        try
        {
            var examen = await _examenRepository.GetExamenAsync(id);
            if (examen == null)
                return Error(GexErrorMessage.NotFound);

            if (examen.Estado == Estado.BAJA)
                return Error(GexErrorMessage.AlreadyDeleted);

            if (!await _examenRepository.DeleteExamenAsync(examen))
                return Error(GexErrorMessage.CouldNotDelete);

            return Data(_mapper.Map<ExamenDTO>(examen), GexSuccessMessage.Deleted);
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<ExamenDTO>> GetExamenAsync(int id)
    {
        try
        {
            var examen = await _examenRepository.GetExamenAsync(id);

            if (examen == null)
                return Error(GexErrorMessage.NotFound);

            return GexResponse<ExamenDTO>.Ok(_mapper.Map<ExamenDTO>(examen));
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<ICollection<ExamenDTO>>> GetExamensAsync()
    {
        try
        {
            var examenes = await _examenRepository.GetExamensAsync();

            if(examenes.Count == 0)
                return CollectionMessage(GexErrorMessage.NotFound);

            var examenesDto = new List<ExamenDTO>();

            foreach (var Examen in examenes)
            {
                examenesDto.Add(_mapper.Map<ExamenDTO>(Examen));
            }
            return OkCollection(examenesDto);
        }
        catch (Exception ex)
        {
            return CollectionMessage(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<ExamenDTO>> UpdateExamenAsync(ExamenDTO examenDto)
    {
        try
        {
            if (examenDto.Id == 0)
                return Error(GexErrorMessage.InvalidId);

            var examen = await _examenRepository.GetExamenAsync(examenDto.Id);

            if (examen == null)
                return Error(GexErrorMessage.NotFound);

            examen.Tipo = examenDto.Tipo;
            examen.NotaRegular = examenDto.NotaRegular;
            examen.NotaPromo = examenDto.NotaPromo;
            examen.Recuperatorio = examenDto.Recuperatorio;

            if (!await _examenRepository.UpdateExamenAsync(examen))
                return Error(GexErrorMessage.CouldNotUpdate);

            return Data(_mapper.Map<ExamenDTO>(examen), GexSuccessMessage.Modified);
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
}
