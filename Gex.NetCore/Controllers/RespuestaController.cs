using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Extensions.Response;
using Gex.Models.Enums;
using Gex.Services.Interface;
using Gex.Utils;
using Gex.ViewModels.Request;
using Gex.ViewModels.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gex.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class RespuestaController : ControllerBase
{
    private readonly IRespuestaService _respuestaService;

    public RespuestaController(IRespuestaService service)
    {
        _respuestaService = service;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<ICollection<RespuestaResponse>>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = nameof(UsuarioTipo.Alumno))]
    public async Task<ActionResult<GexResult<ICollection<RespuestaResponse>>>> GetAll()
    {
        var respuestas = await _respuestaService.GetRespuestasAsync();

        if (!respuestas.Success)
            return StatusCode(ResponseHelper.GetHttpError(respuestas.ErrorCode), respuestas);

        return Ok(respuestas);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<RespuestaResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GexResult<RespuestaResponse>>> Get(long id)
    {
        var respuesta = await _respuestaService.GetRespuestaAsync(id);

        if (!respuesta.Success)
            return StatusCode(ResponseHelper.GetHttpError(respuesta.ErrorCode), respuesta);

        return Ok(respuesta);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<RespuestaResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<GexResult<RespuestaResponse>>> CreateOrUpdateRespuesta([FromBody] RespuestaCreateRequest respuestaDto)
    {
        var respuesta = await _respuestaService.CreateOrUpdateRespuestaAsync(respuestaDto);

        if (!respuesta.Success)
            return StatusCode(ResponseHelper.GetHttpError(respuesta.ErrorCode), respuesta);
        return Created(nameof(Get), respuesta);
    }

    /*
    [HttpPost("prepare")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<RespuestaResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<GexResult<RespuestaResponse>>> CreateRespuesta([FromBody] RespuestaCreateRequest respuestaDto)
    {
        var respuesta = await _respuestaService.PrepareCreateRespuestaAsync(respuestaDto);

        if (!respuesta.Success)
            return StatusCode(ResponseHelper.GetHttpError(respuesta.ErrorCode), respuesta);
        return Created(nameof(Get), respuesta);
    }
    */
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<RespuestaResponse>))]
    public async Task<ActionResult<GexResult<RespuestaResponse>>> UpdateRespuesta([FromBody] RespuestaRequest respuestaDto)
    {
        var respuesta = await _respuestaService.UpdateRespuestaAsync(respuestaDto);

        if (!respuesta.Success)
            return StatusCode(ResponseHelper.GetHttpError(respuesta.ErrorCode), respuesta);
        return Ok(respuesta);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<RespuestaResponse>))]
    public async Task<ActionResult<GexResult<RespuestaResponse>>> DeleteRespuesta(long id)
    {
        var respuesta = await _respuestaService.DeleteRespuestaAsync(id);

        if (!respuesta.Success)
            return StatusCode(ResponseHelper.GetHttpError(respuesta.ErrorCode), respuesta);
        return Ok(respuesta);
    }


}
