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
public class MesaExamenService : IMesaExamenService, IGexResponse<MesaExamenDTO>
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
    public GexResponse<ICollection<MesaExamenDTO>> Collection(GexErrorMessage message) => GexResponse<ICollection<MesaExamenDTO>>.ErrorF(message, _options);
    public GexResponse<MesaExamenDTO> Success(GexSuccessMessage message) => GexResponse<MesaExamenDTO>.Ok(message, _options);
    public GexResponse<MesaExamenDTO> Error(GexErrorMessage error, [Optional] string message) => GexResponse<MesaExamenDTO>.ErrorF(error, _options, message);
    public GexResponse<ICollection<MesaExamenDTO>> CollectionMessage(GexErrorMessage error, [Optional] string message) => GexResponse<ICollection<MesaExamenDTO>>.ErrorF(error, _options, message);
    public GexResponse<MesaExamenDTO> Data(MesaExamenDTO data, GexSuccessMessage gexSuccess) => GexResponse<MesaExamenDTO>.Ok(data, gexSuccess, _options);
    public GexResponse<MesaExamenDTO> Data(MesaExamenDTO data) => GexResponse<MesaExamenDTO>.Ok(data);
    public GexResponse<ICollection<MesaExamenDTO>> OkCollection(ICollection<MesaExamenDTO> data) => GexResponse<ICollection<MesaExamenDTO>>.Ok(data);

    //***************************************//
    public async Task<GexResponse<MesaExamenDTO>> CreateMesaExamenAsync(MesaExamenDTO mesaExamenDto)
    {
        try
        {
            if (await _mesaExamenRepository.ExistsMesaExamenAsync(mesaExamenDto.Id))
                return Error(GexErrorMessage.AlreadyExists);

            var mesaExamen = _mapper.Map<MesaExamen>(mesaExamenDto);
            if (!await _mesaExamenRepository.CreateMesaExamenAsync(mesaExamen))
                return Error(GexErrorMessage.CouldNotCreate);

            var dto = _mapper.Map<MesaExamenDTO>(mesaExamen);
            return GexResponse<MesaExamenDTO>.Ok(dto, GexSuccessMessage.Created, _options);
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
    public async Task<GexResponse<MesaExamenDTO>> DeleteMesaExamenAsync(int id)
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

            return Data(_mapper.Map<MesaExamenDTO>(mesaExamen), GexSuccessMessage.Deleted);
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<MesaExamenDTO>> GetMesaExamenAsync(int id)
    {
        try
        {
            var mesaExamen = await _mesaExamenRepository.GetMesaExamenAsync(id);

            if (mesaExamen == null)
                return Error(GexErrorMessage.NotFound);

            return GexResponse<MesaExamenDTO>.Ok(_mapper.Map<MesaExamenDTO>(mesaExamen));
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<ICollection<MesaExamenDTO>>> GetMesaExamensAsync()
    {
        try
        {
            var MesaExamenes = await _mesaExamenRepository.GetMesasExamenAsync();

            if (MesaExamenes.Count == 0)
                return CollectionMessage(GexErrorMessage.NotFound);

            var MesaExamentesDTO = new List<MesaExamenDTO>();

            foreach (var MesaExamen in MesaExamenes)
            {
                MesaExamentesDTO.Add(_mapper.Map<MesaExamenDTO>(MesaExamen));
            }
            return OkCollection(MesaExamentesDTO);
        }
        catch (Exception ex)
        {
            return CollectionMessage(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResponse<MesaExamenDTO>> UpdateMesaExamenAsync(MesaExamenDTO mesaExamenDto)
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
                return GexResponse<MesaExamenDTO>.ErrorF(GexErrorMessage.NotFound, Examen.Options);

            mesaExamen.Fecha = mesaExamenDto.Fecha;
            mesaExamen.MostrarRespuestas = mesaExamenDto.MostrarRespuestas;
            mesaExamen.Duracion = mesaExamenDto.Duracion;
            

            if (!await _mesaExamenRepository.UpdateMesaExamenAsync(mesaExamen))
                return Error(GexErrorMessage.CouldNotUpdate);

            return Data(_mapper.Map<MesaExamenDTO>(mesaExamen), GexSuccessMessage.Modified);
        }
        catch (Exception ex)
        {
            return Error(GexErrorMessage.Generic, ex.Message);
        }
    }
}
