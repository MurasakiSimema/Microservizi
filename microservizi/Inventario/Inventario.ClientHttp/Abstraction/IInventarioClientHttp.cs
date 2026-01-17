using Inventario.Shared;

namespace Inventario.ClientHttp.Abstraction;

public interface IInventarioClientHttp
{
    Task<string?> CreateLibroAsync(LibroDto libroDto, CancellationToken cancellationToken = default);
    Task<LibroDto?> GetLibroAsync(int id, CancellationToken cancellationToken = default);
    Task<LibroDto?> UpdateQuantitaLibroAsync(int id, int quantita, CancellationToken cancellationToken = default);
}
