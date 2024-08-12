using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
[Authorize(Roles = "Uposlenik")]
public class TroskoviController : Controller
{
    private readonly ITroskoviRepository _troskoviRepository;
    private readonly IKategorijeTroskovaRepository _kategorijeTroskovaRepository;
    private readonly ApplicationDbContext applicationDbContext;

    public TroskoviController(ITroskoviRepository troskoviRepository, IKategorijeTroskovaRepository kategorijeTroskovaRepository, ApplicationDbContext applicationDbContext)
    {
        _troskoviRepository = troskoviRepository;
        _kategorijeTroskovaRepository = kategorijeTroskovaRepository;
        this.applicationDbContext = applicationDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? naziv, string? kategorijaTroska, int? minValue, int? maxValue, string? sortBy, string? sortDirection, int pageSize = 5, int pageNumber = 1)
    {
        var totalRecords = await _troskoviRepository.CountAsync();
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

        ViewBag.Naziv = naziv;
        ViewBag.KategorijaTroska = kategorijaTroska;
        ViewBag.MinValue = minValue ?? 0;
        ViewBag.MaxValue = maxValue ?? 10000;

        ViewBag.SortBy = sortBy;
        ViewBag.SortDirection = sortDirection;

        ViewBag.PageSize = pageSize;
        ViewBag.PageNumber = pageNumber;

        var kategorijeTroskova = await _kategorijeTroskovaRepository.GetAllSecAsync();

        var kategorijeTroskovaList = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "Svi" }
        };

        kategorijeTroskovaList.AddRange(kategorijeTroskova.Select(x => new SelectListItem { Value = x.IDKategorijaTroska.ToString(), Text = x.Naziv }));

        ViewBag.KategorijeTroskovaList = new SelectList(kategorijeTroskovaList, "Value", "Text", kategorijaTroska);

        var troskovi = await _troskoviRepository.GetAllAsync(naziv, kategorijaTroska, minValue, maxValue, sortBy, sortDirection, pageNumber, pageSize);

        ViewData["KategorijeTroskova"] = kategorijeTroskova;

        return View(troskovi);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var model = new AddTrosakRequest
        {
            KategorijeTroskovas = await _kategorijeTroskovaRepository.GetAllSecAsync()
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddTrosakRequest addTrosakRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var trosak = new Troskovi
        {
            Naziv = addTrosakRequest.Naziv,
            Opis = addTrosakRequest.Opis,
            Datum = addTrosakRequest.Datum,
            Iznos = addTrosakRequest.Iznos,
            Mjesec = addTrosakRequest.Mjesec,
            Godina = addTrosakRequest.Godina,
            KategorijeTroskovaIDKategorijaTroska = addTrosakRequest.KategorijeTroskovaIDKategorijaTroska
        };

        await _troskoviRepository.AddAsync(trosak);
        return RedirectToAction("Index", new { id = trosak.IDTrosak });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var trosak = await _troskoviRepository.GetAsync(id);

        if (trosak == null)
        {
            return NotFound();
        }

        var model = new EditTrosakRequest
        {
            IDTrosak = trosak.IDTrosak,
            Naziv = trosak.Naziv,
            Opis = trosak.Opis,
            Datum = trosak.Datum,
            Iznos = trosak.Iznos,
            Mjesec = trosak.Mjesec,
            Godina = trosak.Godina,
            KategorijeTroskovaIDKategorijaTroska = trosak.KategorijeTroskovaIDKategorijaTroska,
            KategorijeTroskovas = applicationDbContext.KategorijeTroskova.ToList()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditTrosakRequest editTrosakRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var trosak = new Troskovi
        {
            IDTrosak = editTrosakRequest.IDTrosak,
            Naziv = editTrosakRequest.Naziv,
            Opis = editTrosakRequest.Opis,
            Datum = editTrosakRequest.Datum,
            Iznos = editTrosakRequest.Iznos,
            Mjesec = editTrosakRequest.Mjesec,
            Godina = editTrosakRequest.Godina,
            KategorijeTroskovaIDKategorijaTroska = editTrosakRequest.KategorijeTroskovaIDKategorijaTroska
        };

        await _troskoviRepository.UpdateAsync(trosak);
        return RedirectToAction("Index", new { id = trosak.IDTrosak });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var trosak = await _troskoviRepository.GetAsync(id);

        if (trosak == null)
        {
            return NotFound();
        }

        var kategorijaTroska = await _kategorijeTroskovaRepository.GetAsync(trosak.KategorijeTroskovaIDKategorijaTroska);
        trosak.KategorijaTroskaNaziv = kategorijaTroska?.Naziv;

        return View(trosak);
    }


    [HttpPost]
    public async Task<IActionResult> Delete(EditTrosakRequest editTrosakRequest)
    {
        var trosak = await _troskoviRepository.DeleteAsync(editTrosakRequest.IDTrosak);

        if (trosak == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index", new { id = editTrosakRequest.IDTrosak });

    }

}


