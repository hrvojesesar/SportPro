using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.ViewModels;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
[Authorize(Roles = "Uposlenik")]
public class KategorijeTroskovaController : Controller
{
    private readonly IKategorijeTroskovaRepository _kategorijeTroskovaRepository;

    public KategorijeTroskovaController(IKategorijeTroskovaRepository kategorijeTroskovaRepository)
    {
        _kategorijeTroskovaRepository = kategorijeTroskovaRepository;
    }

    [HttpGet]
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
        return View(kategorijeTroskova);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    [HttpPost]
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
        return RedirectToAction("Index", new { id = addKategorijaTroskaRequest.IDKategorijaTroska });

    }

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
        return RedirectToAction("Index", new { id = editKategorijaTroskaRequest.IDKategorijaTroska });
    }

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

    [HttpPost]
    public async Task<IActionResult> Delete(EditKategorijaTroskaRequest editKategorijaTroskaRequest)
    {
        var kategorijaTroska = await _kategorijeTroskovaRepository.DeleteAsync(editKategorijaTroskaRequest.IDKategorijaTroska);
        if (kategorijaTroska == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index", new { id = editKategorijaTroskaRequest.IDKategorijaTroska });
    }
}
