using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.ViewModels;
using SportPro.Web.Models.Domains;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
[Authorize(Roles = "Uposlenik")]
public class PrihodiController : Controller
{
    private readonly IPrihodiRepository _prihodiRepository;
    private readonly IKategorijePrihodaRepository kategorijePrihodaRepository;
    private readonly ApplicationDbContext applicationDbContext;

    public PrihodiController(IPrihodiRepository prihodiRepository, IKategorijePrihodaRepository kategorijePrihodaRepository, ApplicationDbContext applicationDbContext)
    {
        _prihodiRepository = prihodiRepository;
        this.kategorijePrihodaRepository = kategorijePrihodaRepository;
        this.applicationDbContext = applicationDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? naziv, string? kategorijaPrihoda, int? minValue, int? maxValue, string? sortBy, string? sortDirection, int pageSize = 5, int pageNumber = 1)
    {
        var totalRecords = await _prihodiRepository.CountAsync();
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

        ViewBag.Naziv = naziv;
        ViewBag.KategorijaPrihoda = kategorijaPrihoda;
        ViewBag.MinValue = minValue ?? 0;
        ViewBag.MaxValue = maxValue ?? 10000;

        ViewBag.SortBy = sortBy;
        ViewBag.SortDirection = sortDirection;

        var kategorijePrihoda = await kategorijePrihodaRepository.GetAllSecAsync();

        var kategorijePrihodaList = new List<SelectListItem>
        {
          new SelectListItem { Value = "", Text = "Svi" }
        };

        kategorijePrihodaList.AddRange(kategorijePrihoda.Select(kategorija => new SelectListItem
        {
            Value = kategorija.IDKategorijePrihoda.ToString(),
            Text = kategorija.Naziv
        }));

        ViewBag.KategorijePrihodaList = new SelectList(kategorijePrihodaList, "Value", "Text", kategorijaPrihoda);

        var prihodi = await _prihodiRepository.GetAllAsync(naziv, kategorijaPrihoda, minValue, maxValue, sortBy, sortDirection, pageNumber, pageSize);

        ViewData["KategorijePrihoda"] = kategorijePrihoda;

        return View(prihodi);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var model = new AddPrihodRequest
        {
            KategorijePrihodas = await kategorijePrihodaRepository.GetAllSecAsync()
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddPrihodRequest addPrihodRequest)
    {
        ValidatePrihodForAdd(addPrihodRequest);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var prihod = new Prihodi
        {
            Naziv = addPrihodRequest.Naziv,
            Opis = addPrihodRequest.Opis,
            Datum = addPrihodRequest.Datum,
            Iznos = addPrihodRequest.Iznos,
            Mjesec = addPrihodRequest.Mjesec,
            Godina = addPrihodRequest.Godina,
            KategorijePrihodaIDKategorijePrihoda = addPrihodRequest.KategorijePrihodaIDKategorijePrihoda
        };

        await _prihodiRepository.AddAsync(prihod);
        return RedirectToAction("Index", new { id = prihod.IDPrihod });

    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var prihod = await _prihodiRepository.GetAsync(id);

        if (prihod == null)
        {
            return NotFound();
        }

        var model = new EditPrihodRequest
        {
            IDPrihod = prihod.IDPrihod,
            Naziv = prihod.Naziv,
            Opis = prihod.Opis,
            Datum = prihod.Datum,
            Iznos = prihod.Iznos,
            Mjesec = prihod.Mjesec,
            Godina = prihod.Godina,
            KategorijePrihodaIDKategorijePrihoda = prihod.KategorijePrihodaIDKategorijePrihoda,
            KategorijePrihodas = applicationDbContext.KategorijePrihoda.ToList()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditPrihodRequest editPrihodRequest)
    {
        var prihod = new Prihodi
        {
            IDPrihod = editPrihodRequest.IDPrihod,
            Naziv = editPrihodRequest.Naziv,
            Opis = editPrihodRequest.Opis,
            Datum = editPrihodRequest.Datum,
            Iznos = editPrihodRequest.Iznos,
            Mjesec = editPrihodRequest.Mjesec,
            Godina = editPrihodRequest.Godina,
            KategorijePrihodaIDKategorijePrihoda = editPrihodRequest.KategorijePrihodaIDKategorijePrihoda
        };

        ValidatePrihodForEdit(prihod);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedPrihod = await _prihodiRepository.UpdateAsync(prihod);

        if (updatedPrihod == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index", new { id = updatedPrihod.IDPrihod });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var prihod = await _prihodiRepository.GetAsync(id);

        if (prihod == null)
        {
            return NotFound();
        }

        var kategorijaPrihoda = await kategorijePrihodaRepository.GetAsync(prihod.KategorijePrihodaIDKategorijePrihoda);
        prihod.KategorijaPrihodaNaziv = kategorijaPrihoda?.Naziv;

        return View(prihod);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(EditPrihodRequest editPrihodRequest)
    {
        var prihod = await _prihodiRepository.DeleteAsync(editPrihodRequest.IDPrihod);
        if (prihod == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index", new { id = editPrihodRequest.IDPrihod });
    }

    private void ValidatePrihodForAdd(AddPrihodRequest addPrihodRequest)
    {
        if (addPrihodRequest.Iznos < 0)
        {
            ModelState.AddModelError("Iznos", "Iznos ne može biti manji od 0.");
        }

        if (addPrihodRequest.Mjesec != null)
        {
            if (addPrihodRequest.Mjesec < 1 || addPrihodRequest.Mjesec > 12)
            {
                ModelState.AddModelError("Mjesec", "Mjesec mora biti između 1 i 12.");
            }
        }
    }

    private void ValidatePrihodForEdit(Prihodi prihodi)
    {
        if (prihodi.Iznos < 0)
        {
            ModelState.AddModelError("Iznos", "Iznos ne može biti manji od 0.");
        }

        if (prihodi.Mjesec != null)
        {
            if (prihodi.Mjesec < 1 || prihodi.Mjesec > 12)
            {
                ModelState.AddModelError("Mjesec", "Mjesec mora biti između 1 i 12.");
            }
        }
    }
}