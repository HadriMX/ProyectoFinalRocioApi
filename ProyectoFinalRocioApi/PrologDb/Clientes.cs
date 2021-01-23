using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ProyectoFinalRocioApi.PrologDb
{
    public partial class Clientes
    {
        public Clientes()
        {
            HistorialConsumo = new HashSet<HistorialConsumo>();
        }

        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public DateTime UltimaVisita { get; set; }

        public virtual ICollection<HistorialConsumo> HistorialConsumo { get; set; }
    }
}
