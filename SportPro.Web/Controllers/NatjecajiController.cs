using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

public class NatjecajiController : Controller
{
    private readonly INatjecajiRepository natjecajiRepository;

    public NatjecajiController(INatjecajiRepository natjecajiRepository)
    {
        this.natjecajiRepository = natjecajiRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var natjecaji = await natjecajiRepository.GetAllAsync();
        return View(natjecaji);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddNatjecajRequest addNatjecajRequest)
    {
        ValidateNatjecajForAdd(addNatjecajRequest);

        if (!ModelState.IsValid)
        {
            return View();
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
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
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
            return View();
        }

        await natjecajiRepository.UpdateAsync(natjecaj);
        return RedirectToAction("Index");


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

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var natjecaj = await natjecajiRepository.DeleteAsync(id);

        if (natjecaj == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index");
    }
  

    private void ValidateNatjecajForAdd(AddNatjecajRequest addNatjecajRequest)
    {
        if (addNatjecajRequest.TrajanjeOd >= addNatjecajRequest.TrajanjeDo)
        {
            ModelState.AddModelError("TrajanjeOd", "TrajanjeOd has to be before TrajanjeDo!");
        }
        if (addNatjecajRequest.DatumObjave >= addNatjecajRequest.TrajanjeOd)
        {
            ModelState.AddModelError("DatumObjave", "DatumObjave has to be before TrajanjeOd!");
        }
    }

    private void ValidateNatjecajForEdit(Natjecaji natjecaj)
    {
        if (natjecaj.TrajanjeOd >= natjecaj.TrajanjeDo)
        {
            ModelState.AddModelError("TrajanjeOd", "TrajanjeOd has to be before TrajanjeDo!");
        }
        if (natjecaj.DatumObjave >= natjecaj.TrajanjeOd)
        {
            ModelState.AddModelError("DatumObjave", "DatumObjave has to be before TrajanjeOd!");
        }
    }
}
