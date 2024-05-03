using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
public class ZaposleniciController : Controller
{
    private readonly IZaposleniciRepository zaposleniciRepository;
    private readonly IPoslovniceRepository poslovniceRepository;
    private readonly IPozicijeRepository pozicijeRepository;
    private readonly ApplicationDbContext _context;

    public ZaposleniciController(IZaposleniciRepository zaposleniciRepository, IPoslovniceRepository poslovniceRepository, ApplicationDbContext applicationDbContext, IPozicijeRepository pozicijeRepository)
    {
        this.zaposleniciRepository = zaposleniciRepository;
        this.poslovniceRepository = poslovniceRepository;
        _context = applicationDbContext;
        this.pozicijeRepository = pozicijeRepository;
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
        var pozicije = await pozicijeRepository.GetAllAsync();
        var model = new AddZaposlenikRequest
        {
            Poslovnices = _context.Poslovnice.ToList(),
            Pozicije = pozicije.Select(p => new SelectListItem
            {
                Text = p.Naziv,
                Value = p.IDPozicija.ToString()
            })
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddZaposlenikRequest addZaposlenikRequest)
    {
        //ValidateZaposlenikForAdd(addZaposlenikRequest);

        //if (!ModelState.IsValid)
        //{
        //    return BadRequest(ModelState);
        //}

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
            Status = addZaposlenikRequest.Status,
            DatumZavrsetkaRadnogOdnosa = addZaposlenikRequest.DatumZavrsetkaRadnogOdnosa,
            PoslovnicaID = addZaposlenikRequest.PoslovnicaID

        };

        var selectedPozicije = new List<Pozicije>();
        foreach (var pozicijaId in addZaposlenikRequest.SelectedPozicije)
        {
            var selectedPozicijaId = int.Parse(pozicijaId);
            var existing = await pozicijeRepository.GetAsync(selectedPozicijaId);

            if (existing != null)
            {
                selectedPozicije.Add(existing);
            }
        }

        zaposlenik.Pozicije = selectedPozicije;


        await zaposleniciRepository.AddAsync(zaposlenik);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var zaposlenik = await zaposleniciRepository.GetAsync(id);
        var pozicije = await pozicijeRepository.GetAllAsync();

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
            Status = zaposlenik.Status,
            DatumZavrsetkaRadnogOdnosa = zaposlenik.DatumZavrsetkaRadnogOdnosa,
            PoslovnicaID = zaposlenik.PoslovnicaID,
            Poslovnices = _context.Poslovnice.ToList(),
            Pozicije = pozicije.Select(p => new SelectListItem
            {
                Value = p.IDPozicija.ToString(),
                Text = p.Naziv
            }),
            SelectedPozicije = zaposlenik.Pozicije.Select(p => p.IDPozicija.ToString()).ToArray()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditZaposlenikRequest editZaposlenikRequest)
    {

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
            Status = editZaposlenikRequest.Status,
            DatumZavrsetkaRadnogOdnosa = editZaposlenikRequest.DatumZavrsetkaRadnogOdnosa,
            PoslovnicaID = editZaposlenikRequest.PoslovnicaID
        };

        var selectedPozicije = new List<Pozicije>();
        foreach (var pozicijaId in editZaposlenikRequest.SelectedPozicije)
        {
            var existing = await pozicijeRepository.GetAsync(int.Parse(pozicijaId));

            if (existing != null)
            {
                selectedPozicije.Add(existing);
            }
        }

        zaposlenik.Pozicije = selectedPozicije;

        ValidateZaposlenikForEdit(zaposlenik);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

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

