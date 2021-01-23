using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ProyectoFinalRocioApi.PrologDb
{
    public partial class Temporada
    {
        public Temporada()
        {
            Bebidas = new HashSet<Bebidas>();
        }

        public int IdTemporada { get; set; }
        public string Temporada1 { get; set; }

        public virtual ICollection<Bebidas> Bebidas { get; set; }
    }
}
