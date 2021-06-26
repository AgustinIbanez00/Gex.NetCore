using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gex.NetCore.Models;

namespace Gex.NetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriasController : ControllerBase
    {
        private readonly GexContext _context;

        public MateriasController(GexContext context)
        {
            _context = context;
        }

        // GET: api/Materias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Materia>>> GetMaterias()
        {
            return await _context.Materias.ToListAsync();
        }

        // GET: api/Materias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Materia>> GetMaterias(long id)
        {
            var materias = await _context.Materias.FindAsync(id);

            if (materias == null)
            {
                return NotFound();
            }

            return materias;
        }

        // PUT: api/Materias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterias(long id, Materia materias)
        {
            if (id != materias.Id)
            {
                return BadRequest();
            }

            _context.Entry(materias).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MateriasExists(id))
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

        // POST: api/Materias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Materia>> PostMaterias(Materia materias)
        {
            _context.Materias.Add(materias);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaterias", new { id = materias.Id }, materias);
        }

        // DELETE: api/Materias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterias(long id)
        {
            var materias = await _context.Materias.FindAsync(id);
            if (materias == null)
            {
                return NotFound();
            }

            _context.Materias.Remove(materias);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MateriasExists(long id)
        {
            return _context.Materias.Any(e => e.Id == id);
        }
    }
}
