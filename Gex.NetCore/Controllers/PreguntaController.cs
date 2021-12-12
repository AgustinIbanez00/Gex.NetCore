using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Gex.Services.Interface;
using System.Threading.Tasks;
using Gex.Utils;
using Microsoft.AspNetCore.Http;
using Gex.Models;
using Gex.ViewModels.Request;
using Gex.Extensions.Response;
using Gex.Models.Enums;
using System.Collections.Generic;
using Gex.ViewModels.Response;

namespace Gex.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class PreguntaController : ControllerBase
{
    private readonly IPreguntaService _preguntaService;
    private readonly IRespuestaService _respuestaService;

    public PreguntaController(IPreguntaService service, IRespuestaService respuestaService)
    {
        _preguntaService = service;
        _respuestaService = respuestaService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<PreguntaResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = nameof(UsuarioTipo.Alumno))]
    public async Task<ActionResult<GexResult<ICollection<PreguntaResponse>>>> GetAll()
    {
        var preguntas = await _preguntaService.GetPreguntasAsync();

        if (!preguntas.Success)
            return StatusCode(ResponseHelper.GetHttpError(preguntas.ErrorCode), preguntas);

        return Ok(preguntas);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<PreguntaResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GexResult<PreguntaResponse>>> Get(int id)
    {
        var pregunta = await _preguntaService.GetPreguntaAsync(id);

        if (!pregunta.Success)
            return StatusCode(ResponseHelper.GetHttpError(pregunta.ErrorCode), pregunta);

        return Ok(pregunta);
    }

    [HttpGet("{preguntaId:int}/respuestas")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<ICollection<RespuestaResponse>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GexResult<ICollection<RespuestaResponse>>>> GetRespuestas(int preguntaId)
    {
        var respuestas = await _respuestaService.GetRespuestasByPreguntaIdAsync(preguntaId);

        if (!respuestas.Success)
            return StatusCode(ResponseHelper.GetHttpError(respuestas.ErrorCode), respuestas);

        return Ok(respuestas);
    }

    /*
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<PreguntaResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<GexResult<PreguntaResponse>>> CreatePregunta([FromBody] PreguntaRequest preguntaDto)
    {
        var pregunta = await _service.CreatePreguntaAsync(preguntaDto);

        if (!pregunta.Success)
            return StatusCode(ResponseHelper.GetHttpError(pregunta.ErrorCode), pregunta);
        return Created(nameof(Get), pregunta);
    }
    */
    /// <summary>
    /// Permite crear múltiples preguntas en un solo llamado.
    /// </summary>
    /// <param name="preguntaDto"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<PreguntaResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<GexResult<PreguntaResponse>>> CreatePregunta([FromBody] PreguntaRequest preguntaDto)
    {
        var pregunta = await _preguntaService.CreatePreguntaAsync(preguntaDto);
        if (!pregunta.Success)
            return StatusCode(ResponseHelper.GetHttpError(pregunta.ErrorCode), pregunta);
        return Ok(pregunta);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<PreguntaResponse>))]
    public async Task<ActionResult<GexResult<PreguntaResponse>>> UpdatePregunta([FromBody] PreguntaRequest preguntaDto)
    {
        var pregunta = await _preguntaService.UpdatePreguntaAsync(preguntaDto);

        if (!pregunta.Success)
            return StatusCode(ResponseHelper.GetHttpError(pregunta.ErrorCode), pregunta);
        return Ok(pregunta);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<PreguntaResponse>))]
    public async Task<ActionResult<GexResult<PreguntaResponse>>> DeletePregunta(int id)
    {
        var pregunta = await _preguntaService.DeletePreguntaAsync(id);

        if (!pregunta.Success)
            return StatusCode(ResponseHelper.GetHttpError(pregunta.ErrorCode), pregunta);
        return Ok(pregunta);
    }



}
