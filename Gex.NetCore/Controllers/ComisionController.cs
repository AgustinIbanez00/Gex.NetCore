using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Gex.NetCore.Services.Interface;
using System.Threading.Tasks;
using Gex.NetCore.ViewModels;
using Microsoft.Extensions.Options;
using System.Net;

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
            if (comision.ErrorCode == Helpers.GexError.NotFound)
                return NotFound(comision);
            else
                return BadRequest(comision);
        }

        return Ok(comision);
    }
    [HttpGet("byName/{nombre}")]
    public async Task<IActionResult> GetByName(string nombre)
    {
        var comision = await _service.GetComisionAsync(nombre);

        if (!comision.Success)
            return BadRequest(comision);
        return Ok(comision);
    }

    [HttpPost]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateComision([FromBody] ComisionDTO comisionDTO)
    {
        if (comisionDTO == null)
            return BadRequest();

        var comision = await _service.CreateComisionAsync(comisionDTO);

        if(!comision.Success)
            return BadRequest(comision);
        return Ok(comision);
    }



}
