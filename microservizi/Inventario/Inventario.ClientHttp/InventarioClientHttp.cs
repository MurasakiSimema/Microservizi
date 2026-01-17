using Inventario.ClientHttp.Abstraction;
using Inventario.Shared;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Net.Http.Json;

namespace Inventario.ClientHttp;

public class InventarioClientHttp(HttpClient httpClient) : IInventarioClientHttp
{
    public async Task<string?> CreateLibroAsync(LibroDto libroDto, CancellationToken cancellationToken = default)
    {
        var res = await httpClient.PostAsync($"/Libro/CreateLibro", JsonContent.Create(libroDto), cancellationToken);
        return await res.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<string>(cancellationToken);
    }
    public async Task<LibroDto?> GetLibroAsync(int id, CancellationToken cancellationToken = default) 
    {
        var queryString = QueryString.Create(new Dictionary<string, string?>()
        {
            { "id", id.ToString(CultureInfo.InvariantCulture) }
        });
        var res = await httpClient.GetAsync($"/Libro/ReadArticolo{queryString}", cancellationToken);

        return await res.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<LibroDto?>(cancellationToken);
    }
    public async Task<LibroDto?> UpdateQuantitaLibroAsync(int id, int quantita, CancellationToken cancellationToken = default)
    {
        var queryString = QueryString.Create(new Dictionary<string, string?>() {
            { "id", id.ToString(CultureInfo.InvariantCulture) },
            { "quantita", quantita.ToString(CultureInfo.InvariantCulture) }
        });
        var res = await httpClient.PutAsync($"/Libro/UpdateQuantitaLibro{queryString}", null, cancellationToken);

        return await res.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<LibroDto?>(cancellationToken);
    }
}
