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
public class ExamenController : ControllerBase
{
    private readonly IExamenService _service;

    public ExamenController(IExamenService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<ICollection<ExamenDTO>>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = nameof(UsuarioTipo.Alumno))]
    public async Task<ActionResult<GexResponse<ICollection<ExamenDTO>>>> GetAll()
    {
        var Examenes = await _service.GetExamensAsync();

        if (!Examenes.Success)
            return StatusCode(ResponseHelper.GetHttpError(Examenes.ErrorCode), Examenes);

        return Ok(Examenes);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<ExamenDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GexResponse<ExamenDTO>>> Get(int id)
    {
        var Examen = await _service.GetExamenAsync(id);

        if (!Examen.Success)
            return StatusCode(ResponseHelper.GetHttpError(Examen.ErrorCode), Examen);

        return Ok(Examen);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<ExamenDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<GexResponse<ExamenDTO>>> CreateExamen([FromBody] ExamenDTO ExamenDTO)
    {
        var Examen = await _service.CreateExamenAsync(ExamenDTO);

        if (!Examen.Success)
            return StatusCode(ResponseHelper.GetHttpError(Examen.ErrorCode), Examen);
        return Created(nameof(Get), Examen);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<ExamenDTO>))]
    public async Task<ActionResult<GexResponse<ExamenDTO>>> UpdateExamen([FromBody] ExamenDTO ExamenDTO)
    {
        var Examen = await _service.UpdateExamenAsync(ExamenDTO);

        if (!Examen.Success)
            return StatusCode(ResponseHelper.GetHttpError(Examen.ErrorCode), Examen);
        return Ok(Examen);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<ExamenDTO>))]
    public async Task<ActionResult<GexResponse<ExamenDTO>>> DeleteExamen(int id)
    {
        var Examen = await _service.DeleteExamenAsync(id);

        if (!Examen.Success)
            return StatusCode(ResponseHelper.GetHttpError(Examen.ErrorCode), Examen);
        return Ok(Examen);
    }


}
