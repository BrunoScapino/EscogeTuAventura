using Microsoft.EntityFrameworkCore;
using EscogeTuAventura.Entidades;

namespace LogicaAccesoDatos.EF
{
    public class EscogeTuAventuraContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Historia> Historias { get; set; }
        public DbSet<Capitulo> Capitulos { get; set; }
        public DbSet<Opcion> Opciones { get; set; }
        public DbSet<Lectura> Lecturas { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EscogeTuAventura;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .OwnsOne(usuario => usuario.Password);

            modelBuilder.Entity<Opcion>()
                .HasOne(opcion => opcion.CapituloOrigen)
                .WithMany(capitulo => capitulo.Opciones)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Opcion>()
                .HasOne(opcion => opcion.CapituloDestino)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Capitulo>()
                .HasOne(capitulo => capitulo.Historia)
                .WithMany(historia => historia.Capitulos)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Historia>()
                .Property<int?>("CapituloInicialId");

            modelBuilder.Entity<Historia>()
                .HasOne(historia => historia.CapituloInicial)
                .WithMany()
                .HasForeignKey("CapituloInicialId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Historia>()
                .HasMany(historia => historia.Categorias)
                .WithMany()
                .UsingEntity("HistoriaCategoria");
        }
    }
}