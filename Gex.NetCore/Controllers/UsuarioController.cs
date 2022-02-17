using System.Collections.Generic;
using System.Security.Claims;
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
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly IPreguntaService _preguntaService;

    public UsuarioController(IUsuarioService service, IPreguntaService preguntaService)
    {
        _usuarioService = service;
        _preguntaService = preguntaService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<ICollection<UsuarioResponse>>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GexResult<ICollection<UsuarioResponse>>>> GetAll()
    {
        var currentClaim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);

        if (currentClaim == null)
            return GexResponse.Error<ICollection<UsuarioResponse>>("usuario no válido.");

        var usuarios = await _usuarioService.GetUsuariosAsync(currentClaim.Value);

        if (!usuarios.Success)
            return StatusCode(ResponseHelper.GetHttpError(usuarios.ErrorCode), usuarios);

        return Ok(usuarios);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<UsuarioResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = nameof(UsuarioTipo.Administrador))]
    public async Task<ActionResult<GexResult<UsuarioResponse>>> Get(int id)
    {
        var usuario = await _usuarioService.GetUsuarioAsync(id);

        if (!usuario.Success)
            return StatusCode(ResponseHelper.GetHttpError(usuario.ErrorCode), usuario);

        return Ok(usuario);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<UsuarioResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [AllowAnonymous]
    public async Task<ActionResult<GexResult<UsuarioResponse>>> RegisterUsuario([FromBody] RegistroRequest usuarioDto)
    {
        var usuario = await _usuarioService.CreateUsuarioAsync(usuarioDto);

        if (!usuario.Success)
            return StatusCode(ResponseHelper.GetHttpError(usuario.ErrorCode), usuario);
        return Created(nameof(Get), usuario);
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<UsuarioResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [AllowAnonymous]
    public async Task<ActionResult<GexResult<LoginResponse>>> LoginUsuario([FromBody] LoginRequest usuarioDto)
    {
        var usuario = await _usuarioService.LoginUsuarioAsync(usuarioDto);

        if (!usuario.Success)
            return StatusCode(ResponseHelper.GetHttpError(usuario.ErrorCode), usuario);
        return Ok(usuario);
    }

    [Authorize(Roles = nameof(UsuarioTipo.Administrador))]
    [HttpPost("ban")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<UsuarioResponse>))]
    public async Task<ActionResult<GexResult<UsuarioResponse>>> LockUsuario(int id)
    {
        var usuario = await _usuarioService.LockUsuarioAsync(id);

        if (!usuario.Success)
            return StatusCode(ResponseHelper.GetHttpError(usuario.ErrorCode), usuario);
        return Ok(usuario);
    }

    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GexResult<UsuarioResponse>))]
    public async Task<ActionResult<GexResult<UsuarioResponse>>> GetMe()
    {
        var currentClaim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
        if (currentClaim == null)
            return BadRequest(GexResponse.Error<UsuarioResponse>("El token actual es inválido o contiene información errónea."));

        var usuario = await _usuarioService.GetUsuarioByEmailAsync(currentClaim.Value);

        if (!usuario.Success)
            return StatusCode(ResponseHelper.GetHttpError(usuario.ErrorCode), usuario);
        return Ok(usuario);
    }

}
