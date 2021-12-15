using System.Collections.Generic;
using System.Runtime.InteropServices;
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

public class InscripcionMesaService : IInscripcionMesaService
{
    private readonly IMapper _mapper;
    private readonly IInscripcionMesaRepository _inscripcionMesaRepository;
    private readonly IMesaExamenRepository _mesaExamenRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public InscripcionMesaService(IMapper mapper, IInscripcionMesaRepository inscripcionMesaRepository, IMesaExamenRepository mesaExamenRepository, IUsuarioRepository usuarioRepository)
    {
        _mapper = mapper;
        _inscripcionMesaRepository = inscripcionMesaRepository;
        _mesaExamenRepository = mesaExamenRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<GexResult<InscripcionMesaResponse>> CreateInscripcionMesaAsync(InscripcionMesaRequest inscripcionMesaDto)
    {
        try
        {
            if (await _inscripcionMesaRepository.ExistsInscripcionMesaAsync(inscripcionMesaDto.Id))
                return Error<InscripcionMesa, InscripcionMesaResponse>(GexErrorMessage.AlreadyExists);

            var inscripcionMesa = _mapper.Map<InscripcionMesa>(inscripcionMesaDto);

            inscripcionMesa.MesaExamen = await _mesaExamenRepository.GetMesaExamenAsync(inscripcionMesaDto.MesaExamenId);

            if (inscripcionMesa.MesaExamen == null)
                return KeyError<MesaExamen, InscripcionMesaResponse>(nameof(inscripcionMesaDto.MesaExamenId), GexErrorMessage.NotFound);

            inscripcionMesa.Alumno = await _usuarioRepository.GetUsuarioAsync(inscripcionMesaDto.UsuarioId);

            if (inscripcionMesa.Alumno == null)
                return KeyError<Usuario, InscripcionMesaResponse>(nameof(inscripcionMesaDto.UsuarioId), GexErrorMessage.NotFound);

            if(inscripcionMesa.Alumno.Tipo != UsuarioTipo.Alumno)
                return KeyError<InscripcionMesaResponse>(nameof(inscripcionMesaDto.UsuarioId), "Sólo se pueden inscribir alumnos en una mesa de exámen.");

            if (!await _inscripcionMesaRepository.CreateInscripcionMesaAsync(inscripcionMesa))
                return Error<InscripcionMesa, InscripcionMesaResponse>(GexErrorMessage.CouldNotCreate);

            var dto = _mapper.Map<InscripcionMesaResponse>(inscripcionMesa);
            return Ok<InscripcionMesa, InscripcionMesaResponse>(dto, GexSuccessMessage.Created);
        }
        catch (UniqueConstraintException)
        {
            return Error<InscripcionMesa, InscripcionMesaResponse>(GexErrorMessage.AlreadyExists);
        }
        catch (Exception ex)
        {
            return Error<InscripcionMesa, InscripcionMesaResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<InscripcionMesaResponse>> DeleteInscripcionMesaAsync(long id)
    {
        try
        {
            var inscripcionMesa = await _inscripcionMesaRepository.GetInscripcionMesaAsync(id);
            if (inscripcionMesa == null)
                return Error<InscripcionMesa, InscripcionMesaResponse>(GexErrorMessage.NotFound);

            if (inscripcionMesa.Estado == Estado.BAJA)
                return Error<InscripcionMesa, InscripcionMesaResponse>(GexErrorMessage.AlreadyDeleted);

            if (!await _inscripcionMesaRepository.DeleteInscripcionMesaAsync(inscripcionMesa))
                return Error<InscripcionMesa, InscripcionMesaResponse>(GexErrorMessage.CouldNotDelete);

            return Ok<InscripcionMesa, InscripcionMesaResponse>(_mapper.Map<InscripcionMesaResponse>(inscripcionMesa), GexSuccessMessage.Deleted);
        }
        catch (Exception ex)
        {
            return Error<InscripcionMesa, InscripcionMesaResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<InscripcionMesaResponse>> GetInscripcionMesaAsync(long id)
    {
        try
        {
            var inscripcionMesa = await _inscripcionMesaRepository.GetInscripcionMesaAsync(id);

            if (inscripcionMesa == null)
                return Error<InscripcionMesa, InscripcionMesaResponse>(GexErrorMessage.NotFound);

            return Ok(_mapper.Map<InscripcionMesaResponse>(inscripcionMesa));
        }
        catch (Exception ex)
        {
            return Error<InscripcionMesa, InscripcionMesaResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<ICollection<InscripcionMesaResponse>>> GetInscripcionMesasAsync()
    {
        try
        {
            var inscripcionMesas = await _inscripcionMesaRepository.GetInscripcionMesasAsync();

            var inscripcionMesasDto = new List<InscripcionMesaResponse>();

            foreach (var InscripcionMesa in inscripcionMesas)
            {
                inscripcionMesasDto.Add(_mapper.Map<InscripcionMesaResponse>(InscripcionMesa));
            }
            return Ok<ICollection<InscripcionMesaResponse>>(inscripcionMesasDto);
        }
        catch (Exception ex)
        {
            return Error<InscripcionMesa, ICollection<InscripcionMesaResponse>>(GexErrorMessage.Generic, ex.Message);
        }
    }

    public async Task<GexResult<InscripcionMesaResponse>> UpdateInscripcionMesaAsync(InscripcionMesaRequest inscripcionMesaDto)
    {
        try
        {
            if (inscripcionMesaDto.Id == 0)
                return Error<InscripcionMesa, InscripcionMesaResponse>(GexErrorMessage.InvalidId);

            var inscripcionMesa = await _inscripcionMesaRepository.GetInscripcionMesaAsync(inscripcionMesaDto.Id);

            if (inscripcionMesa == null)
                return Error<InscripcionMesa, InscripcionMesaResponse>(GexErrorMessage.NotFound);

            inscripcionMesa.Nota = -1;
            inscripcionMesa.Condicion = inscripcionMesaDto.Condicion;

            if (!await _inscripcionMesaRepository.UpdateInscripcionMesaAsync(inscripcionMesa))
                return Error<InscripcionMesa, InscripcionMesaResponse>(GexErrorMessage.CouldNotUpdate);

            return Ok<InscripcionMesa, InscripcionMesaResponse>(_mapper.Map<InscripcionMesaResponse>(inscripcionMesa), GexSuccessMessage.Modified);
        }
        catch (Exception ex)
        {
            return Error<InscripcionMesa, InscripcionMesaResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
}
