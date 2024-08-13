using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.ViewModels;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
[Authorize(Roles = "Uposlenik")]
public class KategorijePrihodaController : Controller
{
    private readonly IKategorijePrihodaRepository kategorijePrihodaRepository;

    public KategorijePrihodaController(IKategorijePrihodaRepository kategorijePrihodaRepository)
    {
        this.kategorijePrihodaRepository = kategorijePrihodaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? searchQuery, string? sortBy, string? sortDirection, int pageSize = 5, int pageNumber = 1)
    {
        var totalRecords = await kategorijePrihodaRepository.CountAsync();
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

        var kategorijePrihoda = await kategorijePrihodaRepository.GetAllAsync(searchQuery, sortBy, sortDirection, pageNumber, pageSize);
        return View(kategorijePrihoda);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddKategorijaPrihodaRequest addKategorijaPrihodaRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var kategorijaPrihoda = new KategorijePrihoda
        {
            Naziv = addKategorijaPrihodaRequest.Naziv,
            Opis = addKategorijaPrihodaRequest.Opis
        };

        await kategorijePrihodaRepository.AddAsync(kategorijaPrihoda);
        return RedirectToAction("Index", new { id = addKategorijaPrihodaRequest.IDKategorijePrihoda });

    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var kategorijaPrihoda = await kategorijePrihodaRepository.GetAsync(id);
        if (kategorijaPrihoda == null)
        {
            return NotFound();
        }


        var editKategorijaPrihodaRequest = new EditKategorijaPrihodaRequest
        {
            IDKategorijePrihoda = kategorijaPrihoda.IDKategorijePrihoda,
            Naziv = kategorijaPrihoda.Naziv,
            Opis = kategorijaPrihoda.Opis
        };

        return View(editKategorijaPrihodaRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditKategorijaPrihodaRequest editKategorijaPrihodaRequest)
    {
        var kategorijaPrihoda = new KategorijePrihoda
        {
            IDKategorijePrihoda = editKategorijaPrihodaRequest.IDKategorijePrihoda,
            Naziv = editKategorijaPrihodaRequest.Naziv,
            Opis = editKategorijaPrihodaRequest.Opis
        };

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await kategorijePrihodaRepository.UpdateAsync(kategorijaPrihoda);
        return RedirectToAction("Index", new { id = editKategorijaPrihodaRequest.IDKategorijePrihoda });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var kategorijaPrihoda = await kategorijePrihodaRepository.GetAsync(id);
        if (kategorijaPrihoda == null)
        {
            return NotFound();
        }

        return View(kategorijaPrihoda);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(EditKategorijaPrihodaRequest editKategorijaPrihodaRequest)
    {
        var kategorijaPrihoda = await kategorijePrihodaRepository.DeleteAsync(editKategorijaPrihodaRequest.IDKategorijePrihoda);
        if (kategorijaPrihoda == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index", new { id = editKategorijaPrihodaRequest.IDKategorijePrihoda });
    }

}
