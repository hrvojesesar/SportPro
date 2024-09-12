using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
[Authorize(Roles = "Uposlenik")]
public class PozicijeController : Controller
{
    private readonly IPozicijeRepository _pozicijeRepository;

    public PozicijeController(IPozicijeRepository pozicijeRepository)
    {
        _pozicijeRepository = pozicijeRepository;
    }

    /// <summary>
    /// Prikaz svih pozicija
    /// </summary>
    /// <param name="searchQuery"></param>
    /// <param name="sortBy"></param>
    /// <param name="sortDirection"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Pozicije>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Index(string? searchQuery, string? sortBy, string? sortDirection, int pageSize = 5, int pageNumber = 1)
    {
        var totalRecords = await _pozicijeRepository.CountAsync();
        var totalPages = Math.Ceiling((decimal)totalRecords / pageSize);

        if (pageNumber > totalPages)
        {
            pageNumber--;
        }

        if (pageNumber < 1)
        {
            pageNumber++;
        }

        ViewBag.TotalPages = totalPages;

        ViewBag.SearchQuery = searchQuery;

        ViewBag.SortBy = sortBy;
        ViewBag.SortDirection = sortDirection;

        ViewBag.PageSize = pageSize;
        ViewBag.PageNumber = pageNumber;

        var pozicije = await _pozicijeRepository.GetAllAsync(searchQuery, sortBy, sortDirection, pageNumber, pageSize);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(pozicije);
        }


        return View(pozicije);
    }

    /// <summary>
    /// Dohvaćanje view-a za dodavanje pozicije
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    /// <summary>
    /// Dodavanje pozicije
    /// </summary>
    /// <param name="addPozicijaRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Pozicije>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add(AddPozicijaRequest addPozicijaRequest)
    {
        ValidatePozicijaForAdd(addPozicijaRequest);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var pozicija = new Pozicije
        {
            Naziv = addPozicijaRequest.Naziv,
            Opis = addPozicijaRequest.Opis
        };

        await _pozicijeRepository.AddAsync(pozicija);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(pozicija);
        }

        return RedirectToAction("Index", new { id = pozicija.IDPozicija });
    }

    /// <summary>
    /// Dohvaćanje view-a za uređivanje pozicije
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pozicija = await _pozicijeRepository.GetAsync(id);
        if (pozicija == null)
        {
            return NotFound();
        }

        var editPozicijaRequest = new EditPozicijaRequest
        {
            IDPozicija = pozicija.IDPozicija,
            Naziv = pozicija.Naziv,
            Opis = pozicija.Opis
        };

        return View(editPozicijaRequest);
    }

    /// <summary>
    /// Uređivanje pozicije
    /// </summary>
    /// <param name="editPozicijaRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Pozicije>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Edit(EditPozicijaRequest editPozicijaRequest)
    {
        var pozicija = new Pozicije
        {
            IDPozicija = editPozicijaRequest.IDPozicija,
            Naziv = editPozicijaRequest.Naziv,
            Opis = editPozicijaRequest.Opis
        };

        ValidatePozicijaForEdit(pozicija);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _pozicijeRepository.UpdateAsync(pozicija);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(pozicija);
        }

        return RedirectToAction("Index", new { id = pozicija.IDPozicija });
    }

    /// <summary>
    /// Dohvaćanje view-a za brisanje pozicije
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pozicija = await _pozicijeRepository.GetAsync(id);
        if (pozicija == null)
        {
            return NotFound();
        }

        return View(pozicija);
    }

    /// <summary>
    /// Brisanje pozicije
    /// </summary>
    /// <param name="editPozicijaRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Pozicije>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(EditPozicijaRequest editPozicijaRequest)
    {
        var pozicija = await _pozicijeRepository.DeleteAsync(editPozicijaRequest.IDPozicija);

        if (pozicija == null)
        {
            return NotFound();
        }

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok("Pozicija je uspješno uklonjena!");
        }

        return RedirectToAction("Index", new { id = editPozicijaRequest.IDPozicija });
    }

    private void ValidatePozicijaForAdd(AddPozicijaRequest addPozicijaRequest)
    {
        if (addPozicijaRequest.Naziv.Length > 20)
        {
            ModelState.AddModelError("Naziv", "Naziv ne može biti duži od 20 znakova.");
        }
    }

    private void ValidatePozicijaForEdit(Pozicije pozicije)
    {
        if (pozicije.Naziv != null && pozicije.Naziv.Length > 20)
        {
            ModelState.AddModelError("Naziv", "Naziv ne može biti duži od 20 znakova.");
        }
    }
}
