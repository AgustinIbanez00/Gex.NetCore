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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<ICollection<MesaExamenRequest>>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = nameof(UsuarioTipo.Alumno))]
    public async Task<ActionResult<GexResponse<ICollection<MesaExamenRequest>>>> GetAll()
    {
        var mesaExamens = await _service.GetMesaExamensAsync();

        if (!mesaExamens.Success)
            return StatusCode(ResponseHelper.GetHttpError(mesaExamens.ErrorCode), mesaExamens);

        return Ok(mesaExamens);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<MesaExamenRequest>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GexResponse<MesaExamenRequest>>> Get(int id)
    {
        var mesaExamen = await _service.GetMesaExamenAsync(id);

        if (!mesaExamen.Success)
            return StatusCode(ResponseHelper.GetHttpError(mesaExamen.ErrorCode), mesaExamen);

        return Ok(mesaExamen);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<MesaExamenRequest>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<GexResponse<MesaExamenRequest>>> CreateMesaExamen([FromBody] MesaExamenRequest MesaExamenDTO)
    {
        var mesaExamen = await _service.CreateMesaExamenAsync(MesaExamenDTO);

        if (!mesaExamen.Success)
            return StatusCode(ResponseHelper.GetHttpError(mesaExamen.ErrorCode), mesaExamen);
        return Created(nameof(Get), mesaExamen);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<MesaExamenRequest>))]
    public async Task<ActionResult<GexResponse<MesaExamenRequest>>> UpdateMesaExamen([FromBody] MesaExamenRequest MesaExamenDTO)
    {
        var mesaExamen = await _service.UpdateMesaExamenAsync(MesaExamenDTO);

        if (!mesaExamen.Success)
            return StatusCode(ResponseHelper.GetHttpError(mesaExamen.ErrorCode), mesaExamen);
        return Ok(mesaExamen);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<MesaExamenRequest>))]
    public async Task<ActionResult<GexResponse<MesaExamenRequest>>> DeleteMesaExamen(int id)
    {
        var mesaExamen = await _service.DeleteMesaExamenAsync(id);

        if (!mesaExamen.Success)
            return StatusCode(ResponseHelper.GetHttpError(mesaExamen.ErrorCode), mesaExamen);
        return Ok(mesaExamen);
    }
}
