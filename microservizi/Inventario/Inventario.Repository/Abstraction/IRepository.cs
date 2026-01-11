using Inventario.Repository.Model;

namespace Inventario.Repository.Abstraction
{
    public interface IRepository
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        // Libri
        // Create
        Task<Libro> CreateLibroAsync(string titolo, string trama, string autore, decimal prezzo, int quantita, int fk_fornitore, CancellationToken cancellationToken = default);
        // Read
        Task<Libro?> ReadLibroAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Libro>> ReadAllLibriAsync(CancellationToken cancellationToken = default);
        Task<List<Libro>> ReadLibriAutoreAsync(string autore, CancellationToken cancellationToken = default);
        Task<List<Libro>> ReadLibriFornitoreAsync(int id_fornitore, CancellationToken cancellationToken = default);
        // Update
        Task<Libro?> UpdateLibroAsync(int id, string? titolo, string? trama, string? autore, decimal? prezzo, int? fk_fornitore, CancellationToken cancellationToken = default);
        Task<Libro?> UpdateQuantitaLibroAsync(int id, int quantita, CancellationToken cancellationToken = default);
        // Delete
        Task DeleteLibroAsync(int id, CancellationToken cancellationToken = default);

        // Fornitori
        // Create
        Task<Fornitore> CreateFornitoreAsync(string nome, string indirizzo, string telefono, string email, CancellationToken cancellationToken = default);
        // Read
        Task<Fornitore?> ReadFornitoreAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Fornitore>> GetAllFornitoriAsync(CancellationToken cancellationToken = default);
        // Update
        Task UpdateFornitoreAsync(int id, string nome, string indirizzo, string telefono, string email, CancellationToken cancellationToken = default);
        // Delete
        Task DeleteFornitoreAsync(int id, CancellationToken cancellationToken = default);
    }
}
