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
    public class ComandasController : ControllerBase
    {
        private readonly ContextoConstructora _context;

        public ComandasController(ContextoConstructora context)
        {
            _context = context;
        }

        // GET: api/Comandas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comandas>>> GetComandas()
        {
            if (_context.Comandas == null)
            {
                return NotFound();
            }

            // Obtener todas las comandas con los trabajadores y los edificios vinculados
            var comandas = await _context.Comandas.Include(c => c.Trabajador)
                                                  .ThenInclude(t => t.Edificio)
                                                  .ToListAsync();

            return comandas;
        }

        // GET: api/Comandas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comandas>> GetComandas(int id)
        {
            if (_context.Comandas == null)
            {
                return NotFound();
            }

            var comandas = await _context.Comandas.Include(c => c.Trabajador)
                                                  .ThenInclude(t => t.Edificio)
                                                  .FirstOrDefaultAsync(c => c.Id == id);

            if (comandas == null)
            {
                return NotFound();
            }

            return comandas;
        }

        // POST: api/Comandas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comandas>> PostComandas(Comandas comandas)
        {
            if (_context.Comandas == null)
            {
                return Problem("Entity set 'ContextoConstructora.Comandas' is null.");
            }

            if (comandas.TrabajadorId == null)
            {
                return BadRequest("Debe proporcionar un ID de trabajador válido en 'Trabajador2Id'.");
            }

            var trabajador = await _context.Trabajadores.FindAsync(comandas.TrabajadorId);
            if (trabajador == null)
            {
                return BadRequest("El ID de trabajador proporcionado no existe.");
            }

            var edificio = await _context.Edificios.FindAsync(comandas.Id_Edificios);
            if (edificio == null)
            {
                return BadRequest("El ID de edificio proporcionado no existe.");
            }

            comandas.Nombre_Edificio = edificio.Nombre_Edificio;
            comandas.Nombre = trabajador.Nombre;

            _context.Comandas.Add(comandas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComandas", new { id = comandas.Id }, comandas);
        }

        // DELETE: api/Comandas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComandas(int id)
        {
            if (_context.Comandas == null)
            {
                return NotFound();
            }

            var comandas = await _context.Comandas.FindAsync(id);
            if (comandas == null)
            {
                return NotFound();
            }

            // Eliminar la referencia al trabajador en la comanda
            comandas.Trabajador = null;
            await _context.SaveChangesAsync();

            _context.Comandas.Remove(comandas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComandasExists(int id)
        {
            return (_context.Comandas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
