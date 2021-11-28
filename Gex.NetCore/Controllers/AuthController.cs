using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Gex.NetCore.DTO;
using Gex.NetCore.Helpers;
using Gex.NetCore.Models;
using Gex.NetCore.Utils;

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
    public async Task<ActionResult<GexResponse<string>>> Login([FromBody] CredentialsDTO credentials)
    {
        Usuario Usuario = await _context.Usuarios.Where(x => x.Email == credentials.Email).FirstOrDefaultAsync();
        if (Usuario == null)
            return BadRequest();
            //return BadRequest(ServiceResponse<string>.FormattedError(nameof(credentials.Email), GexErrorMessage.InvalidEmail, Gender.MALE));

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

            return GexResponse<string>.Ok(bearer_token);
        }
        //else return BadRequest(ServiceResponse<string>.FormattedError(nameof(credentials.Password), GexErrorMessage.InvalidPassword));
        return BadRequest();
    }

    [Route("Register")]
    [HttpPost]
    public async Task<ActionResult<GexResponse<string>>> Register([FromBody] RegistrationDTO registerModel)
    {
        if (await _context.Usuarios.Where(x => x.Email == registerModel.Email).AnyAsync())
            return BadRequest(GexResponse<string>.Error("Error", nameof(registerModel.Email), $"Esa dirección de correo electrónico {registerModel.Email} ya existe."));
        
        HashedPassword Password = HashHelper.Hash(registerModel.Password);

        Usuario user = new Usuario()
        {
            Email = registerModel.Email,
            Password = Password.Password,
            Salt = Password.Salt
        };

        _context.Usuarios.Add(user);
        await _context.SaveChangesAsync();

        return Ok(GexResponse<string>.Ok(null, "Usuario creado correctamente."));
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
