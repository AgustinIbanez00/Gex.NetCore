using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gex.NetCore.Models;
using Microsoft.AspNetCore.Authorization;
using static Gex.NetCore.Helpers.ResponseHelper;
using Gex.NetCore.Helpers;

namespace Gex.NetCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly GexContext _context;

        public CursosController(GexContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: api/Cursos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCursos()
        {
            return await _context.Cursos.ToListAsync();
        }

        // GET: api/Cursos/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetCursos(long id)
        {
            var cursos = await _context.Cursos.FindAsync(id);

            if (cursos == null)
            {
                return NotFound();
            }

            return cursos;
        }

        // PUT: api/Cursos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCursos(long id, Curso cursos)
        {
            if (id != cursos.Id)
            {
                return BadRequest();
            }

            _context.Entry(cursos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cursos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Curso>> PostCursos(Curso cursos)
        { 
            if(!ModelState.IsValid)
                return BadRequest(ResponseHelper.GetModelStateErrors(ModelState));

            if (_context.Cursos.Any(c => c.Id == cursos.Id))
                return BadRequest(ResponseHelper.Response(GexError.DuplicatedEntity));

            _context.Cursos.Add(cursos);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCursos), new { id = cursos.Id }, cursos);
        }

        // DELETE: api/Cursos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Curso>> DeleteCursos(long id)
        {
            var cursos = await _context.Cursos.FindAsync(id);
            if (cursos == null)
            {
                return NotFound();
            }

            _context.Cursos.Remove(cursos);
            await _context.SaveChangesAsync();

            return cursos;
        }

        private bool CursosExists(long id)
        {
            return _context.Cursos.Any(e => e.Id == id);
        }
    }
}  
