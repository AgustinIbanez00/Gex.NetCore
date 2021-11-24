using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Gex.NetCore.Helpers;
using Gex.NetCore.Models;
using Gex.NetCore.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Gex.NetCore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly GexContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(GexContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [Route("Login")]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] CredentialsViewModel credentials)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseHelper.GetModelStateErrors(ModelState));

        Usuario Usuario = await _context.Usuarios.Where(x => x.Email == credentials.Email).FirstOrDefaultAsync();
        if (Usuario == null)
            return NotFound(ResponseHelper.NotFound("usuario"));

        if (HashHelper.CheckHash(credentials.Password, Usuario.Password, Usuario.Salt))
        {
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, credentials.Email));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            string bearer_token = tokenHandler.WriteToken(createdToken);

            return Ok(new { Token = bearer_token });
        }
        else return BadRequest(ResponseHelper.InvalidCredentials("usuario"));
    }

    [Route("Register")]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegistrationViewModel registerModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseHelper.GetModelStateErrors(ModelState));

        if (await _context.Usuarios.Where(x => x.Email == registerModel.Email).AnyAsync())
        {
            ModelState.AddModelError(nameof(registerModel.Email), $"Esa dirección de correo electrónico {registerModel.Email} ya existe.");
            return BadRequest(ModelState);
        }
        //return BadRequest(ResponseHelper.InvalidCredentials());

        HashedPassword Password = HashHelper.Hash(registerModel.Password);

        Usuario user = new Usuario()
        {
            Email = registerModel.Email,
            Password = Password.Password,
            Salt = Password.Salt
        };

        _context.Usuarios.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Usuario creado correctamente." });
    }

    [Route("Me")]
    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {

        var r = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
        return Ok(new { Username = r == null ? "" : r.Value });
    }

}
