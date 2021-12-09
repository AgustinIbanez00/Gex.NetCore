using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Gex.Services.Interface;
using System.Threading.Tasks;
using Gex.Utils;
using Microsoft.AspNetCore.Http;
using Gex.Helpers;
using System.Collections.Generic;
using Gex.Models;
using Gex.ViewModels.Request;
using Gex.ViewModels.Response;

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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<ICollection<ExamenRequest>>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = nameof(UsuarioTipo.Alumno))]
    public async Task<ActionResult<GexResponse<ICollection<ExamenRequest>>>> GetAll()
    {
        var examenes = await _service.GetExamensAsync();

        if (!examenes.Success)
            return StatusCode(ResponseHelper.GetHttpError(examenes.ErrorCode), examenes);

        return Ok(examenes);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<ExamenResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GexResponse<ExamenResponse>>> Get(int id)
    {
        var examen = await _service.GetExamenAsync(id);

        if (!examen.Success)
            return StatusCode(ResponseHelper.GetHttpError(examen.ErrorCode), examen);

        return Ok(examen);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<ExamenRequest>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<GexResponse<ExamenRequest>>> CreateExamen([FromBody] ExamenRequest request)
    {
        var examen = await _service.CreateExamenAsync(request);

        if (!examen.Success)
            return StatusCode(ResponseHelper.GetHttpError(examen.ErrorCode), examen);
        return Created(nameof(Get), examen);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<ExamenRequest>))]
    public async Task<ActionResult<GexResponse<ExamenRequest>>> UpdateExamen([FromBody] ExamenRequest request)
    {
        var Examen = await _service.UpdateExamenAsync(request);

        if (!Examen.Success)
            return StatusCode(ResponseHelper.GetHttpError(Examen.ErrorCode), Examen);
        return Ok(Examen);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<ExamenRequest>))]
    public async Task<ActionResult<GexResponse<ExamenRequest>>> DeleteExamen(int id)
    {
        var examen = await _service.DeleteExamenAsync(id);

        if (!examen.Success)
            return StatusCode(ResponseHelper.GetHttpError(examen.ErrorCode), examen);
        return Ok(examen);
    }


}
