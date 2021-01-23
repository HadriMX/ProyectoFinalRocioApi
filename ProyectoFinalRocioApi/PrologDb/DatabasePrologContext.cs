using Microsoft.EntityFrameworkCore;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ProyectoFinalRocioApi.PrologDb
{
    public partial class DatabasePrologContext : DbContext
    {
        public DatabasePrologContext()
        {
        }

        public DatabasePrologContext(DbContextOptions<DatabasePrologContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bebidas> Bebidas { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<HistorialConsumo> HistorialConsumo { get; set; }
        public virtual DbSet<Tamanos> Tamanos { get; set; }
        public virtual DbSet<Temporada> Temporada { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseMySQL("server=wolvesoftware.com;uid=wolvesof_prlg;pwd=qwerty;database=wolvesof_prolog");
                optionsBuilder.UseMySQL("server=localhost;uid=root;pwd=qwerty;database=prolog");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bebidas>(entity =>
            {
                entity.HasKey(e => e.IdBebida)
                    .HasName("PRIMARY");

                entity.ToTable("bebidas");

                entity.HasIndex(e => e.IdTemporada)
                    .HasName("id_temporada");

                entity.Property(e => e.IdBebida)
                    .HasColumnName("id_bebida")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdTemporada)
                    .HasColumnName("id_temporada")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LtContenedor).HasColumnName("lt_contenedor");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnName("tipo")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdTemporadaNavigation)
                    .WithMany(p => p.Bebidas)
                    .HasForeignKey(d => d.IdTemporada)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bebidas_ibfk_1");
            });

            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PRIMARY");

                entity.ToTable("clientes");

                entity.Property(e => e.IdCliente)
                    .HasColumnName("id_cliente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NombreCliente)
                    .IsRequired()
                    .HasColumnName("nombre_cliente")
                    .HasMaxLength(50);

                entity.Property(e => e.UltimaVisita)
                    .HasColumnName("ultima_visita")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<HistorialConsumo>(entity =>
            {
                entity.HasKey(e => e.IdHistorial)
                    .HasName("PRIMARY");

                entity.ToTable("historial_consumo");

                entity.HasIndex(e => e.IdBebida)
                    .HasName("id_bebida");

                entity.HasIndex(e => e.IdCliente)
                    .HasName("id_cliente");

                entity.HasIndex(e => e.IdTamano)
                    .HasName("id_tamaño");

                entity.Property(e => e.IdHistorial)
                    .HasColumnName("id_historial")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("date");

                entity.Property(e => e.IdBebida)
                    .HasColumnName("id_bebida")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdCliente)
                    .HasColumnName("id_cliente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdTamano)
                    .HasColumnName("id_tamano")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdBebidaNavigation)
                    .WithMany(p => p.HistorialConsumo)
                    .HasForeignKey(d => d.IdBebida)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("historial_consumo_ibfk_2");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.HistorialConsumo)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("historial_consumo_ibfk_1");

                entity.HasOne(d => d.IdTamanoNavigation)
                    .WithMany(p => p.HistorialConsumo)
                    .HasForeignKey(d => d.IdTamano)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("historial_consumo_ibfk_3");
            });

            modelBuilder.Entity<Tamanos>(entity =>
            {
                entity.HasKey(e => e.IdTamano)
                    .HasName("PRIMARY");

                entity.ToTable("tamanos");

                entity.Property(e => e.IdTamano)
                    .HasColumnName("id_tamano")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AproxMl)
                    .IsRequired()
                    .HasColumnName("aprox_ml")
                    .HasMaxLength(50);

                entity.Property(e => e.TamanoBebida)
                    .IsRequired()
                    .HasColumnName("tamano_bebida")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Temporada>(entity =>
            {
                entity.HasKey(e => e.IdTemporada)
                    .HasName("PRIMARY");

                entity.ToTable("temporada");

                entity.Property(e => e.IdTemporada)
                    .HasColumnName("id_temporada")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Temporada1)
                    .IsRequired()
                    .HasColumnName("temporada")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
