using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
[Authorize(Roles = "Uposlenik")]
public class TipoviPromocijaController : Controller
{
    private readonly ITipoviPromocijaRepository _tipoviPromocijaRepository;

    public TipoviPromocijaController(ITipoviPromocijaRepository tipoviPromocijaRepository)
    {
        _tipoviPromocijaRepository = tipoviPromocijaRepository;
    }

    /// <summary>
    /// Prikaz svih tipova promocija
    /// </summary>
    /// <param name="searchQuery"></param>
    /// <param name="sortBy"></param>
    /// <param name="sortDirection"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TipoviPromocija>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Index(string? searchQuery, string? sortBy, string? sortDirection, int pageSize = 5, int pageNumber = 1)
    {
        var totalRecords = await _tipoviPromocijaRepository.CountAsync();
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

        var tipoviPromocija = await _tipoviPromocijaRepository.GetAllAsync(searchQuery, sortBy, sortDirection, pageNumber, pageSize);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(tipoviPromocija);
        }

        return View(tipoviPromocija);
    }

    /// <summary>
    /// Dohvaćanje view-a za dodavanje tipa promocije
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    /// <summary>
    /// Dodavanje tipa promocije
    /// </summary>
    /// <param name="addTipPromocijeRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<TipoviPromocija>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add(AddTipPromocijeRequest addTipPromocijeRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var tipPromocije = new TipoviPromocija
        {
            Naziv = addTipPromocijeRequest.Naziv,
            Opis = addTipPromocijeRequest.Opis
        };

        await _tipoviPromocijaRepository.AddAsync(tipPromocije);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(tipPromocije);
        }

        return RedirectToAction("Index", new { id = tipPromocije.IDTipPromocije });
    }

    /// <summary>
    /// Dohvaćanje view-a za uređivanje tipa promocije
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

        var tipPromocije = await _tipoviPromocijaRepository.GetAsync(id);
        if (tipPromocije == null)
        {
            return NotFound();
        }

        var editTipPromocijeRequest = new EditTipPromocijeRequest
        {
            IDTipPromocije = tipPromocije.IDTipPromocije,
            Naziv = tipPromocije.Naziv,
            Opis = tipPromocije.Opis
        };

        return View(editTipPromocijeRequest);
    }

    /// <summary>
    /// Uređivanje tipa promocije
    /// </summary>
    /// <param name="editTipPromocijeRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<TipoviPromocija>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Edit(EditTipPromocijeRequest editTipPromocijeRequest)
    {
        var tipPromocije = new TipoviPromocija
        {
            IDTipPromocije = editTipPromocijeRequest.IDTipPromocije,
            Naziv = editTipPromocijeRequest.Naziv,
            Opis = editTipPromocijeRequest.Opis
        };

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _tipoviPromocijaRepository.UpdateAsync(tipPromocije);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(tipPromocije);
        }

        return RedirectToAction("Index", new { id = tipPromocije.IDTipPromocije });
    }

    /// <summary>
    /// Dohvaćanje view-a za brisanje tipa promocije
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

        var tipPromocije = await _tipoviPromocijaRepository.GetAsync(id);
        if (tipPromocije == null)
        {
            return NotFound();
        }

        return View(tipPromocije);
    }

    /// <summary>
    /// Brisanje tipa promocije
    /// </summary>
    /// <param name="editTipPromocijeRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<TipoviPromocija>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(EditTipPromocijeRequest editTipPromocijeRequest)
    {
        var tipPromocije = await _tipoviPromocijaRepository.DeleteAsync(editTipPromocijeRequest.IDTipPromocije);

        if (tipPromocije == null)
        {
            return NotFound();
        }

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok("Tip promocije je uspješno uklonjen!");
        }

        return RedirectToAction("Index", new { id = editTipPromocijeRequest.IDTipPromocije });
    }
}
