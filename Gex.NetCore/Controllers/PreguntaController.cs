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
    private readonly IPreguntaService _service;

    public PreguntaController(IPreguntaService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<PreguntaResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = nameof(UsuarioTipo.Alumno))]
    public async Task<ActionResult<GexResult<ICollection<PreguntaResponse>>>> GetAll()
    {
        var preguntas = await _service.GetPreguntasAsync();

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
        var pregunta = await _service.GetPreguntaAsync(id);

        if (!pregunta.Success)
            return StatusCode(ResponseHelper.GetHttpError(pregunta.ErrorCode), pregunta);

        return Ok(pregunta);
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
        var pregunta = await _service.CreatePreguntaAsync(preguntaDto);
        if (!pregunta.Success)
            return StatusCode(ResponseHelper.GetHttpError(pregunta.ErrorCode), pregunta);
        return Ok(pregunta);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<PreguntaResponse>))]
    public async Task<ActionResult<GexResult<PreguntaResponse>>> UpdatePregunta([FromBody] PreguntaRequest preguntaDto)
    {
        var pregunta = await _service.UpdatePreguntaAsync(preguntaDto);

        if (!pregunta.Success)
            return StatusCode(ResponseHelper.GetHttpError(pregunta.ErrorCode), pregunta);
        return Ok(pregunta);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<PreguntaResponse>))]
    public async Task<ActionResult<GexResult<PreguntaResponse>>> DeletePregunta(int id)
    {
        var pregunta = await _service.DeletePreguntaAsync(id);

        if (!pregunta.Success)
            return StatusCode(ResponseHelper.GetHttpError(pregunta.ErrorCode), pregunta);
        return Ok(pregunta);
    }


}
