using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
[Authorize(Roles = "Uposlenik")]
[ApiController]
public class KategorijeController : Controller
{
    private readonly IKategorijeRepository _kategorijeRepository;

    public KategorijeController(IKategorijeRepository kategorijeRepository)
    {
        _kategorijeRepository = kategorijeRepository;
    }

    /// <summary>
    /// Prikaz svih kategorija
    /// </summary>
    /// <param name="searchQuery"></param>
    /// <param name="sortBy"></param>
    /// <param name="sortDirection"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Kategorije>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Index(string? searchQuery, string? sortBy, string? sortDirection, int pageSize = 5, int pageNumber = 1)
    {
        var totalRecords = await _kategorijeRepository.CountAsync();
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

        var kategorije = await _kategorijeRepository.GetAllAsync(searchQuery, sortBy, sortDirection, pageNumber, pageSize);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(kategorije);
        }

        return View(kategorije);
    }

    /// <summary>
    /// Dohvaćanje view-a za dodavanje kategorije
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    /// <summary>
    /// Dodavanje nove kategorije
    /// </summary>
    /// <param name="addKategorijaRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Kategorije>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add(AddKategorijaRequest addKategorijaRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var kategorija = new Kategorije
        {
            Naziv = addKategorijaRequest.Naziv,
            Opis = addKategorijaRequest.Opis
        };

        await _kategorijeRepository.AddAsync(kategorija);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(kategorija);
        }

        return RedirectToAction("Index", new { id = kategorija.IDKategorija });
    }

    /// <summary>
    /// Dohvaćanje view-a za uređivanje kategorije
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

        var kategorija = await _kategorijeRepository.GetAsync(id);
        if (kategorija == null)
        {
            return NotFound();
        }

        var editKategorijaRequest = new EditKategorijaRequest
        {
            IDKategorija = kategorija.IDKategorija,
            Naziv = kategorija.Naziv,
            Opis = kategorija.Opis
        };

        return View(editKategorijaRequest);

    }

    /// <summary>
    /// Uređivanje kategorije
    /// </summary>
    /// <param name="editKategorijaRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Kategorije>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Edit(EditKategorijaRequest editKategorijaRequest)
    {
        var kategorija = new Kategorije
        {
            IDKategorija = editKategorijaRequest.IDKategorija,
            Naziv = editKategorijaRequest.Naziv,
            Opis = editKategorijaRequest.Opis
        };

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _kategorijeRepository.UpdateAsync(kategorija);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(kategorija);
        }

        return RedirectToAction("Index", new { id = kategorija.IDKategorija });
    }

    /// <summary>
    /// Dohvaćanje view-a za brisanje kategorije
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

        var kategorija = await _kategorijeRepository.GetAsync(id);
        if (kategorija == null)
        {
            return NotFound();
        }

        return View(kategorija);
    }

    /// <summary>
    /// Brisanje kategorije
    /// </summary>
    /// <param name="editKategorijaRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Kategorije>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(EditKategorijaRequest editKategorijaRequest)
    {
        var kategorija = await _kategorijeRepository.DeleteAsync(editKategorijaRequest.IDKategorija);

        if (kategorija == null)
        {
            return NotFound();
        }

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok("Kategorija je uspješno uklonjena!");
        }

        return RedirectToAction("Index", new { id = editKategorijaRequest.IDKategorija });
    }
}
