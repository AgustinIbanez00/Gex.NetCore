using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Gex.Utils;
using Microsoft.AspNetCore.Http;
using Gex.ViewModels.Request;
using Gex.Extensions.Response;
using Gex.Models.Enums;
using Gex.Services.Interface;
using Gex.ViewModels.Response;

namespace Gex.Controllers;
/*
[Route("api/[controller]")]
[Authorize]
[ApiController]
public class ComisionController : ControllerBase
{
    private readonly IComisionService _service;

    public ComisionController(IComisionService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<ComisionResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = nameof(UsuarioTipo.Alumno))]
    public async Task<ActionResult<GexResult<ComisionResponse>>> GetAll()
    {
        var comisiones = await _service.GetComisionesAsync();

        if (!comisiones.Success)
            return StatusCode(ResponseHelper.GetHttpError(comisiones.ErrorCode), comisiones);

        return Ok(comisiones);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<ComisionResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GexResult<ComisionResponse>>> Get(int id)
    {
        var comision = await _service.GetComisionAsync(id);

        if (!comision.Success)
            return StatusCode(ResponseHelper.GetHttpError(comision.ErrorCode), comision);

        return Ok(comision);
    }

    [HttpGet("nombre/{nombre}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<ComisionResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GexResult<ComisionResponse>>> GetByName(string nombre)
    {
        var comision = await _service.GetComisionAsync(nombre);

        if (!comision.Success)
            return BadRequest(comision);
        return Ok(comision);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<ComisionResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<GexResult<ComisionResponse>>> CreateComision([FromBody] ComisionRequest comisionDTO)
    {
        var comision = await _service.CreateComisionAsync(comisionDTO);

        if (!comision.Success)
            return StatusCode(ResponseHelper.GetHttpError(comision.ErrorCode), comision);
        return Created(nameof(Get), comision);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<ComisionResponse>))]
    public async Task<ActionResult<GexResult<ComisionResponse>>> UpdateComision([FromBody] ComisionRequest comisionDTO)
    {
        var comision = await _service.UpdateComisionAsync(comisionDTO);

        if (!comision.Success)
            return StatusCode(ResponseHelper.GetHttpError(comision.ErrorCode), comision);
        return Ok(comision);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<ComisionResponse>))]
    public async Task<ActionResult<GexResult<ComisionResponse>>> DeleteComision(int id)
    {
        var comision = await _service.DeleteComisionAsync(id);

        if (!comision.Success)
            return StatusCode(ResponseHelper.GetHttpError(comision.ErrorCode), comision);
        return Ok(comision);
    }


}
*/