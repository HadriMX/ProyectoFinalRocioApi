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
    public class TamanosController : ControllerBase
    {
        private readonly DatabasePrologContext _context;

        public TamanosController(DatabasePrologContext context)
        {
            _context = context;
        }

        // GET: api/Tamanos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tamanos>>> GetTamanos()
        {
            return await _context.Tamanos.ToListAsync();
        }

        // GET: api/Tamanos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tamanos>> GetTamanos(int id)
        {
            Tamanos tamanos = await _context.Tamanos.FindAsync(id);

            if (tamanos == null)
            {
                return NotFound();
            }

            return tamanos;
        }

        // PUT: api/Tamanos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTamanos(int id, Tamanos tamanos)
        {
            if (id != tamanos.IdTamano)
            {
                return BadRequest();
            }

            _context.Entry(tamanos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TamanosExists(id))
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

        // POST: api/Tamanos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Tamanos>> PostTamanos(Tamanos tamanos)
        {
            _context.Tamanos.Add(tamanos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTamanos", new { id = tamanos.IdTamano }, tamanos);
        }

        // DELETE: api/Tamanos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tamanos>> DeleteTamanos(int id)
        {
            Tamanos tamanos = await _context.Tamanos.FindAsync(id);
            if (tamanos == null)
            {
                return NotFound();
            }

            _context.Tamanos.Remove(tamanos);
            await _context.SaveChangesAsync();

            return tamanos;
        }

        private bool TamanosExists(int id)
        {
            return _context.Tamanos.Any(e => e.IdTamano == id);
        }
    }
}
