using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalRocioApi.PrologDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalRocioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialConsumoController : ControllerBase
    {
        private readonly DatabasePrologContext _context;

        public HistorialConsumoController(DatabasePrologContext context)
        {
            _context = context;
        }

        // GET: api/HistorialConsumo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialConsumo>>> GetHistorialConsumo()
        {
            return await _context.HistorialConsumo.ToListAsync();
        }

        // GET: api/HistorialConsumo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialConsumo>> GetHistorialConsumo(int id)
        {
            HistorialConsumo historialConsumo = await _context.HistorialConsumo.FindAsync(id);

            if (historialConsumo == null)
            {
                return NotFound();
            }

            return historialConsumo;
        }

        // PUT: api/HistorialConsumo/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistorialConsumo(int id, HistorialConsumo historialConsumo)
        {
            if (id != historialConsumo.IdHistorial)
            {
                return BadRequest();
            }

            _context.Entry(historialConsumo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorialConsumoExists(id))
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

        // POST: api/HistorialConsumo
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HistorialConsumo>> PostHistorialConsumo(HistorialConsumo historialConsumo)
        {
            _context.HistorialConsumo.Add(historialConsumo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistorialConsumo", new { id = historialConsumo.IdHistorial }, historialConsumo);
        }

        // DELETE: api/HistorialConsumo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HistorialConsumo>> DeleteHistorialConsumo(int id)
        {
            HistorialConsumo historialConsumo = await _context.HistorialConsumo.FindAsync(id);
            if (historialConsumo == null)
            {
                return NotFound();
            }

            _context.HistorialConsumo.Remove(historialConsumo);
            await _context.SaveChangesAsync();

            return historialConsumo;
        }

        private bool HistorialConsumoExists(int id)
        {
            return _context.HistorialConsumo.Any(e => e.IdHistorial == id);
        }
    }
}
