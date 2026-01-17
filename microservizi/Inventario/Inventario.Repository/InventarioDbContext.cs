using Microsoft.EntityFrameworkCore;
using Inventario.Repository.Model;

namespace Inventario.Repository
{
    public class InventarioDbContext(DbContextOptions<InventarioDbContext> dbContextOptions) : DbContext(dbContextOptions)
    {
        // Creazione del modello e configurazione delle entità con le varie relazioni
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Articolo
            modelBuilder.Entity<Libro>(articolo =>
            {
                articolo.ToTable("Libri"); // Tabella di riferimento
                articolo.HasKey(x => x.Id); // Chiave primaria
                articolo.Property(e => e.Id).ValueGeneratedOnAdd(); // Generazione automatica dell'Id   
                articolo.Property(e => e.Titolo).IsRequired().HasMaxLength(255); // Proprietà Titolo
                articolo.Property(e => e.Trama).HasColumnType("TEXT"); // Proprietà Trama
                articolo.Property(e => e.Autore).HasMaxLength(100); // Proprietà Autore
                articolo.Property(e => e.Prezzo).IsRequired().HasColumnType("DECIMAL(10, 2)"); // Proprietà Prezzo
                articolo.Property(e => e.QuantitaDisponibile).IsRequired(); // Proprietà Quantità
                articolo.Property(e => e.DataInserimento).IsRequired().HasDefaultValueSql("NOW()"); // Proprietà DataInserimento
                articolo.HasOne(e => e.Fornitore) // Un articolo ha un solo fornitore
                      .WithMany(f => f.Libri) // Un fornitore può avere più libri
                      .HasForeignKey(e => e.Fk_fornitore) // Chiave esterna
                      .OnDelete(DeleteBehavior.Cascade); // Se un fornitore viene eliminato, vengono eliminati anche i suoi articoli
            });
            // Fornitore
            modelBuilder.Entity<Fornitore>(fornitore =>
            {
                fornitore.ToTable("Fornitori"); // Tabella di riferimento
                fornitore.HasKey(x => x.Id); // Chiave primaria
                fornitore.Property(e => e.Id).ValueGeneratedOnAdd(); // Generazione automatica dell'Id
                fornitore.Property(e => e.Nome).IsRequired().HasMaxLength(255); // Proprietà Nome
                fornitore.Property(e => e.Indirizzo).HasMaxLength(255); // Proprietà Indirizzo
                fornitore.Property(e => e.Telefono).HasMaxLength(15); // Proprietà Telefono
                fornitore.Property(e => e.Email).IsRequired().HasMaxLength(100); // Proprietà Email
            });
        }
        // DbSet per le entità
        public DbSet<Libro> Articoli { get; set; }
        public DbSet<Fornitore> Fornitori { get; set; }
    }
}
