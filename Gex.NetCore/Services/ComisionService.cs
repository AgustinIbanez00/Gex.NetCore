using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;
using EntityFramework.Exceptions.Common;
using Gex.NetCore.Helpers;
using Gex.NetCore.Models;
using Gex.NetCore.Repository.Interface;
using Gex.NetCore.Services.Interface;
using Gex.NetCore.Utils;
using Gex.NetCore.ViewModels;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Gex.NetCore.Services;
public class ComisionService : IComisionService
{
    private readonly IMapper _mapper;
    private readonly IComisionRepository _repository;
    private readonly string _entityName = "comisión";


    public ComisionService(IMapper mapper, IComisionRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ServiceResponse<ComisionDTO>> CreateComisionAsync(ComisionDTO comisionDTO)
    {
        try
        {
            if (await _repository.ExistsComisionAsync(comisionDTO.Id))
                return ServiceResponse<ComisionDTO>.Error("La comisión ya existe.");

            var comision = _mapper.Map<Comision>(comisionDTO);
            if (!await _repository.CreateComisionAsync(comision))
                return ServiceResponse<ComisionDTO>.Error("No se pudo crear la comisión.");

            var dto = _mapper.Map<ComisionDTO>(comision);
            return ServiceResponse<ComisionDTO>.Ok(dto, "Comisión creada correctamente.");
        }
        catch (UniqueConstraintException)
        {
            return ServiceResponse<ComisionDTO>.Error("Existe una comisión con esos valores.");
        }
        catch (Exception ex)
        {
            return ServiceResponse<ComisionDTO>.Error("Error fatal", ex.Message);
        }
    }

    public async Task<ServiceResponse<ComisionDTO>> DeleteComisionAsync(int id)
    {
        try
        {
            var comision = await _repository.GetComisionAsync(id);
            if (comision == null)
                return ServiceResponse<ComisionDTO>.Error($"No se encontró la comisión {id}");

            if (!await _repository.DeleteComisionAsync(comision))
                return ServiceResponse<ComisionDTO>.Error($"No se pudo eliminar la comisión {id}.");

            return ServiceResponse<ComisionDTO>.Ok(_mapper.Map<ComisionDTO>(comision), $"Comisión {id} eliminada correctamente.");
        }
        catch (Exception ex)
        {
            return ServiceResponse<ComisionDTO>.Error($"Imposible eliminar la comisión {id}", $"Detalles: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<ComisionDTO>> GetComisionAsync(int id)
    {
        try
        {
            var comision = await _repository.GetComisionAsync(id);

            if (comision == null)
                return ServiceResponse<ComisionDTO>.FormattedError(GexError.NotFound, _entityName);

            return ServiceResponse<ComisionDTO>.Ok(_mapper.Map<ComisionDTO>(comision));
        }
        catch (Exception ex)
        {
            return ServiceResponse<ComisionDTO>.FormattedError(GexError.NotFound, _entityName, ex.Message);
        }
    }

    public async Task<ServiceResponse<ComisionDTO>> GetComisionAsync(string nombre)
    {
        try
        {
            var comision = await _repository.GetComisionAsync(nombre);

            if (comision == null)
                return ServiceResponse<ComisionDTO>.Error($"No se encontró la comisión con el nombre \"{nombre}\"");

            return ServiceResponse<ComisionDTO>.Ok(_mapper.Map<ComisionDTO>(comision));
        }
        catch (Exception ex)
        {
            return ServiceResponse<ComisionDTO>.Error($"No se encontró la comisión con el nombre \"{nombre}\"", ex.Message);
        }
    }

    public async Task<ServiceResponse<ICollection<ComisionDTO>>> GetComisionsAsync()
    {
        try
        {
            var comisiones = await _repository.GetComisionsAsync();

            var comisiontesDTO = new List<ComisionDTO>();

            foreach (var comision in comisiones)
            {
                comisiontesDTO.Add(_mapper.Map<ComisionDTO>(comision));
            }
            return ServiceResponse<ICollection<ComisionDTO>>.Ok(comisiontesDTO);
        }
        catch (Exception ex)
        {
            return ServiceResponse<ICollection<ComisionDTO>>.Error("Imposible encontrar las comisiones debido a un error", ex.Message);
        }
    }

    public async Task<ServiceResponse<ComisionDTO>> UpdateComisionAsync(ComisionDTO comisionDTO)
    {
        try
        {
            if (comisionDTO.Id == 0)
                return ServiceResponse<ComisionDTO>.Error("El identificador de la comisión es inválido.");

            var comision = await _repository.GetComisionAsync(comisionDTO.Id);

            if (comision == null)
                ServiceResponse<ComisionDTO>.Error($"No se encontró la comisión con el identificador {comisionDTO.Id}.");

            comision.Nombre = comisionDTO.Nombre;
            comision.CicloLectivo = comisionDTO.CicloLectivo;

            if (!await _repository.UpdateComisionAsync(comision))
                return ServiceResponse<ComisionDTO>.Error($"No se pudo actualizar la comisión {comisionDTO.Id} - {comisionDTO.Nombre}.");

            return ServiceResponse<ComisionDTO>.Ok(_mapper.Map<ComisionDTO>(comision), "Comisión actualizada correctamente.");
        }
        catch (Exception ex)
        {
            return ServiceResponse<ComisionDTO>.Error("Imposible actualizar la comisión.", ex.Message);
        }
    }
}
