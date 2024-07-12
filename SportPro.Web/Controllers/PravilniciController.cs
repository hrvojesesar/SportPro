using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
[Authorize(Roles = "Uposlenik")]
public class PravilniciController : Controller
{
    private readonly IPravilniciRepository _pravilniciRepository;

    public PravilniciController(IPravilniciRepository pravilniciRepository)
    {
        _pravilniciRepository = pravilniciRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? searchQuery, string? searchQuery2, DateTime? startDate, DateTime? endDate, string? sortBy, string? sortDirection, int pageSize = 5, int pageNumber = 1)
    {
        var totalRecords = await _pravilniciRepository.CountAsync();
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
        ViewBag.SearchQuery2 = searchQuery2;
        ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
        ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

        var selectList = new SelectList(new[]
        {
        new { Value = "", Text = "Svi" },
        new { Value = "1", Text = "Aktivni" },
        new { Value = "0", Text = "Neaktivni" },
        }, "Value", "Text", searchQuery2);

        ViewBag.AktivanList = selectList;

        ViewBag.SortBy = sortBy;
        ViewBag.SortDirection = sortDirection;

        ViewBag.PageSize = pageSize;
        ViewBag.PageNumber = pageNumber;

        var pravilnici = await _pravilniciRepository.GetAllAsync(searchQuery, searchQuery2, startDate, endDate, sortBy, sortDirection, pageNumber, pageSize);
        return View(pravilnici);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddPravilnikRequest addPravilnikRequest)
    {
        ValidatePravilnikForAdd(addPravilnikRequest);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var pravilnik = new Pravilnici
        {
            Naziv = addPravilnikRequest.Naziv,
            Opis = addPravilnikRequest.Opis,
            DatumObjavljivanja = addPravilnikRequest.DatumObjavljivanja,
            Aktivan = addPravilnikRequest.Aktivan
        };

        await _pravilniciRepository.AddAsync(pravilnik);
        return RedirectToAction("Index", new { id = pravilnik.IDPravilnik });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return BadRequest();
        }

        var pravilnik = await _pravilniciRepository.GetAsync(id);
        if (pravilnik == null)
        {
            return NotFound();
        }

        var editPravilnikRequest = new EditPravilnikRequest
        {
            IDPravilnik = pravilnik.IDPravilnik,
            Naziv = pravilnik.Naziv,
            Opis = pravilnik.Opis,
            DatumObjavljivanja = pravilnik.DatumObjavljivanja,
            Aktivan = pravilnik.Aktivan
        };

        return View(editPravilnikRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditPravilnikRequest editPravilnikRequest)
    {

        var pravilnik = new Pravilnici
        {
            IDPravilnik = editPravilnikRequest.IDPravilnik,
            Naziv = editPravilnikRequest.Naziv,
            Opis = editPravilnikRequest.Opis,
            DatumObjavljivanja = editPravilnikRequest.DatumObjavljivanja,
            Aktivan = editPravilnikRequest.Aktivan
        };

        ValidatePravilnikForEdit(editPravilnikRequest);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _pravilniciRepository.UpdateAsync(pravilnik);
        return RedirectToAction("Index", new { id = pravilnik.IDPravilnik });
    }



    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return BadRequest();
        }

        var pravilnik = await _pravilniciRepository.GetAsync(id);
        if (pravilnik == null)
        {
            return NotFound();
        }

        return View(pravilnik);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(EditPravilnikRequest editPravilnikRequest)
    {
        var pravilnik = await _pravilniciRepository.DeleteAsync(editPravilnikRequest.IDPravilnik);
        if (pravilnik == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index", new { id = editPravilnikRequest.IDPravilnik });
    }

    private void ValidatePravilnikForAdd(AddPravilnikRequest addPravilnikRequest)
    {
        if (addPravilnikRequest.Naziv.Length > 100)
        {
            ModelState.AddModelError("Naziv", "Naziv ne smije biti duži od 100 karaktera!");
        }
    }

    private void ValidatePravilnikForEdit(EditPravilnikRequest editPravilnikRequest)
    {
        if (editPravilnikRequest.Naziv != null && editPravilnikRequest.Naziv.Length > 100)
        {
            ModelState.AddModelError("Naziv", "Naziv ne smije biti duži od 100 karaktera!");
        }
    }
}
