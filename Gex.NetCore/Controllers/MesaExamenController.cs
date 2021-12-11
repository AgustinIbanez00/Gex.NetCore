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
        var mesaExamens = await _service.GetMesasExamenesAsync();

        if (!mesaExamens.Success)
            return StatusCode(ResponseHelper.GetHttpError(mesaExamens.ErrorCode), mesaExamens);

        return Ok(mesaExamens);
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
    public async Task<ActionResult<GexResult<MesaExamenResponse>>> CreateMesaExamen([FromBody] MesaExamenRequest MesaExamenDTO)
    {
        var mesaExamen = await _service.CreateMesaExamenAsync(MesaExamenDTO);

        if (!mesaExamen.Success)
            return StatusCode(ResponseHelper.GetHttpError(mesaExamen.ErrorCode), mesaExamen);
        return Created(nameof(Get), mesaExamen);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<MesaExamenResponse>))]
    public async Task<ActionResult<GexResult<MesaExamenResponse>>> UpdateMesaExamen([FromBody] MesaExamenRequest MesaExamenDTO)
    {
        var mesaExamen = await _service.UpdateMesaExamenAsync(MesaExamenDTO);

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
