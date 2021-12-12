using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Gex.Extensions.Hashing;
using Gex.Extensions.Response;
using Gex.Models;
using Gex.Models.Enums;
using Gex.Utils;
using Gex.ViewModels.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Gex.Controllers;
using static GexResponse;

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
    public async Task<ActionResult<GexResult<object>>> Login([FromBody] LoginRequest credentials)
    {
        Usuario Usuario = await _context.Usuarios.Where(x => x.Email == credentials.Email).FirstOrDefaultAsync();
        if (Usuario == null)
            return BadRequest(KeyError<Usuario, object>(nameof(credentials.Email), GexErrorMessage.InvalidEmail));

        if (HashingExtensions.CheckHash(credentials.Password, Usuario.Password, Usuario.Salt))
        {
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, credentials.Email));
            claims.AddClaim(new Claim(ClaimTypes.Role, Usuario.Tipo.ToString()));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            string bearer_token = tokenHandler.WriteToken(createdToken);

            return Ok(Ok<string>(bearer_token));
        }
        return BadRequest(KeyError<Usuario, object>(nameof(credentials.Password), GexErrorMessage.InvalidPassword));
    }

    [Route("Register")]
    [HttpPost]
    public async Task<ActionResult<GexResult<object>>> Register([FromBody] RegistroRequest registerModel)
    {
        if (await _context.Usuarios.Where(x => x.Email == registerModel.Email).AnyAsync())
            return BadRequest(KeyError<object>(nameof(registerModel.Email), $"Esa dirección de correo electrónico {registerModel.Email} ya existe."));

        HashedPassword Password = registerModel.Password.Hash();

        Usuario user = new Usuario()
        {
            Email = registerModel.Email,
            Password = Password.Password,
            Salt = Password.Salt,
            Tipo = UsuarioTipo.Alumno
        };

        _context.Usuarios.Add(user);
        await _context.SaveChangesAsync();

        return Ok(Ok<Usuario, object>(null, GexSuccessMessage.Created));
    }

    [Route("Me")]
    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        var r = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Role);
        return Ok(new { Username = r == null ? "" : r.Value });
    }

}
