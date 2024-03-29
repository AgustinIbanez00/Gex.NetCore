﻿using System.Collections.Generic;
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

public class MateriaService : IMateriaService
{
    private readonly IMapper _mapper;
    private readonly IMateriaRepository _materiaRepository;

    public MateriaService(IMapper mapper, IMateriaRepository materiaRepository)
    {
        _mapper = mapper;
        _materiaRepository = materiaRepository;
    }

    public async Task<GexResult<MateriaResponse>> CreateMateriaAsync(MateriaRequest materiaDto)
    {
        try
        {
            if (await _materiaRepository.ExistsMateriaAsync(materiaDto.Id))
                return Error<Materia, MateriaResponse>(GexErrorMessage.AlreadyExists);

            var materia = _mapper.Map<Materia>(materiaDto);
            if (!await _materiaRepository.CreateMateriaAsync(materia))
                return Error<Materia, MateriaResponse>(GexErrorMessage.CouldNotCreate);

            var dto = _mapper.Map<MateriaResponse>(materia);
            return Ok<Materia, MateriaResponse>(dto, GexSuccessMessage.Created);
        }
        catch (UniqueConstraintException)
        {
            return Error<Materia, MateriaResponse>(GexErrorMessage.AlreadyExists);
        }
        catch (Exception ex)
        {
            return Error<Materia, MateriaResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<MateriaResponse>> DeleteMateriaAsync(long id)
    {
        try
        {
            var materia = await _materiaRepository.GetMateriaAsync(id);
            if (materia == null)
                return Error<Materia, MateriaResponse>(GexErrorMessage.NotFound);

            if (materia.Estado == Estado.BAJA)
                return Error<Materia, MateriaResponse>(GexErrorMessage.AlreadyDeleted);

            if (!await _materiaRepository.DeleteMateriaAsync(materia))
                return Error<Materia, MateriaResponse>(GexErrorMessage.CouldNotDelete);

            return Ok<Materia, MateriaResponse>(_mapper.Map<MateriaResponse>(materia), GexSuccessMessage.Deleted);
        }
        catch (Exception ex)
        {
            return Error<Materia, MateriaResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<MateriaResponse>> GetMateriaAsync(long id)
    {
        try
        {
            var materia = await _materiaRepository.GetMateriaAsync(id);

            if (materia == null)
                return Error<Materia, MateriaResponse>(GexErrorMessage.NotFound);

            return Ok(_mapper.Map<MateriaResponse>(materia));
        }
        catch (Exception ex)
        {
            return Error<Materia, MateriaResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
    public async Task<GexResult<ICollection<MateriaResponse>>> GetMateriasAsync()
    {
        try
        {
            var materias = await _materiaRepository.GetMateriasAsync();

            var materiasDto = new List<MateriaResponse>();

            foreach (var Materia in materias)
            {
                materiasDto.Add(_mapper.Map<MateriaResponse>(Materia));
            }
            return Ok<ICollection<MateriaResponse>>(materiasDto);
        }
        catch (Exception ex)
        {
            return Error<Materia, ICollection<MateriaResponse>>(GexErrorMessage.Generic, ex.Message);
        }
    }

    public async Task<GexResult<MateriaResponse>> UpdateMateriaAsync(MateriaRequest materiaDto)
    {
        try
        {
            if (materiaDto.Id == 0)
                return Error<Materia, MateriaResponse>(GexErrorMessage.InvalidId);

            var materia = await _materiaRepository.GetMateriaAsync(materiaDto.Id);

            if (materia == null)
                return Error<Materia, MateriaResponse>(GexErrorMessage.NotFound);

            materia.Nombre = materiaDto.Nombre;
            materia.Tipo = materiaDto.Tipo;

            if (!await _materiaRepository.UpdateMateriaAsync(materia))
                return Error<Materia, MateriaResponse>(GexErrorMessage.CouldNotUpdate);

            return Ok<Materia, MateriaResponse>(_mapper.Map<MateriaResponse>(materia), GexSuccessMessage.Modified);
        }
        catch (Exception ex)
        {
            return Error<Materia, MateriaResponse>(GexErrorMessage.Generic, ex.Message);
        }
    }
}
