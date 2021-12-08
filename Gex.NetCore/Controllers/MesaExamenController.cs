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
public class MesaExamenController : ControllerBase
{
    private readonly IMesaExamenService _service;

    public MesaExamenController(IMesaExamenService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<ICollection<MesaExamenDTO>>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = nameof(UsuarioTipo.Alumno))]
    public async Task<ActionResult<GexResponse<ICollection<MesaExamenDTO>>>> GetAll()
    {
        var mesaexamens = await _service.GetMesaExamensAsync();

        if (!mesaexamens.Success)
            return StatusCode(ResponseHelper.GetHttpError(mesaexamens.ErrorCode), mesaexamens);

        return Ok(mesaexamens);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<MesaExamenDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GexResponse<MesaExamenDTO>>> Get(int id)
    {
        var mesaexamen = await _service.GetMesaExamenAsync(id);

        if (!mesaexamen.Success)
            return StatusCode(ResponseHelper.GetHttpError(mesaexamen.ErrorCode), mesaexamen);

        return Ok(mesaexamen);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<MesaExamenDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<GexResponse<MesaExamenDTO>>> CreateMesaExamen([FromBody] MesaExamenDTO MesaExamenDTO)
    {
        var mesaexamen = await _service.CreateMesaExamenAsync(MesaExamenDTO);

        if (!mesaexamen.Success)
            return StatusCode(ResponseHelper.GetHttpError(mesaexamen.ErrorCode), mesaexamen);
        return Created(nameof(Get), mesaexamen);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<MesaExamenDTO>))]
    public async Task<ActionResult<GexResponse<MesaExamenDTO>>> UpdateMesaExamen([FromBody] MesaExamenDTO MesaExamenDTO)
    {
        var mesaexamen = await _service.UpdateMesaExamenAsync(MesaExamenDTO);

        if (!mesaexamen.Success)
            return StatusCode(ResponseHelper.GetHttpError(mesaexamen.ErrorCode), mesaexamen);
        return Ok(mesaexamen);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<MesaExamenDTO>))]
    public async Task<ActionResult<GexResponse<MesaExamenDTO>>> DeleteMesaExamen(int id)
    {
        var mesaexamen = await _service.DeleteMesaExamenAsync(id);

        if (!mesaexamen.Success)
            return StatusCode(ResponseHelper.GetHttpError(mesaexamen.ErrorCode), mesaexamen);
        return Ok(mesaexamen);
    }
}