    private void ValidateZaposlenikForAdd(AddZaposlenikRequest addZaposlenikRequest)
    {
        if (addZaposlenikRequest.Ime.Length > 20)
        {
            ModelState.AddModelError("Ime", "Ime ne smije biti duže od 20 znakova!");
        }
        if (addZaposlenikRequest.Prezime.Length > 20)
        {
            ModelState.AddModelError("Prezime", "Prezime ne smije biti duže od 20 znakova!");
        }
        if (addZaposlenikRequest.Adresa.Length > 50)
        {
            ModelState.AddModelError("Adresa", "Adresa ne smije biti duža od 50 znakova!");
        }
        if (addZaposlenikRequest.Grad.Length > 50)
        {
            ModelState.AddModelError("Grad", "Grad ne smije biti duži od 50 znakova!");
        }
        if (addZaposlenikRequest.Drzava.Length > 50)
        {
            ModelState.AddModelError("Drzava", "Država ne smije biti duža od 50 znakova!");
        }
        if (addZaposlenikRequest.Telefon.Length > 20)
        {
            ModelState.AddModelError("Telefon", "Telefon ne smije biti duži od 20 znakova!");
        }
        if (!Regex.IsMatch(addZaposlenikRequest.Telefon, @"^[0-9+]*$"))
        {
            ModelState.AddModelError("Telefon", "Telefon podržava samo brojeve i znak +!");
        }
        if (addZaposlenikRequest.Email != null && addZaposlenikRequest.Email.Length > 50)
        {
            ModelState.AddModelError("Email", "Email ne smije biti duži od 50 znakova!");
        }
        if (addZaposlenikRequest.JMBG.Length > 20)
        {
            ModelState.AddModelError("JMBG", "JMBG ne smije biti duži od 20 znakova!");
        }
        if (!Regex.IsMatch(addZaposlenikRequest.JMBG, @"^[0-9]*$"))
        {
            ModelState.AddModelError("JMBG", "JMBG podržava samo brojeve!");
        }
        if (addZaposlenikRequest.BrojBankovnogRacuna.Length > 20)
        {
            ModelState.AddModelError("BrojBankovnogRacuna", "Broj bankovnog računa ne smije biti duži od 20 znakova!");
        }
        if (addZaposlenikRequest.Kvalifikacija.Length > 50)
        {
            ModelState.AddModelError("Kvalifikacija", "Kvalifikacija ne smije biti duža od 50 znakova!");
        }
        if (addZaposlenikRequest.Status.Length > 10)
        {
            ModelState.AddModelError("Status", "Status ne smije biti duži od 10 znakova!");
        }
        if (addZaposlenikRequest.DatumZavrsetkaRadnogOdnosa != null && addZaposlenikRequest.DatumZavrsetkaRadnogOdnosa < addZaposlenikRequest.DatumZaposlenja)
        {
            ModelState.AddModelError("DatumZavrsetkaRadnogOdnosa", "Datum završetka radnog odnosa mora biti poslije datuma zaposlenja!");
        }
    }

    private void ValidateZaposlenikForEdit(Zaposlenici zaposlenik)
    {
        if (zaposlenik.Ime != null && zaposlenik.Ime.Length > 20)
        {
            ModelState.AddModelError("Ime", "Ime ne smije biti duže od 20 znakova!");
        }
        if (zaposlenik.Prezime != null && zaposlenik.Prezime.Length > 20)
        {
            ModelState.AddModelError("Prezime", "Prezime ne smije biti duže od 20 znakova!");
        }
        if (zaposlenik.Adresa != null && zaposlenik.Adresa.Length > 50)
        {
            ModelState.AddModelError("Adresa", "Adresa ne smije biti duža od 50 znakova!");
        }
        if (zaposlenik.Grad != null && zaposlenik.Grad.Length > 50)
        {
            ModelState.AddModelError("Grad", "Grad ne smije biti duži od 50 znakova!");
        }
        if (zaposlenik.Drzava != null && zaposlenik.Drzava.Length > 50)
        {
            ModelState.AddModelError("Drzava", "Država ne smije biti duža od 50 znakova!");
        }
        if (zaposlenik.Telefon != null && zaposlenik.Telefon.Length > 20)
        {
            ModelState.AddModelError("Telefon", "Telefon ne smije biti duži od 20 znakova!");
        }
        if (!Regex.IsMatch(zaposlenik.Telefon, @"^[0-9+]*$"))
        {
            ModelState.AddModelError("Telefon", "Telefon podržava samo brojeve!");
        }
        if (zaposlenik.Email != null && zaposlenik.Email.Length > 50)
        {
            ModelState.AddModelError("Email", "Email ne smije biti duži od 50 znakova!");
        }
        if (zaposlenik.JMBG != null && zaposlenik.JMBG.Length > 20)
        {
            ModelState.AddModelError("JMBG", "JMBG ne smije biti duži od 20 znakova!");
        }
        if (zaposlenik.JMBG != null && !Regex.IsMatch(zaposlenik.JMBG, @"^[0-9]*$"))
        {
            ModelState.AddModelError("JMBG", "JMBG podržava samo brojeve!");
        }
        if (zaposlenik.BrojBankovnogRacuna != null && zaposlenik.BrojBankovnogRacuna.Length > 20)
        {
            ModelState.AddModelError("BrojBankovnogRacuna", "Broj bankovnog računa ne smije biti duži od 20 znakova!");
        }
        if (zaposlenik.Kvalifikacija != null && zaposlenik.Kvalifikacija.Length > 50)
        {
            ModelState.AddModelError("Kvalifikacija", "Kvalifikacija ne smije biti duža od 50 znakova!");
        }
        if (zaposlenik.Status != null && zaposlenik.Status.Length > 10)
        {
            ModelState.AddModelError("Status", "Status ne smije biti duži od 10 znakova!");
        }
        if (zaposlenik.DatumZavrsetkaRadnogOdnosa != null && zaposlenik.DatumZaposlenja != null && zaposlenik.DatumZavrsetkaRadnogOdnosa < zaposlenik.DatumZaposlenja)
        {
            ModelState.AddModelError("DatumZavrsetkaRadnogOdnosa", "Datum završetka radnog odnosa mora biti poslije datuma zaposlenja!");
        }
    }



}
