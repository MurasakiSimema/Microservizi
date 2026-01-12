using Inventario.Repository.Model;
using Inventario.Shared;

namespace Inventario.Business;

public static class MyMapper
{
    public static LibroDto MapToDto(this Libro libro)
    {
        return new LibroDto
        {
            Id = libro.Id,
            Titolo = libro.Titolo,
            Trama = libro.Trama,
            Autore = libro.Autore,
            Prezzo = libro.Prezzo,
            QuantitaDisponibile = libro.QuantitaDisponibile,
            Fk_fornitore = libro.Fk_fornitore
        };
    }
    public static FornitoreDto MapToDto(this Fornitore fornitore)
    {
        return new FornitoreDto
        {
            Id = fornitore.Id,
            Nome = fornitore.Nome,
            Indirizzo = fornitore.Indirizzo,
            Telefono = fornitore.Telefono,
            Email = fornitore.Email
        };
    }
}
