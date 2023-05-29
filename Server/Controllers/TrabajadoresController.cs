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
    public class TrabajadoresController : ControllerBase
    {
        private readonly ContextoConstructora _context;

        public TrabajadoresController(ContextoConstructora context)
        {
            _context = context;
        }

        // GET: api/Trabajadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trabajadores>>> GetTrabajadores()
        {
            if (_context.Trabajadores == null)
            {
                return NotFound();
            }

            var trabajadores = await _context.Trabajadores.Include(t => t.Edificio).Include(t => t.Comanda).ToListAsync();
            return trabajadores;
        }

        // GET: api/Trabajadores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trabajadores>> GetTrabajadores(int id)
        {
            if (_context.Trabajadores == null)
            {
                return NotFound();
            }

            var trabajadores = await _context.Trabajadores.Include(t => t.Edificio).Include(t => t.Comanda).FirstOrDefaultAsync(t => t.Id == id);

            if (trabajadores == null)
            {
                return NotFound();
            }

            return trabajadores;
        }

        // PUT: api/Trabajadores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrabajadores(int id, Trabajadores trabajadores)
        {
            if (id != trabajadores.Id)
            {
                return BadRequest();
            }

            _context.Entry(trabajadores).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrabajadoresExists(id))
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

        // POST: api/Trabajadores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trabajadores>> PostTrabajadores(Trabajadores trabajadores)
        {
            if (_context.Trabajadores == null)
            {
                return Problem("Entity set 'ContextoConstructora.Trabajadores' is null.");
            }

            _context.Trabajadores.Add(trabajadores);
            await _context.SaveChangesAsync();

            // Vincular las comandas correspondientes al trabajador
            var comandas = await _context.Comandas.Where(c => c.TrabajadorId == trabajadores.Id).ToListAsync();
            if (comandas != null)
            {
                trabajadores.Comanda = comandas;
                foreach (var comanda in comandas)
                {
                    comanda.Trabajador = trabajadores;
                }
                await _context.SaveChangesAsync();
            }

            // Vincular los edificios correspondientes al trabajador
            var edificios = await _context.Edificios.Where(e => e.TrabajadoresId == trabajadores.Id).ToListAsync();
            if (edificios != null)
            {
                trabajadores.Edificio = edificios;
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction("GetTrabajadores", new { id = trabajadores.Id }, trabajadores);
        }

        // DELETE: api/Trabajadores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrabajadores(int id)
        {
            if (_context.Trabajadores == null)
            {
                return NotFound();
            }

            var trabajadores = await _context.Trabajadores.FindAsync(id);
            if (trabajadores == null)
            {
                return NotFound();
            }

            _context.Trabajadores.Remove(trabajadores);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrabajadoresExists(int id)
        {
            return (_context.Trabajadores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
