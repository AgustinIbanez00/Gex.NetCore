using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Gex.Utils;
using Microsoft.AspNetCore.Http;
using Gex.ViewModels.Request;
using Gex.Models.Enums;
using Gex.Extensions.Response;
using Gex.Services.Interface;
using Gex.ViewModels.Response;
using System.Collections.Generic;

namespace Gex.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class MesaExamenController : ControllerBase
{
    private readonly IMesaExamenService _service;

    public MesaExamenController(IMesaExamenService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<ICollection<MesaExamenResponse>>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = nameof(UsuarioTipo.Alumno))]
    public async Task<ActionResult<GexResult<ICollection<MesaExamenResponse>>>> GetAll()
    {
        var mesasDeExamenes = await _service.GetMesasExamenesAsync();

        if (!mesasDeExamenes.Success)
            return StatusCode(ResponseHelper.GetHttpError(mesasDeExamenes.ErrorCode), mesasDeExamenes);
        return Ok(mesasDeExamenes);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<MesaExamenResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GexResult<MesaExamenResponse>>> Get(int id)
    {
        var mesaExamen = await _service.GetMesaExamenAsync(id);

        if (!mesaExamen.Success)
            return StatusCode(ResponseHelper.GetHttpError(mesaExamen.ErrorCode), mesaExamen);
        return Ok(mesaExamen);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<MesaExamenResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<GexResult<MesaExamenResponse>>> CreateMesaExamen([FromBody] MesaExamenRequest mesaExamenDto)
    {
        var mesaExamen = await _service.CreateMesaExamenAsync(mesaExamenDto);

        if (!mesaExamen.Success)
            return StatusCode(ResponseHelper.GetHttpError(mesaExamen.ErrorCode), mesaExamen);
        return Created(nameof(Get), mesaExamen);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<MesaExamenResponse>))]
    public async Task<ActionResult<GexResult<MesaExamenResponse>>> UpdateMesaExamen([FromBody] MesaExamenRequest mesaExamenDto)
    {
        var mesaExamen = await _service.UpdateMesaExamenAsync(mesaExamenDto);

        if (!mesaExamen.Success)
            return StatusCode(ResponseHelper.GetHttpError(mesaExamen.ErrorCode), mesaExamen);
        return Ok(mesaExamen);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<MesaExamenResponse>))]
    public async Task<ActionResult<GexResult<MesaExamenResponse>>> DeleteMesaExamen(int id)
    {
        var mesaExamen = await _service.DeleteMesaExamenAsync(id);

        if (!mesaExamen.Success)
            return StatusCode(ResponseHelper.GetHttpError(mesaExamen.ErrorCode), mesaExamen);
        return Ok(mesaExamen);
    }
}
