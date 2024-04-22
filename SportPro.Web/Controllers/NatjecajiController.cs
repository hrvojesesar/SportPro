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
    public async Task<IActionResult> Delete(EditNatjecajRequest editNatjecajRequest)
    {
        await natjecajiRepository.DeleteAsync(editNatjecajRequest.IDNatjecaj);
        if (editNatjecajRequest != null)
        {
            return RedirectToAction("Index");
        }

        return NotFound();
        
    }
}
