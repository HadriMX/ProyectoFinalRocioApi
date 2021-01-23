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
    public class BebidasController : ControllerBase
    {
        private readonly DatabasePrologContext _context;

        public BebidasController(DatabasePrologContext context)
        {
            _context = context;
        }

        // GET: api/Bebidas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bebidas>>> GetBebidas()
        {
            return await _context.Bebidas.ToListAsync();
        }

        // GET: api/Bebidas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bebidas>> GetBebidas(int id)
        {
            Bebidas bebidas = await _context.Bebidas.FindAsync(id);

            if (bebidas == null)
            {
                return NotFound();
            }

            return bebidas;
        }

        // PUT: api/Bebidas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBebidas(int id, Bebidas bebidas)
        {
            if (id != bebidas.IdBebida)
            {
                return BadRequest();
            }

            _context.Entry(bebidas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BebidasExists(id))
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

        // POST: api/Bebidas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Bebidas>> PostBebidas(Bebidas bebidas)
        {
            _context.Bebidas.Add(bebidas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBebidas", new { id = bebidas.IdBebida }, bebidas);
        }

        // DELETE: api/Bebidas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bebidas>> DeleteBebidas(int id)
        {
            Bebidas bebidas = await _context.Bebidas.FindAsync(id);
            if (bebidas == null)
            {
                return NotFound();
            }

            _context.Bebidas.Remove(bebidas);
            await _context.SaveChangesAsync();

            return bebidas;
        }

        private bool BebidasExists(int id)
        {
            return _context.Bebidas.Any(e => e.IdBebida == id);
        }
    }
}
