using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.ViewModels;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
[ApiController]
[Authorize(Roles = "Uposlenik")]
public class KategorijeTroskovaController : Controller
{
    private readonly IKategorijeTroskovaRepository _kategorijeTroskovaRepository;

    public KategorijeTroskovaController(IKategorijeTroskovaRepository kategorijeTroskovaRepository)
    {
        _kategorijeTroskovaRepository = kategorijeTroskovaRepository;
    }

    /// <summary>
    /// Prikaz svih katgorija troskova
    /// </summary>
    /// <param name="searchQuery"></param>
    /// <param name="sortBy"></param>
    /// <param name="sortDirection"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<KategorijeTroskova>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Index(string? searchQuery, string? sortBy, string? sortDirection, int pageSize = 5, int pageNumber = 1)
    {
        var totalRecords = await _kategorijeTroskovaRepository.CountAsync();
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

        ViewBag.PageSize = pageSize;
        ViewBag.PageNumber = pageNumber;

        ViewBag.SearchQuery = searchQuery;

        ViewBag.SortBy = sortBy;
        ViewBag.SortDirection = sortDirection;

        var kategorijeTroskova = await _kategorijeTroskovaRepository.GetAllAsync(searchQuery, sortBy, sortDirection, pageNumber, pageSize);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(kategorijeTroskova);
        }

        return View(kategorijeTroskova);
    }

    /// <summary>
    /// Dohvaćanje view-a za dodavanje kategorije troska
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    /// <summary>
    /// Dodavanje kategorije troska
    /// </summary>
    /// <param name="addKategorijaTroskaRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<KategorijeTroskova>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add(AddKategorijaTroskaRequest addKategorijaTroskaRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var kategorijaTroska = new KategorijeTroskova
        {
            Naziv = addKategorijaTroskaRequest.Naziv,
            Opis = addKategorijaTroskaRequest.Opis
        };

        await _kategorijeTroskovaRepository.AddAsync(kategorijaTroska);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(kategorijaTroska);
        }

        return RedirectToAction("Index", new { id = addKategorijaTroskaRequest.IDKategorijaTroska });

    }

    /// <summary>
    /// Dohvaćanje view-a za uređivanje kategorije troska
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

        var kategorijaTroska = await _kategorijeTroskovaRepository.GetAsync(id);
        if (kategorijaTroska == null)
        {
            return NotFound();
        }

        var editKategorijaTroskaRequest = new EditKategorijaTroskaRequest
        {
            IDKategorijaTroska = kategorijaTroska.IDKategorijaTroska,
            Naziv = kategorijaTroska.Naziv,
            Opis = kategorijaTroska.Opis
        };

        return View(editKategorijaTroskaRequest);
    }

    /// <summary>
    /// Uređivanje kategorije troska
    /// </summary>
    /// <param name="editKategorijaTroskaRequest"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Edit(EditKategorijaTroskaRequest editKategorijaTroskaRequest)
    {
        var kategorijaTroska = new KategorijeTroskova
        {
            IDKategorijaTroska = editKategorijaTroskaRequest.IDKategorijaTroska,
            Naziv = editKategorijaTroskaRequest.Naziv,
            Opis = editKategorijaTroskaRequest.Opis
        };

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _kategorijeTroskovaRepository.UpdateAsync(kategorijaTroska);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(kategorijaTroska);
        }

        return RedirectToAction("Index", new { id = editKategorijaTroskaRequest.IDKategorijaTroska });
    }

    /// <summary>
    /// Dohvaćanje view-a za brisanje kategorije troska
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

        var kategorijaTroska = await _kategorijeTroskovaRepository.GetAsync(id);
        if (kategorijaTroska == null)
        {
            return NotFound();
        }

        return View(kategorijaTroska);
    }

    /// <summary>
    /// Brisanje kategorije troska
    /// </summary>
    /// <param name="editKategorijaTroskaRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<KategorijeTroskova>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(EditKategorijaTroskaRequest editKategorijaTroskaRequest)
    {
        var kategorijaTroska = await _kategorijeTroskovaRepository.DeleteAsync(editKategorijaTroskaRequest.IDKategorijaTroska);
        if (kategorijaTroska == null)
        {
            return NotFound();
        }

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok("Kategorija troška je uspješno uklonjena!");
        }

        return RedirectToAction("Index", new { id = editKategorijaTroskaRequest.IDKategorijaTroska });
    }
}
