using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Gex.Services.Interface;
using System.Threading.Tasks;
using Gex.Utils;
using Microsoft.AspNetCore.Http;
using Gex.ViewModels.Request;
using Gex.Extensions.Response;
using Gex.ViewModels.Response;
using System.Collections;
using System.Collections.Generic;

namespace Gex.Controllers;

[Route("api/[controller]")]
[Authorize("ProfesoresOnly")]
[ApiController]
public class ExamenController : ControllerBase
{
    private readonly IExamenService _examenService;
    private readonly IPreguntaService _preguntaService;

    public ExamenController(IExamenService examenService, IPreguntaService preguntaService)
    {
        _examenService = examenService;
        _preguntaService = preguntaService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<ICollection<ExamenResponse>>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GexResult<ICollection<ExamenResponse>>>> GetAll()
    {
        var examenes = await _examenService.GetExamenesAsync();

        if (!examenes.Success)
            return StatusCode(ResponseHelper.GetHttpError(examenes.ErrorCode), examenes);

        return Ok(examenes);
    }


    [HttpGet("{examenId:int}/preguntas")]
    public async Task<ActionResult<GexResult<ExamenResponse>>> GetPreguntas(long examenId)
    {
        var preguntas = await _preguntaService.GetPreguntasByExamenIdAsync(examenId);

        if (!preguntas.Success)
            return StatusCode(ResponseHelper.GetHttpError(preguntas.ErrorCode), preguntas.Data);

        return Ok(preguntas);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<ExamenResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GexResult<ExamenResponse>>> Get(long id)
    {
        var examen = await _examenService.GetExamenAsync(id);

        if (!examen.Success)
            return StatusCode(ResponseHelper.GetHttpError(examen.ErrorCode), examen);

        return Ok(examen);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<ExamenResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<GexResult<ExamenResponse>>> CreateExamen([FromBody] ExamenRequest request)
    {
        var examen = await _examenService.CreateExamenAsync(request);

        if (!examen.Success)
            return StatusCode(ResponseHelper.GetHttpError(examen.ErrorCode), examen);
        return Created(nameof(Get), examen);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<ExamenResponse>))]
    public async Task<ActionResult<GexResult<ExamenResponse>>> UpdateExamen([FromBody] ExamenRequest request)
    {
        var Examen = await _examenService.UpdateExamenAsync(request);

        if (!Examen.Success)
            return StatusCode(ResponseHelper.GetHttpError(Examen.ErrorCode), Examen);
        return Ok(Examen);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<ExamenResponse>))]
    public async Task<ActionResult<GexResult<ExamenResponse>>> DeleteExamen(long id)
    {
        var examen = await _examenService.DeleteExamenAsync(id);

        if (!examen.Success)
            return StatusCode(ResponseHelper.GetHttpError(examen.ErrorCode), examen);
        return Ok(examen);
    }

    [HttpPost("rendir")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<ExamenResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<GexResult<ExamenResponse>>> RendirExamen([FromBody] RendirExamenRequest request)
    {
        var examen = await _examenService.RendirExamenAsync(request);

        if (!examen.Success)
            return StatusCode(ResponseHelper.GetHttpError(examen.ErrorCode), examen);
        return Created(nameof(Get), examen);
    }


}
