using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ProyectoFinalRocioApi.PrologDb
{
    public partial class HistorialConsumo
    {
        public int IdHistorial { get; set; }
        public int IdCliente { get; set; }
        public int IdBebida { get; set; }
        public int IdTamano { get; set; }
        public DateTime Fecha { get; set; }

        public virtual Bebidas IdBebidaNavigation { get; set; }
        public virtual Clientes IdClienteNavigation { get; set; }
        public virtual Tamanos IdTamanoNavigation { get; set; }
    }
}
