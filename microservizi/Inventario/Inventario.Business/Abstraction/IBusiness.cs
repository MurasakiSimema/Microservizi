using Inventario.Shared;

namespace Inventario.Business.Abstraction;
public interface IBusiness
{
    // Libri
    // Create
    Task CreateLibroAsync(string titolo, string trama, string autore, decimal prezzo, int quantita, int fk_fornitore, CancellationToken cancellationToken = default);
    // Read
    Task<LibroDto?> ReadLibroAsync(int id, CancellationToken cancellationToken = default);
    Task<List<LibroDto>> ReadAllLibriAsync(CancellationToken cancellationToken = default);
    Task<List<LibroDto>> ReadLibriAutoreAsync(string autore, CancellationToken cancellationToken = default);
    Task<List<LibroDto>> ReadLibriFornitoreAsync(int id_fornitore, CancellationToken cancellationToken = default);
    // Update
    Task<LibroDto?> UpdateLibroAsync(int id, string titolo, string trama, string autore, decimal prezzo, int fk_fornitore, CancellationToken cancellationToken = default);
    Task<LibroDto?> UpdateQuantitaLibroAsync(int id, int quantita, CancellationToken cancellationToken = default);
    // Delete
    Task DeleteLibroAsync(int id, CancellationToken cancellationToken = default);

    // Fornitori
    // Create
    Task CreateFornitoreAsync(string nome, string indirizzo, string telefono, string email, CancellationToken cancellationToken = default);
    // Read
    Task<FornitoreDto?> ReadFornitoreAsync(int id, CancellationToken cancellationToken = default);
    Task<List<FornitoreDto>> ReadAllFornitoriAsync(CancellationToken cancellationToken = default);
    // Update
    Task<FornitoreDto?> UpdateFornitoreAsync(int id, string nome, string indirizzo, string telefono, string email, CancellationToken cancellationToken = default);
    // Delete
    Task DeleteFornitoreAsync(int id, CancellationToken cancellationToken = default);
}
