using Microsoft.Extensions.Logging;
using Inventario.Repository.Abstraction;
using Inventario.Business.Abstraction;
using Inventario.Shared;

namespace Inventario.Business;
public class Business(IRepository repository, ILogger<Business> logger) : IBusiness
{
    // Libri
    // Create
    public async Task CreateLibroAsync(string titolo, string trama, string autore, decimal prezzo, int quantita, int fk_fornitore, CancellationToken cancellationToken = default)
    {
        await repository.CreateLibroAsync(titolo, trama, autore, prezzo, quantita, fk_fornitore, cancellationToken);
        
        await repository.SaveChangesAsync(cancellationToken);
    }
    // Read
    public async Task<LibroDto?> ReadLibroAsync(int id, CancellationToken cancellationToken = default)
    {
        var libro = await repository.ReadLibroAsync(id, cancellationToken);
        if(libro == null)
            return null;

        return libro.MapToDto();
    }
    public async Task<List<LibroDto>> ReadAllLibriAsync(CancellationToken cancellationToken = default)
    {
        var libri = await repository.ReadAllLibriAsync(cancellationToken);

        var libriDto = new List<LibroDto>();
        foreach( var libro in libri) 
            libriDto.Add(libro.MapToDto());

        return libriDto;
    }
    public async Task<List<LibroDto>> ReadLibriAutoreAsync(string autore, CancellationToken cancellationToken = default)
    {
        var libri = await repository.ReadLibriAutoreAsync(autore, cancellationToken);

        var libriDto = new List<LibroDto>();
        foreach (var libro in libri)
            libriDto.Add(libro.MapToDto());

        return libriDto;
    }
    public async Task<List<LibroDto>> ReadLibriFornitoreAsync(int id_fornitore, CancellationToken cancellationToken = default)
    {
        var libri = await repository.ReadLibriFornitoreAsync(id_fornitore, cancellationToken);

        var libriDto = new List<LibroDto>();
        foreach (var libro in libri)
            libriDto.Add(libro.MapToDto());

        return libriDto;
    }
    // Update
    public async Task<LibroDto?> UpdateLibroAsync(int id, string titolo, string trama, string autore, decimal prezzo, int fk_fornitore, CancellationToken cancellationToken = default)
    {
        var libro = await repository.UpdateLibroAsync(id, titolo, trama, autore, prezzo, fk_fornitore, cancellationToken);
        if(libro == null)
            return null;
        
        await repository.SaveChangesAsync(cancellationToken);

        return libro.MapToDto();
    }
    public async Task<LibroDto?> UpdateQuantitaLibroAsync(int id, int quantita, CancellationToken cancellationToken = default)
    {
        var libro = await repository.UpdateQuantitaLibroAsync(id, quantita, cancellationToken);
        if(libro == null)
            return null;

        await repository.SaveChangesAsync(cancellationToken);

        return libro.MapToDto();
    }
    // Delete
    public async Task DeleteLibroAsync(int id, CancellationToken cancellationToken = default)
    {
        await repository.DeleteLibroAsync(id, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
    }

    // Fornitori
    // Create
    public async Task CreateFornitoreAsync(string nome, string indirizzo, string telefono, string email, CancellationToken cancellationToken = default)
    {
        await repository.CreateFornitoreAsync(nome, indirizzo, telefono, email, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
    }
    // Read
    public async Task<FornitoreDto?> ReadFornitoreAsync(int id, CancellationToken cancellationToken = default)
    {
        var fornitore = await repository.ReadFornitoreAsync(id, cancellationToken);
        if (fornitore == null)
            return null;

        return fornitore.MapToDto();
    }
    public async Task<List<FornitoreDto>> GetAllFornitoriAsync(CancellationToken cancellationToken = default)
    {
        var fornitori = await repository.ReadAllFornitoriAsync(cancellationToken);

        var fornitoriDto = new List<FornitoreDto>();
        foreach (var fornitore in fornitori)
            fornitoriDto.Add(fornitore.MapToDto());

        return fornitoriDto;
    }
    // Update
    public async Task<FornitoreDto?> UpdateFornitoreAsync(int id, string nome, string indirizzo, string telefono, string email, CancellationToken cancellationToken = default)
    {
        var fornitore = await repository.UpdateFornitoreAsync(id, nome, indirizzo, telefono, email, cancellationToken);
        if(fornitore == null) 
            return null;
        
        await repository.SaveChangesAsync(cancellationToken);

        return fornitore.MapToDto();
    }
    // Delete
    public async Task DeleteFornitoreAsync(int id, CancellationToken cancellationToken = default)
    {
        await repository.DeleteFornitoreAsync(id, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
    }
}
