using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;
using System.Collections.Immutable;

namespace SportPro.Web.Controllers;

public class ZaposleniciController : Controller
{
    private readonly IZaposleniciRepository zaposleniciRepository;
    private readonly IPoslovniceRepository poslovniceRepository;
    private readonly ApplicationDbContext _context;

    public ZaposleniciController(IZaposleniciRepository zaposleniciRepository, IPoslovniceRepository poslovniceRepository, ApplicationDbContext applicationDbContext)
    {
        this.zaposleniciRepository = zaposleniciRepository;
        this.poslovniceRepository = poslovniceRepository;
        _context = applicationDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var zaposlenici = await zaposleniciRepository.GetAllAsync();
        var poslovnice = await poslovniceRepository.GetAllAsync(); // Get the collection of Poslovnice

        ViewData["Poslovnice"] = poslovnice; // Pass the Poslovnice collection to the view

        return View(zaposlenici);
    }


    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var model = new AddZaposlenikRequest
        {
            Poslovnices = _context.Poslovnice.ToList()

        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddZaposlenikRequest addZaposlenikRequest)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var zaposlenik = new Zaposlenici
        {
            Ime = addZaposlenikRequest.Ime,
            Prezime = addZaposlenikRequest.Prezime,
            Spol = addZaposlenikRequest.Spol,
            Adresa = addZaposlenikRequest.Adresa,
            Grad = addZaposlenikRequest.Grad,
            Drzava = addZaposlenikRequest.Drzava,
            Telefon = addZaposlenikRequest.Telefon,
            Email = addZaposlenikRequest.Email,
            DatumZaposlenja = addZaposlenikRequest.DatumZaposlenja,
            Certifikati = addZaposlenikRequest.Certifikati,
            JMBG = addZaposlenikRequest.JMBG,
            BrojBankovnogRacuna = addZaposlenikRequest.BrojBankovnogRacuna,
            Kvalifikacija = addZaposlenikRequest.Kvalifikacija,
            DatumZavrsetkaRadnogOdnosa = addZaposlenikRequest.DatumZavrsetkaRadnogOdnosa,
            PoslovnicaID = addZaposlenikRequest.PoslovnicaID
        };



        await zaposleniciRepository.AddAsync(zaposlenik);
        return RedirectToAction("Index", new { id = zaposlenik.IDZaposlenik });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var zaposlenik = await zaposleniciRepository.GetAsync(id);

        if (zaposlenik == null)
        {
            return NotFound();
        }

        var model = new EditZaposlenikRequest
        {
            IDZaposlenik = zaposlenik.IDZaposlenik,
            Ime = zaposlenik.Ime,
            Prezime = zaposlenik.Prezime,
            Spol = zaposlenik.Spol,
            Adresa = zaposlenik.Adresa,
            Grad = zaposlenik.Grad,
            Drzava = zaposlenik.Drzava,
            Telefon = zaposlenik.Telefon,
            Email = zaposlenik.Email,
            DatumZaposlenja = zaposlenik.DatumZaposlenja,
            Certifikati = zaposlenik.Certifikati,
            JMBG = zaposlenik.JMBG,
            BrojBankovnogRacuna = zaposlenik.BrojBankovnogRacuna,
            Kvalifikacija = zaposlenik.Kvalifikacija,
            DatumZavrsetkaRadnogOdnosa = zaposlenik.DatumZavrsetkaRadnogOdnosa,
            PoslovnicaID = zaposlenik.PoslovnicaID,
            Poslovnices = _context.Poslovnice.ToList()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditZaposlenikRequest editZaposlenikRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var zaposlenik = new Zaposlenici
        {
            IDZaposlenik = editZaposlenikRequest.IDZaposlenik,
            Ime = editZaposlenikRequest.Ime,
            Prezime = editZaposlenikRequest.Prezime,
            Spol = editZaposlenikRequest.Spol,
            Adresa = editZaposlenikRequest.Adresa,
            Grad = editZaposlenikRequest.Grad,
            Drzava = editZaposlenikRequest.Drzava,
            Telefon = editZaposlenikRequest.Telefon,
            Email = editZaposlenikRequest.Email,
            DatumZaposlenja = editZaposlenikRequest.DatumZaposlenja,
            Certifikati = editZaposlenikRequest.Certifikati,
            JMBG = editZaposlenikRequest.JMBG,
            BrojBankovnogRacuna = editZaposlenikRequest.BrojBankovnogRacuna,
            Kvalifikacija = editZaposlenikRequest.Kvalifikacija,
            DatumZavrsetkaRadnogOdnosa = editZaposlenikRequest.DatumZavrsetkaRadnogOdnosa,
            PoslovnicaID = editZaposlenikRequest.PoslovnicaID
        };

        await zaposleniciRepository.UpdateAsync(zaposlenik);
        return RedirectToAction("Index", new { id = zaposlenik.IDZaposlenik });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var zaposlenik = await zaposleniciRepository.GetAsync(id);
        var poslovnice = await poslovniceRepository.GetAllAsync(); // Get the collection of Poslovnice

        ViewData["Poslovnice"] = poslovnice; // Pass the Poslovnice collection to the view

        if (zaposlenik == null)
        {
            return NotFound();
        }

        return View(zaposlenik);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(EditZaposlenikRequest editZaposlenikRequest)
    {
        var zaposlenik = await zaposleniciRepository.DeleteAsync(editZaposlenikRequest.IDZaposlenik);

        if (zaposlenik == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index", new { id = editZaposlenikRequest.IDZaposlenik });
    }




}
