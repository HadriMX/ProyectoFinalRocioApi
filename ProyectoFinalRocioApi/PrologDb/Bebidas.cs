using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ProyectoFinalRocioApi.PrologDb
{
    public partial class Bebidas
    {
        public Bebidas()
        {
            HistorialConsumo = new HashSet<HistorialConsumo>();
        }

        public int IdBebida { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public double LtContenedor { get; set; }
        public int IdTemporada { get; set; }

        public virtual Temporada IdTemporadaNavigation { get; set; }
        public virtual ICollection<HistorialConsumo> HistorialConsumo { get; set; }
    }
}
