using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Actividad_18.Server.Contexto;
using Actividad_18.Shared.Models;

namespace Actividad_18.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EdificiosController : ControllerBase
    {
        private readonly ContextoConstructora _context;

        public EdificiosController(ContextoConstructora context)
        {
            _context = context;
        }

        // GET: api/Edificios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Edificios>>> GetEdificios()
        {
            if (_context.Edificios == null)
            {
                return NotFound();
            }

            // Obtener los edificios con los trabajadores y comandas vinculadas
            var edificios = await _context.Edificios.Include(e => e.Trabajador)
                                                    .ThenInclude(t => t.Comanda)
                                                    .ToListAsync();

            return edificios;
        }

        // GET: api/Edificios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Edificios>> GetEdificios(int id)
        {
            if (_context.Edificios == null)
            {
                return NotFound();
            }

            var edificios = await _context.Edificios.Include(e => e.Trabajador)
                                                    .ThenInclude(t => t.Comanda)
                                                    .FirstOrDefaultAsync(e => e.Id == id);

            if (edificios == null)
            {
                return NotFound();
            }

            return edificios;
        }

        // PUT: api/Edificios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEdificios(int id, Edificios edificios)
        {
            if (id != edificios.Id)
            {
                return BadRequest();
            }

            _context.Entry(edificios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EdificiosExists(id))
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

        // POST: api/Edificios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Edificios>> PostEdificios(Edificios edificios)
        {
            if (_context.Edificios == null)
            {
                return Problem("Entity set 'ContextoConstructora.Edificios' is null.");
            }

            if (edificios.Trabajador != null)
            {
                return BadRequest("El edificio ya tiene un trabajador asignado.");
            }

            _context.Edificios.Add(edificios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEdificios", new { id = edificios.Id }, edificios);
        }

        // DELETE: api/Edificios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEdificios(int id)
        {
            if (_context.Edificios == null)
            {
                return NotFound();
            }

            var edificios = await _context.Edificios.FindAsync(id);
            if (edificios == null)
            {
                return NotFound();
            }

            // Eliminar la referencia al trabajador en el edificio
            edificios.Trabajador = null;
            await _context.SaveChangesAsync();

            _context.Edificios.Remove(edificios);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EdificiosExists(int id)
        {
            return (_context.Edificios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
