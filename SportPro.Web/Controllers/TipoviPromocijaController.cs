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

    [HttpGet]
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
        return View(tipoviPromocija);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    [HttpPost]
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
        return RedirectToAction("Index", new { id = tipPromocije.IDTipPromocije });
    }

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

    [HttpPost]
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
        return RedirectToAction("Index", new { id = tipPromocije.IDTipPromocije });
    }

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

    [HttpPost]
    public async Task<IActionResult> Delete(EditTipPromocijeRequest editTipPromocijeRequest)
    {
        var tipPromocije = await _tipoviPromocijaRepository.DeleteAsync(editTipPromocijeRequest.IDTipPromocije);

        if (tipPromocije == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index", new { id = editTipPromocijeRequest.IDTipPromocije });
    }
}
