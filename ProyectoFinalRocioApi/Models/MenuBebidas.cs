using ProyectoFinalRocioApi.PrologDb;

namespace ProyectoFinalRocioApi.Models
{
    public class MenuBebidas
    {
        public BebidaLista Favorita { get; set; }
        public Bebidas BebidaMasVendida { get; set; }
        public Bebidas BebidaMenosVendida { get; set; }
    }
}
