using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Extensions.Response;
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
public class InscripcionMesaController : ControllerBase
{
    private readonly IInscripcionMesaService _inscripcionMesaService;

    public InscripcionMesaController(IInscripcionMesaService service)
    {
        _inscripcionMesaService = service;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<ICollection<InscripcionMesaResponse>>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GexResult<ICollection<InscripcionMesaResponse>>>> GetAll()
    {
        var inscripcionMesas = await _inscripcionMesaService.GetInscripcionMesasAsync();

        if (!inscripcionMesas.Success)
            return StatusCode(ResponseHelper.GetHttpError(inscripcionMesas.ErrorCode), inscripcionMesas);

        return Ok(inscripcionMesas);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<InscripcionMesaResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GexResult<InscripcionMesaResponse>>> Get(long id)
    {
        var inscripcionMesa = await _inscripcionMesaService.GetInscripcionMesaAsync(id);

        if (!inscripcionMesa.Success)
            return StatusCode(ResponseHelper.GetHttpError(inscripcionMesa.ErrorCode), inscripcionMesa);

        return Ok(inscripcionMesa);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<InscripcionMesaResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<GexResult<InscripcionMesaResponse>>> CreateInscripcionMesa([FromBody] InscripcionMesaRequest inscripcionMesaDto)
    {
        var inscripcionMesa = await _inscripcionMesaService.CreateInscripcionMesaAsync(inscripcionMesaDto);

        if (!inscripcionMesa.Success)
            return StatusCode(ResponseHelper.GetHttpError(inscripcionMesa.ErrorCode), inscripcionMesa);
        return Created(nameof(Get), inscripcionMesa);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<InscripcionMesaResponse>))]
    public async Task<ActionResult<GexResult<InscripcionMesaResponse>>> UpdateInscripcionMesa([FromBody] InscripcionMesaRequest inscripcionMesaDto)
    {
        var inscripcionMesa = await _inscripcionMesaService.UpdateInscripcionMesaAsync(inscripcionMesaDto);

        if (!inscripcionMesa.Success)
            return StatusCode(ResponseHelper.GetHttpError(inscripcionMesa.ErrorCode), inscripcionMesa);
        return Ok(inscripcionMesa);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<InscripcionMesaResponse>))]
    public async Task<ActionResult<GexResult<InscripcionMesaResponse>>> DeleteInscripcionMesa(long id)
    {
        var inscripcionMesa = await _inscripcionMesaService.DeleteInscripcionMesaAsync(id);

        if (!inscripcionMesa.Success)
            return StatusCode(ResponseHelper.GetHttpError(inscripcionMesa.ErrorCode), inscripcionMesa);
        return Ok(inscripcionMesa);
    }

}
