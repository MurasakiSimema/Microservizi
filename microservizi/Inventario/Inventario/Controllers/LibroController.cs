using Microsoft.AspNetCore.Mvc;

using Inventario.Business.Abstraction;
using Inventario.Shared;
using Microsoft.Extensions.Logging;
using Inventario.Repository.Model;

namespace Inventario.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class LibroController: ControllerBase
{
    private readonly IBusiness _business;
    private readonly ILogger<LibroController> _logger;

    public LibroController(IBusiness business, ILogger<LibroController> logger)
    {
        _business = business;
        _logger = logger;
    }

    // Create
    [HttpPost(Name = "CreateLibro")]
    public async Task<ActionResult> CreateLibro(string titolo, string trama, string autore, decimal prezzo, int quantita, int fk_fornitore)
    {
        if (string.IsNullOrWhiteSpace(titolo))
            return new JsonResult(new { message = "Titolo obbligatorio" }) { StatusCode = 400 };
        if (prezzo < 0)
            return new JsonResult(new { message = "Il prezzo non puo essere negativo" }) { StatusCode = 400 };
        if (quantita < 0)
            return new JsonResult(new { message = "La quantita non puo essere negativa" }) { StatusCode = 400 };

        try
        {
            await _business.CreateLibroAsync(titolo, trama, autore, prezzo, quantita, fk_fornitore);
            return new JsonResult(new { message = "Libro creato" }) { StatusCode = 200 };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Errore durante la creazione del libro");
            return new JsonResult(new { message = "Interal server error" }) { StatusCode = 500 };
        }
    }

    // Read 
    [HttpGet(Name = "ReadLibro")]
    public async Task<ActionResult<LibroDto>> ReadLibro(int id)
    {
        try
        {
            var libro = await _business.ReadLibroAsync(id);
            if (libro == null)
                return new JsonResult(new { message = $"Libro con ID '{id}' non trovato" }) { StatusCode = 404 };

            return new JsonResult(libro) { StatusCode = 200 };
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Errore durante la lettura del libro con ID '{id}'");
            return new JsonResult(new { message = "Interal server error" }) { StatusCode = 500 };
        }
    }

    [HttpGet(Name = "ReadAllLibri")]
    public async Task<ActionResult<List<LibroDto>>> ReadAllLibri()
    {
        try
        {
            var libri = await _business.ReadAllLibriAsync();
            if (libri == null || !libri.Any())
                return new JsonResult(new { message = "Nessun libro trovato" }) { StatusCode = 404 };

            return new JsonResult(libri) { StatusCode = 200 };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Errore durante il recupero della lista dei libri");
            return new JsonResult(new { message = "Internal server error" }) { StatusCode = 500 };
        }
    }

    [HttpGet(Name = "ReadLibriAutore")]
    public async Task<ActionResult<List<LibroDto>>> ReadLibriAutore(string autore)
    {
        try
        {
            var libri = await _business.ReadLibriAutoreAsync(autore);
            if(libri == null || !libri.Any())
                return new JsonResult(new { message = "Nessun libro trovato" }) { StatusCode = 404 };

            return new JsonResult(libri) { StatusCode = 200 };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Errore durante il recupero della lista dei libri");
            return new JsonResult(new { message = "Internal server error" }) { StatusCode = 500 };
        }
    }

    [HttpGet(Name = "ReadLibriFornitore")]
    public async Task<ActionResult<List<LibroDto>>> ReadLibriFornitore(int id_fornitore)
    {
        try
        {
            var libri = await _business.ReadLibriFornitoreAsync(id_fornitore);
            if(libri == null || !libri.Any())
                return new JsonResult(new { message = "Nessun libro trovato" }) { StatusCode = 404 };

            return new JsonResult(libri) { StatusCode = 200 };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Errore durante il recupero della lista dei libri");
            return new JsonResult(new { message = "Internal server error" }) { StatusCode = 500 };
        }
    }

    // Update
    [HttpPut(Name = "UpdateLibro")]
    public async Task<ActionResult<LibroDto>> UpdateLibro(int id, [FromBody] LibroDto libroDto)
    {
        if (libroDto == null)
            return new JsonResult(new { message = "Dati libro non validi" }) { StatusCode = 400 };

        if (libroDto.Prezzo < 0)
            return new JsonResult(new { message = "Il prezzo non puo essere negativo" }) { StatusCode = 400 };
        if (libroDto.QuantitaDisponibile < 0)
            return new JsonResult(new { message = "La quantita non puo essere negativa" }) { StatusCode = 400 };

        try
        {
            var libro = await _business.UpdateLibroAsync(id, libroDto.Titolo, libroDto.Trama, libroDto.Autore, libroDto.Prezzo, libroDto.Fk_fornitore);
            if(libro == null)
                return new JsonResult(new { message = $"Libro con ID '{id}' non trovato" }) { StatusCode = 404 };

            return new JsonResult(libro) { StatusCode = 404 };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Errore durante l'aggiornamento del libro");
            return new JsonResult(new { message = "Internal server error" }) { StatusCode = 500 };
        }
    }

    [HttpPut(Name = "UpdateQuantitaLibro")]
    public async Task<ActionResult<LibroDto>> UpdateQuantitaLibro(int id, int quantita)
    {
        try
        {
            var libro = await _business.UpdateQuantitaLibroAsync(id, quantita);
            if(libro == null)
                return new JsonResult(new { message = $"Libro con ID '{id}' non trovato" }) { StatusCode = 404 };

            return new JsonResult(libro) { StatusCode = 404 };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Errore durante l'aggiornamento della quantità del libro");
            return new JsonResult(new { message = "Internal server error" }) { StatusCode = 500 };
        }
    }

    // Delete
    [HttpDelete(Name = "DeleteLibro")]
    public async Task<ActionResult> DeleteLibro(int id)
    {
        try
        {
            await _business.DeleteLibroAsync(id);
            return new JsonResult(new { message = $"Libro con ID '{id}' eliminato correttamente" }) { StatusCode = 200 };
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Errore durante l'eliminazione del libro con ID '{id}'");
            return new JsonResult(new { message = "Internal server error" }) { StatusCode = 500 };
        }
    }
}
