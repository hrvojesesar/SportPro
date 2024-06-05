using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;
using SportPro.Web.Repositories;

namespace SportPro.Web.Controllers;
[Route("[controller]/[action]")]
[Authorize(Roles = "Menadzer")]
public class NatjecajiController : Controller
{
    private readonly INatjecajiRepository natjecajiRepository;
    private readonly IKandidatiRepository kandidatiRepository;

    public NatjecajiController(INatjecajiRepository natjecajiRepository, IKandidatiRepository kandidatiRepository)
    {
        this.natjecajiRepository = natjecajiRepository;
        this.kandidatiRepository = kandidatiRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int pageSize = 5, int pageNumber = 1)
    {
        var totalRecords = await natjecajiRepository.CountAsync();
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


        var natjecaji = await natjecajiRepository.GetAllAsync(pageNumber, pageSize);
        return View(natjecaji);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Add(AddNatjecajRequest addNatjecajRequest)
    {
        ValidateNatjecajForAdd(addNatjecajRequest);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var natjecaj = new Natjecaji
        {
            Naziv = addNatjecajRequest.Naziv,
            Opis = addNatjecajRequest.Opis,
            ProcijenjenaVrijednost = addNatjecajRequest.ProcijenjenaVrijednost,
            TrajanjeOd = addNatjecajRequest.TrajanjeOd,
            TrajanjeDo = addNatjecajRequest.TrajanjeDo,
            DatumObjave = addNatjecajRequest.DatumObjave,
            Aktivan = addNatjecajRequest.Aktivan,
            Dobitnik = addNatjecajRequest.Dobitnik
        };


        await natjecajiRepository.AddAsync(natjecaj);
        return RedirectToAction("Index", new { natjecaj.IDNatjecaj });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var natjecaj = await natjecajiRepository.GetAsync(id);
        var kandidati = await kandidatiRepository.GetByNatjecajAsync(id);

        if (natjecaj == null)
        {
            return NotFound();
        }

        var editNatjecajRequest = new EditNatjecajRequest
        {
            IDNatjecaj = natjecaj.IDNatjecaj,
            Naziv = natjecaj.Naziv,
            Opis = natjecaj.Opis,
            ProcijenjenaVrijednost = natjecaj.ProcijenjenaVrijednost,
            TrajanjeOd = natjecaj.TrajanjeOd,
            TrajanjeDo = natjecaj.TrajanjeDo,
            DatumObjave = natjecaj.DatumObjave,
            Aktivan = natjecaj.Aktivan,
            Dobitnik = natjecaj.Dobitnik
        };

        ViewData["Kandidati"] = new SelectList(kandidati, "IDKandidat", "ImePrezime");


        return View(editNatjecajRequest);
    }


    [HttpPost]
    public async Task<IActionResult> Edit(EditNatjecajRequest editNatjecajRequest)
    {
        var natjecaj = new Natjecaji
        {
            IDNatjecaj = editNatjecajRequest.IDNatjecaj,
            Naziv = editNatjecajRequest.Naziv,
            Opis = editNatjecajRequest.Opis,
            ProcijenjenaVrijednost = editNatjecajRequest.ProcijenjenaVrijednost,
            TrajanjeOd = editNatjecajRequest.TrajanjeOd,
            TrajanjeDo = editNatjecajRequest.TrajanjeDo,
            DatumObjave = editNatjecajRequest.DatumObjave,
            Aktivan = editNatjecajRequest.Aktivan,
            Dobitnik = editNatjecajRequest.Dobitnik
        };

        ValidateNatjecajForEdit(natjecaj);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await natjecajiRepository.UpdateAsync(natjecaj);
        return RedirectToAction("Index", new { id = editNatjecajRequest.IDNatjecaj });


    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var natjecaj = await natjecajiRepository.GetAsync(id);

        if (natjecaj == null)
        {
            return NotFound();
        }

        return View(natjecaj);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(EditNatjecajRequest editNatjecajRequest)
    {
        var natjecaj = await natjecajiRepository.DeleteAsync(editNatjecajRequest.IDNatjecaj);

        if (natjecaj == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index", new { id = editNatjecajRequest.IDNatjecaj });
    }

    private void ValidateNatjecajForAdd(AddNatjecajRequest addNatjecajRequest)
    {
        if (addNatjecajRequest.TrajanjeOd >= addNatjecajRequest.TrajanjeDo)
        {
            ModelState.AddModelError("TrajanjeOd", "Datum početka mora biti prije datuma završetka!");
        }
        if (addNatjecajRequest.DatumObjave != null && addNatjecajRequest.DatumObjave >= addNatjecajRequest.TrajanjeOd)
        {
            ModelState.AddModelError("DatumObjave", "Datum objave natječaja mora biti prije datuma početka!");
        }
        if (addNatjecajRequest.Naziv != null && addNatjecajRequest.Naziv.Length > 200)
        {
            ModelState.AddModelError("Naziv", "Naziv ne može biti duži od 200 znakova!");
        }
        if (addNatjecajRequest.Dobitnik != null && addNatjecajRequest.Dobitnik.Length > 50)
        {
            ModelState.AddModelError("Dobitnik", "Dobitnik ne može biti duži od 50 znakova!");
        }
    }

    private void ValidateNatjecajForEdit(Natjecaji natjecaj)
    {
        if (natjecaj.TrajanjeOd >= natjecaj.TrajanjeDo)
        {
            ModelState.AddModelError("TrajanjeOd", "Datum početka mora biti prije datuma završetka!");
        }
        if (natjecaj.DatumObjave != null && natjecaj.DatumObjave >= natjecaj.TrajanjeOd)
        {
            ModelState.AddModelError("DatumObjave", "Datum objave natječaja mora biti prije datuma početka!");
        }
        if (natjecaj.Naziv != null && natjecaj.Naziv.Length > 200)
        {
            ModelState.AddModelError("Naziv", "Naziv ne može biti duži od 200 znakova!");
        }
        if (natjecaj.Dobitnik != null && natjecaj.Dobitnik.Length > 50)
        {
            ModelState.AddModelError("Dobitnik", "Dobitnik ne može biti duži od 50 znakova!");
        }
    }
}