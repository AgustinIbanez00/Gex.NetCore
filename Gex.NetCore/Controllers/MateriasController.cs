using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Gex.Services.Interface;
using System.Threading.Tasks;
using Gex.DTO;
using Gex.Utils;
using Microsoft.AspNetCore.Http;
using Gex.Helpers;
using System.Collections.Generic;
using Gex.Models;

namespace Gex.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class MateriaController : ControllerBase
{
    private readonly IMateriaService _service;

    public MateriaController(IMateriaService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<ICollection<MateriaDTO>>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = nameof(UsuarioTipo.Alumno))]
    public async Task<ActionResult<GexResponse<ICollection<MateriaDTO>>>> GetAll()
    {
        var materias = await _service.GetMateriasAsync();

        if (!materias.Success)
            return StatusCode(ResponseHelper.GetHttpError(materias.ErrorCode), materias);

        return Ok(materias);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<MateriaDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GexResponse<MateriaDTO>>> Get(int id)
    {
        var materia = await _service.GetMateriaAsync(id);

        if (!materia.Success)
            return StatusCode(ResponseHelper.GetHttpError(materia.ErrorCode), materia);

        return Ok(materia);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<MateriaDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<GexResponse<MateriaDTO>>> CreateMateria([FromBody] MateriaDTO MateriaDTO)
    {
        var materia = await _service.CreateMateriaAsync(MateriaDTO);

        if (!materia.Success)
            return StatusCode(ResponseHelper.GetHttpError(materia.ErrorCode), materia);
        return Created(nameof(Get), materia);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<MateriaDTO>))]
    public async Task<ActionResult<GexResponse<MateriaDTO>>> UpdateMateria([FromBody] MateriaDTO MateriaDTO)
    {
        var materia = await _service.UpdateMateriaAsync(MateriaDTO);

        if (!materia.Success)
            return StatusCode(ResponseHelper.GetHttpError(materia.ErrorCode), materia);
        return Ok(materia);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<MateriaDTO>))]
    public async Task<ActionResult<GexResponse<MateriaDTO>>> DeleteMateria(int id)
    {
        var materia = await _service.DeleteMateriaAsync(id);

        if (!materia.Success)
            return StatusCode(ResponseHelper.GetHttpError(materia.ErrorCode), materia);
        return Ok(materia);
    }


}
