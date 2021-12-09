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
public class MesaExamenService : IMesaExamenService, IGexResponse<MesaExamenRequest>
{
    private readonly IMapper _mapper;
    private readonly IMesaExamenRepository _mesaExamenRepository;
    private readonly IExamenRepository _examenRepository;
    private readonly GexResponseOptions _options = MesaExamen.Options;

    public MesaExamenService(IMapper mapper, IMesaExamenRepository mesaExamenRepository, IExamenRepository examenRepository)
    {
        _mapper = mapper;
        _mesaExamenRepository = mesaExamenRepository;
        _examenRepository = examenRepository;

    }

    //*********** HANDLING ERRORS ***********//
    public GexResponse<ICollection<MesaExamenRequest>> Collection(GexErrorMessage message) => GexResponse<ICollection<MesaExamenRequest>>.ErrorF(message, _options);
    public GexResponse<MesaExamenRequest> Success(GexSuccessMessage message) => GexResponse<MesaExamenRequest>.Ok(message, _options);
    public GexResponse<MesaExamenRequest> Error(GexErrorMessage error, [Optional] string message) => GexResponse<MesaExamenRequest>.ErrorF(error, _options, message);
    public GexResponse<ICollection<MesaExamenRequest>> CollectionMessage(GexErrorMessage error, [Optional] string message) => GexResponse<ICollection<MesaExamenRequest>>.ErrorF(error, _options, message);
    public GexResponse<MesaExamenRequest> Data(MesaExamenRequest data, GexSuccessMessage gexSuccess) => GexResponse<MesaExamenRequest>.Ok(data, gexSuccess, _options);
    public GexResponse<MesaExamenRequest> Data(MesaExamenRequest data) => GexResponse<MesaExamenRequest>.Ok(data);
    public GexResponse<ICollection<MesaExamenRequest>> OkCollection(ICollection<MesaExamenRequest> data) => GexResponse<ICollection<MesaExamenRequest>>.Ok(data);

    //***************************************//
    public async Task<GexResponse<MesaExamenRequest>> CreateMesaExamenAsync(MesaExamenRequest mesaExamenDto)
    {
        try
        {
            if (await _mesaExamenRepository.ExistsMesaExamenAsync(mesaExamenDto.Id))
                return Error(GexErrorMessage.AlreadyExists);

            var mesaExamen = _mapper.Map<MesaExamen>(mesaExamenDto);

            mesaExamen.Examen = await _examenRepository.GetExamenAsync(mesaExamenDto.ExamenId);

            if (mesaExamen.Examen == null)
                return GexResponse<MesaExamenRequest>.ErrorF(GexErrorMessage.NotFound, Examen.Options);

            mesaExamen.Profesor = null;

            if (!await _mesaExamenRepository.CreateMesaExamenAsync(mesaExamen))
                return Error(GexErrorMessage.CouldNotCreate);

            var dto = _mapper.Map<MesaExamenRequest>(mesaExamen);
            return GexResponse<MesaExamenRequest>.Ok(dto, GexSuccessMessage.Created, _options);
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
    public async Task<GexResponse<MesaExamenRequest>> DeleteMesaExamenAsync(int id)
    {
        try
        {
            var mesaExamen = await _mesaExamenRepository.GetMesaExamenAsync(id);
            if (mesaExamen == null)
                return Error(GexErrorMessage.NotFound);

            if (mesaExamen.Estado == Estado.BAJA)
                return Error(GexErrorMessage.AlreadyDeleted);

            if (!await _mesaExamenRepository.DeleteMesaExamenAsync(mesaExamen))
                return Error(GexErrorMessage.CouldNotDelete);

            return Data(_mapper.Map<MesaExamenRequest>(mesaExamen), GexSuccessMessage.Deleted);
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<MesaExamenRequest>> GetMesaExamenAsync(int id)
    {
        try
        {
            var mesaExamen = await _mesaExamenRepository.GetMesaExamenAsync(id);

            if (mesaExamen == null)
                return Error(GexErrorMessage.NotFound);

            return GexResponse<MesaExamenRequest>.Ok(_mapper.Map<MesaExamenRequest>(mesaExamen));
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<ICollection<MesaExamenRequest>>> GetMesaExamensAsync()
    {
        try
        {
            var mesaExamenes = await _mesaExamenRepository.GetMesasExamenAsync();

            if (mesaExamenes.Count == 0)
                return CollectionMessage(GexErrorMessage.NotFound);

            var mesaExamenesDto = new List<MesaExamenRequest>();

            foreach (var MesaExamen in mesaExamenes)
            {
                mesaExamenesDto.Add(_mapper.Map<MesaExamenRequest>(MesaExamen));
            }
            return OkCollection(mesaExamenesDto);
        }
        catch (Exception ex)
        {
            return CollectionMessage(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<MesaExamenRequest>> UpdateMesaExamenAsync(MesaExamenRequest mesaExamenDto)
    {
        try
        {
            if (mesaExamenDto.Id == 0)
                return Error(GexErrorMessage.InvalidId);

            var mesaExamen = await _mesaExamenRepository.GetMesaExamenAsync(mesaExamenDto.Id);

            if (mesaExamen == null)
                return Error(GexErrorMessage.NotFound);

            mesaExamen.Examen = await _examenRepository.GetExamenAsync(mesaExamenDto.ExamenId);

            if (mesaExamen.Examen == null)
                return GexResponse<MesaExamenRequest>.ErrorF(GexErrorMessage.NotFound, Examen.Options);

            mesaExamen.Fecha = mesaExamenDto.Fecha;
            mesaExamen.MostrarRespuestas = mesaExamenDto.MostrarRespuestas;
            mesaExamen.Duracion = mesaExamenDto.Duracion;
            

            if (!await _mesaExamenRepository.UpdateMesaExamenAsync(mesaExamen))
                return Error(GexErrorMessage.CouldNotUpdate);

            return Data(_mapper.Map<MesaExamenRequest>(mesaExamen), GexSuccessMessage.Modified);
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
}
