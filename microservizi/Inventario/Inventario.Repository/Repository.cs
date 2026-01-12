using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Inventario.Repository.Abstraction;
using Inventario.Repository.Model;

namespace Inventario.Repository
{
    public class Repository(InventarioDbContext inventarioDbContext) : IRepository
    {
        // Articoli
        // Save state
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await inventarioDbContext.SaveChangesAsync(cancellationToken);
        }
        // Create Libro
        public async Task<Libro> CreateLibroAsync(string titolo, string trama, string autore, decimal prezzo, int quantita, int fk_fornitore, CancellationToken cancellationToken = default)
        {
            Libro libro = new Libro
            {
                Titolo = titolo,
                Trama = trama,
                Autore = autore,
                Prezzo = prezzo,
                QuantitaDisponibile = quantita,
                DataInserimento = DateTime.UtcNow,
                Fk_fornitore = fk_fornitore
            };
            await inventarioDbContext.Articoli.AddAsync(libro, cancellationToken);

            return libro;
        }
        // Read Libro
        public async Task<Libro?> ReadLibroAsync(int id, CancellationToken cancellationToken = default)
        {
            return await inventarioDbContext.Articoli
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
        }
        public async Task<List<Libro>> ReadAllLibriAsync(CancellationToken cancellationToken = default)
        {
            return await inventarioDbContext.Articoli
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        public async Task<List<Libro>> ReadLibriAutoreAsync(string autore, CancellationToken cancellationToken = default)
        {
            return await inventarioDbContext.Articoli
                .AsNoTracking()
                .Where(l => l.Autore == autore)
                .ToListAsync(cancellationToken);
        }
        public async Task<List<Libro>> ReadLibriFornitoreAsync(int id_fornitore, CancellationToken cancellationToken = default)
        {
            return await inventarioDbContext.Articoli
                .AsNoTracking()
                .Where(l => l.Fk_fornitore == id_fornitore)
                .ToListAsync(cancellationToken);
        }
        // Update Libro
        public async Task<Libro?> UpdateLibroAsync(int id, string? titolo, string? trama, string? autore, decimal? prezzo, int? fk_fornitore, CancellationToken cancellationToken = default)
        {
            if(prezzo != null && prezzo <= 0)
                throw new ArgumentException("Il prezzo del libro deve essere maggiore di zero");

            Libro? libro = await ReadLibroAsync(id, cancellationToken);
            if (libro == null)
                return null;

            if (titolo != null)
                libro.Titolo = titolo;
            if (trama != null)
                libro.Trama = trama;
            if (autore != null)
                libro.Autore = autore;
            if (prezzo != null)
                libro.Prezzo = prezzo.Value;
            if (fk_fornitore != null)
                libro.Fk_fornitore = fk_fornitore.Value;


            inventarioDbContext.Articoli.Update(libro);

            return libro;
        }
        public async Task<Libro?> UpdateQuantitaLibroAsync(int id, int quantita, CancellationToken cancellationToken = default)
        {
            Libro? libro = await ReadLibroAsync(id, cancellationToken);
            if (libro == null)
                return null;

            if (libro.QuantitaDisponibile + quantita < 0)
                throw new ArgumentOutOfRangeException("La quantita totale non puo essere negativa");

            libro.QuantitaDisponibile += quantita;
            inventarioDbContext.Articoli.Update(libro);

            return libro;
        }
        // Delete Libro
        public async Task DeleteLibroAsync(int id, CancellationToken cancellationToken = default)
        {
            Libro? libro = await ReadLibroAsync(id, cancellationToken);
            if (libro != null)
                inventarioDbContext.Articoli.Remove(libro);
        }

        // Fornitori
        // Create Fornitore
        public async Task<Fornitore> CreateFornitoreAsync(string nome, string indirizzo, string telefono, string email, CancellationToken cancellationToken = default)
        {
            Fornitore fornitore = new Fornitore
            {
                Nome = nome,
                Indirizzo = indirizzo,
                Telefono = telefono,
                Email = email
            };
            await inventarioDbContext.Fornitori.AddAsync(fornitore, cancellationToken);
            return fornitore;
        }
        // Read Fornitore
        public async Task<Fornitore?> ReadFornitoreAsync(int id, CancellationToken cancellationToken = default)
        {
            return await inventarioDbContext.Fornitori
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
        }
        public async Task<List<Fornitore>> ReadAllFornitoriAsync(CancellationToken cancellationToken = default)
        {
            return await inventarioDbContext.Fornitori
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        // Update Fornitore
        public async Task<Fornitore?> UpdateFornitoreAsync(int id, string nome, string indirizzo, string telefono, string email, CancellationToken cancellationToken = default)
        {
            Fornitore? fornitore = await ReadFornitoreAsync(id, cancellationToken);
            if (fornitore == null) 
                return null;
            
            fornitore.Nome = nome;
            fornitore.Indirizzo = indirizzo;
            fornitore.Telefono = telefono;
            fornitore.Email = email;
            inventarioDbContext.Fornitori.Update(fornitore);

            return fornitore;
        }
        // Delete Fornitore
        public async Task DeleteFornitoreAsync(int id, CancellationToken cancellationToken = default)
        {
            Fornitore? fornitore = await ReadFornitoreAsync(id, cancellationToken);
            if (fornitore != null)
                inventarioDbContext.Fornitori.Remove(fornitore);
        }
    }
}
