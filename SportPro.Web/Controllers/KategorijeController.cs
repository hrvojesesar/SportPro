using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
[Authorize(Roles = "Uposlenik")]
public class KategorijeController : Controller
{
    private readonly IKategorijeRepository _kategorijeRepository;

    public KategorijeController(IKategorijeRepository kategorijeRepository)
    {
        _kategorijeRepository = kategorijeRepository;
    }

    [HttpGet]
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

        ViewBag.SearchQuery = searchQuery;

        ViewBag.SortBy = sortBy;
        ViewBag.SortDirection = sortDirection;

        ViewBag.PageSize = pageSize;
        ViewBag.PageNumber = pageNumber;

        var kategorije = await _kategorijeRepository.GetAllAsync(searchQuery, sortBy, sortDirection, pageNumber, pageSize);
        return View(kategorije);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    [HttpPost]
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
        return RedirectToAction("Index", new { id = kategorija.IDKategorija });
    }

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

    [HttpPost]
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
        return RedirectToAction("Index", new { id = kategorija.IDKategorija });
    }

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

    [HttpPost]
    public async Task<IActionResult> Delete(EditKategorijaRequest editKategorijaRequest)
    {
        var kategorija = await _kategorijeRepository.DeleteAsync(editKategorijaRequest.IDKategorija);

        if (kategorija == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index", new { id = editKategorijaRequest.IDKategorija });
    }
}
