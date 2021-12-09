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
using Gex.ViewModels.Response;

namespace Gex.Services;
public class ExamenService : IExamenService, IGexResponse<ExamenRequest>
{
    private readonly IMapper _mapper;
    private readonly IExamenRepository _examenRepository;
    private readonly IMateriaRepository _materiaRepository;
    private readonly GexResponseOptions _options = Examen.Options;

    public ExamenService(IMapper mapper, IExamenRepository examenRepository, IMateriaRepository materiaRepository)
    {
        _mapper = mapper;
        _examenRepository = examenRepository;
        _materiaRepository = materiaRepository;
    }

    //*********** HANDLING ERRORS ***********//
    public GexResponse<ICollection<ExamenRequest>> Collection(GexErrorMessage message) => GexResponse<ICollection<ExamenRequest>>.ErrorF(message, _options);
    public GexResponse<ExamenRequest> Success(GexSuccessMessage message) => GexResponse<ExamenRequest>.Ok(message, _options);
    public GexResponse<ExamenRequest> Error(GexErrorMessage error, [Optional] string message) => GexResponse<ExamenRequest>.ErrorF(error, _options, message);
    public GexResponse<ICollection<ExamenRequest>> CollectionMessage(GexErrorMessage error, [Optional] string message) => GexResponse<ICollection<ExamenRequest>>.ErrorF(error, _options, message);
    public GexResponse<ExamenRequest> Data(ExamenRequest data, GexSuccessMessage gexSuccess) => GexResponse<ExamenRequest>.Ok(data, gexSuccess, _options);
    public GexResponse<ExamenRequest> Data(ExamenRequest data) => GexResponse<ExamenRequest>.Ok(data);
    public GexResponse<ICollection<ExamenRequest>> OkCollection(ICollection<ExamenRequest> data) => GexResponse<ICollection<ExamenRequest>>.Ok(data);

    //***************************************//
    public async Task<GexResponse<ExamenRequest>> CreateExamenAsync(ExamenRequest examenDto)
    {
        try
        {
            if (await _examenRepository.ExistsExamenAsync(examenDto.Id))
                return Error(GexErrorMessage.AlreadyExists);

            var examen = _mapper.Map<Examen>(examenDto);

            examen.Materia = await _materiaRepository.GetMateriaAsync(examenDto.MateriaId);

            if (examen.Materia == null)
                return GexResponse<ExamenRequest>.ErrorF(GexErrorMessage.NotFound, Materia.Options);

            if (!await _examenRepository.CreateExamenAsync(examen))
                return Error(GexErrorMessage.CouldNotCreate);

            var dto = _mapper.Map<ExamenRequest>(examen);
            return Data(dto, GexSuccessMessage.Created);
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
    public async Task<GexResponse<ExamenRequest>> DeleteExamenAsync(int id)
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

            return Data(_mapper.Map<ExamenRequest>(examen), GexSuccessMessage.Deleted);
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<ExamenResponse>> GetExamenAsync(int id)
    {
        try
        {
            var examen = await _examenRepository.GetExamenAsync(id);

            if (examen == null)
                return GexResponse<ExamenResponse>.ErrorF(GexErrorMessage.NotFound, Examen.Options);

            return GexResponse<ExamenResponse>.Ok(_mapper.Map<ExamenResponse>(examen));
        }
        catch (Exception ex)
        {
            return GexResponse<ExamenResponse>.ErrorF(GexErrorMessage.Generic, Examen.Options, ex.Message);
        }
    }
    public async Task<GexResponse<ICollection<ExamenResponse>>> GetExamensAsync()
    {
        try
        {
            var examenes = await _examenRepository.GetExamensAsync();

            if(examenes.Count == 0)
                return GexResponse<ICollection<ExamenResponse>>.ErrorF(GexErrorMessage.NotFound, Examen.Options);

            var examenesDto = new List<ExamenResponse>();

            foreach (var Examen in examenes)
            {
                examenesDto.Add(_mapper.Map<ExamenResponse>(Examen));
            }
            return GexResponse<ICollection<ExamenResponse>>.Ok(examenesDto);
        }
        catch (Exception ex)
        {
            return GexResponse<ICollection<ExamenResponse>>.ErrorF(GexErrorMessage.Generic, Examen.Options, ex.Message);
        }
    }
    public async Task<GexResponse<ExamenRequest>> UpdateExamenAsync(ExamenRequest examenDto)
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

            return Data(_mapper.Map<ExamenRequest>(examen), GexSuccessMessage.Modified);
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
}
