using Microsoft.AspNetCore.Mvc;

using Inventario.Business.Abstraction;
using Inventario.Shared;
using Microsoft.Extensions.Logging;
using Inventario.Repository.Model;

namespace Inventario.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class FornitoreController : ControllerBase
{
    private readonly IBusiness _business;
    private readonly ILogger<FornitoreController> _logger;

    public FornitoreController(IBusiness business, ILogger<FornitoreController> logger)
    {
        _business = business;
        _logger = logger;
    }

    // Create
    [HttpPost(Name = "CreateFornitore")]
    public async Task<ActionResult> CreateFornitore(string nome, string indirizzo, string telefono, string email)
    {
        if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(email))
            return new JsonResult(new { message = "Nome ed email sono obbligatori" }) { StatusCode = 400 };

        try
        {
            await _business.CreateFornitoreAsync(nome, indirizzo, telefono, email);
            return new JsonResult(new { message = "Fornitore creato" }) { StatusCode = 200 };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Errore durante la creazione del fornitore");
            return new JsonResult(new { message = "Interal server error" }) { StatusCode = 500 };
        }
    }

    // Read
    [HttpGet(Name = "ReadFornitore")]
    public async Task<ActionResult<FornitoreDto>> ReadFornitore(int id)
    {
        try
        {
            var fornitore = await _business.ReadFornitoreAsync(id);
            if (fornitore == null)
                return new JsonResult(new { message = $"Fornitore con ID '{id}' non trovato" }) { StatusCode = 404 };

            return new JsonResult(fornitore) { StatusCode = 200 };
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Errore durante la lettura del fornitore con ID '{id}'");
            return new JsonResult(new { message = "Interal server error" }) { StatusCode = 500 };
        }
    }

    [HttpGet(Name = "ReadAllFornitoriAsync")]
    public async Task<ActionResult<List<FornitoreDto>>> ReadAllFornitoriAsync()
    {
        try
        {
            var fornitori = await _business.ReadAllFornitoriAsync();
            if (fornitori == null || !fornitori.Any())
                return new JsonResult(new { message = "Nessun fornitore trovato" }) { StatusCode = 404 };

            return new JsonResult(fornitori) { StatusCode = 200 };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Errore durante il recupero della lista dei fornitori");
            return new JsonResult(new { message = "Internal server error" }) { StatusCode = 500 };
        }
    }

    // Update
    [HttpPut(Name = "UpdateFornitore")]
    public async Task<ActionResult<FornitoreDto>> UpdateFornitore(int id, [FromBody] FornitoreDto fornitoreDto)
    {
        if (fornitoreDto == null)
            return new JsonResult(new { message = "Dati fornitore non validi" }) { StatusCode = 400 };

        try
        {
            var fornitore = await _business.UpdateFornitoreAsync(id, fornitoreDto.Nome, fornitoreDto.Indirizzo, fornitoreDto.Telefono, fornitoreDto.Email);
            if (fornitore == null)
                return new JsonResult(new { message = $"Fornitore con ID '{id}' non trovato" }) { StatusCode = 404 };

            return new JsonResult(fornitore) { StatusCode = 200 };
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Errore durante l'aggiornamento del fornitore con ID '{id}'");
            return new JsonResult(new { message = "Internal server error" }) { StatusCode = 500 };
        }
    }

    // Delete
    [HttpDelete(Name = "DeleteFornitore")]
    public async Task<ActionResult> DeleteFornitore(int id)
    {
        try
        {
            await _business.DeleteFornitoreAsync(id);
            return new JsonResult(new { message = $"Fornitore con ID '{id}' eliminato correttamente" }) { StatusCode = 200 };
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Errore durante l'eliminazione del fornitore con ID '{id}'");
            return new JsonResult(new { message = "Internal server error" }) { StatusCode = 500 };
        }
    }
}
