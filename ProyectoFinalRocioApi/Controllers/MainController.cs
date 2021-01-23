using CliWrap;
using CliWrap.Buffered;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinalRocioApi.Models;
using ProyectoFinalRocioApi.PrologDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalRocioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly DatabasePrologContext _context;

        public MainController(DatabasePrologContext context)
        {
            _context = context;
        }

        private static async Task<string> RunProlog(string accion, string arg1 = "0", string arg2 = "", string arg3 = "")
        {
            BufferedCommandResult result = await Cli.Wrap("swipl")
                            .WithArguments(new[] { "-q", "-t", accion, "prolog.pl", arg1, arg2, arg3 })
                            .WithValidation(CommandResultValidation.None)
                            .ExecuteBufferedAsync(Encoding.UTF8);

            var output = result.StandardOutput;
            return output;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var favorita = await CombinacionFavorita(id);
            var masVendida = await MasVendida();
            var menosVendida = await MenosVendida();

            return Ok(new MenuBebidas
            {
                Favorita = favorita,
                BebidaMasVendida = masVendida,
                BebidaMenosVendida = menosVendida
            });
        }

        [HttpGet("mas-vendida")]
        public async Task<ActionResult> GetMasVendida()
        {
            Bebidas bebida = await MasVendida();
            return Ok(bebida);
        }

        private async Task<Bebidas> MasVendida()
        {
            var output = await RunProlog("main2");
            var valores = new Dictionary<string, int>();

            foreach (var item in output.Split("\r\n"))
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    var pair = item.Split(',');
                    valores.Add(pair[0], Convert.ToInt32(pair[1]));
                }
            }

            var idBebida = int.Parse(valores.First(x => x.Value == valores.Values.Max()).Key);
            var bebida = await _context.Bebidas.FindAsync(idBebida);
            return bebida;
        }

        [HttpGet("menos-vendida")]
        public async Task<ActionResult> GetMenosVendida()
        {
            Bebidas bebida = await MenosVendida();
            return Ok(bebida);
        }

        private async Task<Bebidas> MenosVendida()
        {
            var output = await RunProlog("main2");
            var valores = new Dictionary<string, int>();

            foreach (var item in output.Split("\r\n"))
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    var pair = item.Split(',');
                    valores.Add(pair[0], Convert.ToInt32(pair[1]));
                }
            }

            var idBebida = int.Parse(valores.First(x => x.Value == valores.Values.Min()).Key);
            var bebida = await _context.Bebidas.FindAsync(idBebida);
            return bebida;
        }

        [HttpGet("combinacion-favorita/{id}")]
        public async Task<ActionResult> GetCombinacionFavorita(int id)
        {
            var bebidaLista = await CombinacionFavorita(id);
            return Ok(bebidaLista);
        }

        private async Task<BebidaLista> CombinacionFavorita(int id)
        {
            var output = await RunProlog("run_combinacion_favorita", id.ToString());
            var split = output.Split(",");
            var idBebida = int.Parse(split[1].Trim(new[] { '\r', '\n' }));
            var idTamano = int.Parse(split[0].Trim(new[] { '\r', '\n' }));

            return new BebidaLista
            {
                Bebida = await _context.Bebidas.FindAsync(idBebida),
                Tamano = await _context.Tamanos.FindAsync(idTamano)
            };
        }

        //[HttpGet("combinacion-favorita/{id}")]
        //public async Task<ActionResult> GetCombinacionFavorita(int id)
        //{
        //    var output = await RunProlog("run_combinacion_favorita", id.ToString());
        //    var split = output.Split(",");
        //    var idBebida = int.Parse(split[1].Trim(new[] { '\r', '\n' }));
        //    var idTamano = int.Parse(split[0].Trim(new[] { '\r', '\n' }));

        //    return Ok(new BebidaLista
        //    {
        //        Bebida = await _context.Bebidas.FindAsync(idBebida),
        //        Tamano = await _context.Tamanos.FindAsync(idTamano)
        //    });
        //}

        // POST api/<MainController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MainController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MainController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
