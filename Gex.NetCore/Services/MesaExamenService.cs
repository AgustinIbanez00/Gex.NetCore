﻿using System.Collections.Generic;
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

public class MesaExamenService : IMesaExamenService
{
    private readonly IMapper _mapper;
    private readonly IMesaExamenRepository _mesaExamenRepository;
    private readonly IExamenRepository _examenRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public MesaExamenService(IMapper mapper, IMesaExamenRepository repository, IExamenRepository examenRepository, IUsuarioRepository usuarioRepository)
    {
        _mapper = mapper;
        _mesaExamenRepository = repository;
        _examenRepository = examenRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<GexResult<MesaExamenResponse>> CreateMesaExamenAsync(MesaExamenRequest mesaExamenDto)
    {
        try
        {
            if (await _mesaExamenRepository.ExistsMesaExamenAsync(mesaExamenDto.Id))
                return KeyError<MesaExamen, MesaExamenResponse>(nameof(mesaExamenDto.Id), GexErrorMessage.AlreadyExists);

            if(!await _examenRepository.ExistsExamenAsync(mesaExamenDto.ExamenId))
                return KeyError<Examen, MesaExamenResponse>(nameof(mesaExamenDto.ExamenId), GexErrorMessage.NotFound);

            if (!await _usuarioRepository.ExistsUsuarioAsync(mesaExamenDto.ProfesorId))
                return KeyError<Usuario, MesaExamenResponse>(nameof(mesaExamenDto.ProfesorId), GexErrorMessage.NotFound);

            var mesaExamen = _mapper.Map<MesaExamen>(mesaExamenDto);

            if (!await _mesaExamenRepository.CreateMesaExamenAsync(mesaExamen))
                return Error<MesaExamen, MesaExamenResponse>(GexErrorMessage.CouldNotCreate);

            var dto = _mapper.Map<MesaExamenResponse>(mesaExamen);
            return Ok<MesaExamen, MesaExamenResponse>(dto, GexSuccessMessage.Created);
        }
        catch (UniqueConstraintException)
        {
            return Error<MesaExamen, MesaExamenResponse>(GexErrorMessage.AlreadyExists);
        }
        catch (Exception ex)
        {
            return Error<MesaExamen, MesaExamenResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<MesaExamenResponse>> DeleteMesaExamenAsync(long id)
    {
        try
        {
            var mesaExamen = await _mesaExamenRepository.GetMesaExamenAsync(id);
            if (mesaExamen == null)
                return Error<MesaExamen, MesaExamenResponse>(GexErrorMessage.NotFound);

            if (mesaExamen.Estado == Estado.BAJA)
                return Error<MesaExamen, MesaExamenResponse>(GexErrorMessage.AlreadyDeleted);

            if (!await _mesaExamenRepository.DeleteMesaExamenAsync(mesaExamen))
                return Error<MesaExamen, MesaExamenResponse>(GexErrorMessage.CouldNotDelete);

            return Ok<MesaExamen, MesaExamenResponse>(_mapper.Map<MesaExamenResponse>(mesaExamen), GexSuccessMessage.Deleted);
        }
        catch (Exception ex)
        {
            return Error<MesaExamen, MesaExamenResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<MesaExamenResponse>> GetMesaExamenAsync(long id)
    {
        try
        {
            var mesaExamen = await _mesaExamenRepository.GetMesaExamenAsync(id);

            if (mesaExamen == null)
                return Error<MesaExamen, MesaExamenResponse>(GexErrorMessage.NotFound);

            return Ok(_mapper.Map<MesaExamenResponse>(mesaExamen));
        }
        catch (Exception ex)
        {
            return Error<MesaExamen, MesaExamenResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<ICollection<MesaExamenResponse>>> GetMesasExamenesAsync()
    {
        try
        {
            var mesaExamens = await _mesaExamenRepository.GetMesasExamenAsync();

            var mesaExamensDto = new List<MesaExamenResponse>();

            foreach (var MesaExamen in mesaExamens)
            {
                mesaExamensDto.Add(_mapper.Map<MesaExamenResponse>(MesaExamen));
            }
            return Ok<ICollection<MesaExamenResponse>>(mesaExamensDto);
        }
        catch (Exception ex)
        {
            return Error<MesaExamen, ICollection<MesaExamenResponse>>(GexErrorMessage.Generic, ex.Message);
        }
    }

    public async Task<GexResult<MesaExamenResponse>> UpdateMesaExamenAsync(MesaExamenRequest mesaExamenDto)
    {
        try
        {
            if (mesaExamenDto.Id == 0)
                return KeyError<MesaExamen, MesaExamenResponse>(nameof(mesaExamenDto.Id), GexErrorMessage.NotFound);

            var mesaExamen = await _mesaExamenRepository.GetMesaExamenAsync(mesaExamenDto.Id);

            if (mesaExamen == null)
                return KeyError<MesaExamen, MesaExamenResponse>(nameof(mesaExamenDto.Id), GexErrorMessage.NotFound);

            if (!await _examenRepository.ExistsExamenAsync(mesaExamenDto.ExamenId))
                return KeyError<Examen, MesaExamenResponse>(nameof(mesaExamenDto.ExamenId), GexErrorMessage.NotFound);

            mesaExamen.ExamenId = mesaExamenDto.ExamenId;

            if (!await _usuarioRepository.ExistsUsuarioAsync(mesaExamenDto.ProfesorId))
                return KeyError<Usuario, MesaExamenResponse>(nameof(mesaExamenDto.ProfesorId), GexErrorMessage.NotFound);

            mesaExamen.ProfesorId = mesaExamenDto.ProfesorId;
            mesaExamen.Fecha = mesaExamenDto.Fecha;
            mesaExamen.MostrarRespuestas = mesaExamenDto.MostrarRespuestas;
            mesaExamen.Duracion = mesaExamenDto.Duracion;

            if (!await _mesaExamenRepository.UpdateMesaExamenAsync(mesaExamen))
                return Error<MesaExamen, MesaExamenResponse>(GexErrorMessage.CouldNotUpdate);

            return Ok<MesaExamen, MesaExamenResponse>(_mapper.Map<MesaExamenResponse>(mesaExamen), GexSuccessMessage.Modified);
        }
        catch (Exception ex)
        {
            return Error<MesaExamen, MesaExamenResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
}
