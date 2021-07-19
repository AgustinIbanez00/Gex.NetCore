using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Gex.NetCore.ViewModels.Curso;
using Gex.NetCore.ViewModels;
using Gex.NetCore.Services.Interface;

namespace Gex.NetCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly ICursoService _service;

        public CursosController(
            ICursoService service
            )
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<CursoCreationDTO>>> GetCursos() => await _service.AllAsync();

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<CursoDTO>> GetCursos(long id)
        {
            var curso = await _service.FindAync(id);
            return curso == null ? NotFound() : curso;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCursos(long id, CursoDTO curso)
        {
            var modified = await _service.ModifyAsync(id, curso);
            return modified == null ? NotFound() :  NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CursoDTO>> PostCursos(CursoDTO curso)
        {
            var id = await _service.CreateAsync(curso);
            return CreatedAtAction(nameof(GetCursos), new { Id = id }, curso);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CursoDTO>> DeleteCursos(long id) => 
            await _service.DeleteAsync(id) == null ? NotFound() : NoContent();
        
    }
}  
