using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Gex.Services.Interface;
using System.Threading.Tasks;
using Gex.Utils;
using Microsoft.AspNetCore.Http;
using Gex.ViewModels.Request;
using Gex.Extensions.Response;
using Gex.Models.Enums;
using Gex.ViewModels.Response;
using System.Collections.Generic;

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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<ICollection<MateriaResponse>>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = nameof(UsuarioTipo.Alumno))]
    public async Task<ActionResult<GexResult<ICollection<MateriaResponse>>>> GetAll()
    {
        var materias = await _service.GetMateriasAsync();

        if (!materias.Success)
            return StatusCode(ResponseHelper.GetHttpError(materias.ErrorCode), materias);

        return Ok(materias);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<MateriaResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GexResult<MateriaResponse>>> Get(int id)
    {
        var materia = await _service.GetMateriaAsync(id);

        if (!materia.Success)
            return StatusCode(ResponseHelper.GetHttpError(materia.ErrorCode), materia);

        return Ok(materia);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<MateriaResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<GexResult<MateriaResponse>>> CreateMateria([FromBody] MateriaRequest materiaDto)
    {
        var materia = await _service.CreateMateriaAsync(materiaDto);

        if (!materia.Success)
            return StatusCode(ResponseHelper.GetHttpError(materia.ErrorCode), materia);
        return Created(nameof(Get), materia);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<MateriaResponse>))]
    public async Task<ActionResult<GexResult<MateriaResponse>>> UpdateMateria([FromBody] MateriaRequest materiaDto)
    {
        var materia = await _service.UpdateMateriaAsync(materiaDto);

        if (!materia.Success)
            return StatusCode(ResponseHelper.GetHttpError(materia.ErrorCode), materia);
        return Ok(materia);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<MateriaResponse>))]
    public async Task<ActionResult<GexResult<MateriaResponse>>> DeleteMateria(int id)
    {
        var materia = await _service.DeleteMateriaAsync(id);

        if (!materia.Success)
            return StatusCode(ResponseHelper.GetHttpError(materia.ErrorCode), materia);
        return Ok(materia);
    }
}
