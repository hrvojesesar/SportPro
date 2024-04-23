using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

public class ZaposleniciController : Controller
{
    private readonly IZaposleniciRepository _zaposleniciRepository;

    public ZaposleniciController(IZaposleniciRepository zaposleniciRepository)
    {
        _zaposleniciRepository = zaposleniciRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var zaposlenici = await _zaposleniciRepository.GetAllAsync();
        return View(zaposlenici);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddZaposlenikRequest addZaposlenikRequest)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var zaposlenik = new Zaposlenici
        {
            Ime = addZaposlenikRequest.Ime,
            Prezime = addZaposlenikRequest.Prezime,
            Spol = addZaposlenikRequest.Spol,
            Adresa = addZaposlenikRequest.Adresa,
            Telefon = addZaposlenikRequest.Telefon,
            Email = addZaposlenikRequest.Email,
            DatumZaposlenja = addZaposlenikRequest.DatumZaposlenja,
            Pozicija = addZaposlenikRequest.Pozicija,
            Cerfitikati = addZaposlenikRequest.Cerfitikati,
            Placa = addZaposlenikRequest.Placa,
            Status = addZaposlenikRequest.Status
        };

        await _zaposleniciRepository.AddAsync(zaposlenik);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var zaposlenik = await _zaposleniciRepository.GetAsync(id);
        if (zaposlenik == null)
        {
            return NotFound();
        }

        var editZaposlenikRequest = new EditZaposlenikRequest
        {
            IDZaposlenik = zaposlenik.IDZaposlenik,
            Ime = zaposlenik.Ime,
            Prezime = zaposlenik.Prezime,
            Spol = zaposlenik.Spol,
            Adresa = zaposlenik.Adresa,
            Telefon = zaposlenik.Telefon,
            Email = zaposlenik.Email,
            DatumZaposlenja = zaposlenik.DatumZaposlenja,
            Pozicija = zaposlenik.Pozicija,
            Cerfitikati = zaposlenik.Cerfitikati,
            Placa = zaposlenik.Placa,
            Status = zaposlenik.Status
        };

        return View(editZaposlenikRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditZaposlenikRequest editZaposlenikRequest)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var zaposlenik = new Zaposlenici
        {
            IDZaposlenik = editZaposlenikRequest.IDZaposlenik,
            Ime = editZaposlenikRequest.Ime,
            Prezime = editZaposlenikRequest.Prezime,
            Spol = editZaposlenikRequest.Spol,
            Adresa = editZaposlenikRequest.Adresa,
            Telefon = editZaposlenikRequest.Telefon,
            Email = editZaposlenikRequest.Email,
            DatumZaposlenja = editZaposlenikRequest.DatumZaposlenja,
            Pozicija = editZaposlenikRequest.Pozicija,
            Cerfitikati = editZaposlenikRequest.Cerfitikati,
            Placa = editZaposlenikRequest.Placa,
            Status = editZaposlenikRequest.Status
        };

        await _zaposleniciRepository.UpdateAsync(zaposlenik);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var zaposlenik = await _zaposleniciRepository.GetAsync(id);
        if (zaposlenik == null)
        {
            return NotFound();
        }

        return View(zaposlenik);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var zaposlenik = await _zaposleniciRepository.DeleteAsync(id);
        if (zaposlenik == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index");
    }


}
