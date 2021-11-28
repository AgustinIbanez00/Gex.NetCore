using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Gex.NetCore.Services.Interface;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Net;
using Gex.NetCore.DTO;
using Gex.NetCore.Utils;
using Microsoft.AspNetCore.Http;
using Gex.NetCore.Helpers;

namespace Gex.NetCore.Controllers;

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
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetComisionsAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        if(id == 0)
            return BadRequest();

        var comision = await _service.GetComisionAsync(id);

        if (!comision.Success)
        {
            if (comision.ErrorCode == Helpers.GexErrorMessage.NotFound)
                return NotFound(comision);
            else
                return BadRequest(comision);
        }

        return Ok(comision);
    }
    [HttpGet("nombre/{nombre}")]
    public async Task<IActionResult> GetByName(string nombre)
    {
        var comision = await _service.GetComisionAsync(nombre);

        if (!comision.Success)
            return BadRequest(comision);
        return Ok(comision);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<ComisionDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<GexResponse<ComisionDTO>>> CreateComision([FromBody] ComisionDTO comisionDTO)
    {
        var comision = await _service.CreateComisionAsync(comisionDTO);

        if (!comision.Success)
            return StatusCode(ResponseHelper.GetHttpError(comision.ErrorCode), comision);
        return Created(nameof(Get), comision);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<ComisionDTO>))]
    public async Task<ActionResult<GexResponse<ComisionDTO>>> UpdateComision([FromBody] ComisionDTO comisionDTO)
    {
        var comision = await _service.UpdateComisionAsync(comisionDTO);

        if (!comision.Success)
            return StatusCode(ResponseHelper.GetHttpError(comision.ErrorCode), comision);
        return Ok(comision);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResponse<ComisionDTO>))]
    public async Task<ActionResult<GexResponse<ComisionDTO>>> DeleteComision(int id)
    {
        var comision = await _service.DeleteComisionAsync(id);

        if (!comision.Success)
            return StatusCode(ResponseHelper.GetHttpError(comision.ErrorCode), comision);
        return Ok(comision);
    }


}
