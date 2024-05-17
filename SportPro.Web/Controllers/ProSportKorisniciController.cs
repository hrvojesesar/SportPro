using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;
using System.Security.Cryptography;
using System.Text;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
public class ProSportKorisniciController : Controller
{
    private readonly IProSportKorisniciRepository _proSportKorisniciRepository;

    public ProSportKorisniciController(IProSportKorisniciRepository proSportKorisniciRepository)
    {
        _proSportKorisniciRepository = proSportKorisniciRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var proSportKorisnici = await _proSportKorisniciRepository.GetAllAsync();
        return View(proSportKorisnici);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddProSportKorisnikRequest addProSportKorisnikRequest)
    {
        ValidateProSportKorisnikForAdd(addProSportKorisnikRequest);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        //haširanje lozinke
        var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(addProSportKorisnikRequest.Lozinka);
        var hash = sha256.ComputeHash(bytes);
        var stringBuilder = new StringBuilder();
        foreach (var b in hash)
        {
            stringBuilder.Append(b.ToString("x2"));
        }
        addProSportKorisnikRequest.Lozinka = stringBuilder.ToString();


        var proSportKorisnik = new ProSportKorisnici
        {
            Ime = addProSportKorisnikRequest.Ime,
            Prezime = addProSportKorisnikRequest.Prezime,
            Spol = addProSportKorisnikRequest.Spol,
            Email = addProSportKorisnikRequest.Email,
            Lozinka = addProSportKorisnikRequest.Lozinka,
            Telefon = addProSportKorisnikRequest.Telefon,
            Adresa = addProSportKorisnikRequest.Adresa,
            Grad = addProSportKorisnikRequest.Grad,
            Drzava = addProSportKorisnikRequest.Drzava,
            PostanskiBroj = addProSportKorisnikRequest.PostanskiBroj,
            DatumRodenja = addProSportKorisnikRequest.DatumRodenja,
            DatumRegistracije = addProSportKorisnikRequest.DatumRegistracije,
            Verificiran = addProSportKorisnikRequest.Verificiran,
            Status = addProSportKorisnikRequest.Status
        };

        await _proSportKorisniciRepository.AddAsync(proSportKorisnik);
        return RedirectToAction("Index", new { id = proSportKorisnik.IDKorisnik });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var proSportKorisnik = await _proSportKorisniciRepository.GetAsync(id);
        if (proSportKorisnik == null)
        {
            return NotFound();
        }

        var editProSportKorisnikRequest = new EditProSportKorisnikRequest
        {
            IDKorisnik = proSportKorisnik.IDKorisnik,
            Ime = proSportKorisnik.Ime,
            Prezime = proSportKorisnik.Prezime,
            Spol = proSportKorisnik.Spol,
            Email = proSportKorisnik.Email,
            Lozinka = proSportKorisnik.Lozinka,
            Telefon = proSportKorisnik.Telefon,
            Adresa = proSportKorisnik.Adresa,
            Grad = proSportKorisnik.Grad,
            Drzava = proSportKorisnik.Drzava,
            PostanskiBroj = proSportKorisnik.PostanskiBroj,
            DatumRodenja = proSportKorisnik.DatumRodenja,
            DatumRegistracije = proSportKorisnik.DatumRegistracije,
            Verificiran = proSportKorisnik.Verificiran,
            Status = proSportKorisnik.Status
        };

        return View(editProSportKorisnikRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditProSportKorisnikRequest editProSportKorisnikRequest)
    {
        var proSportKorisnik = new ProSportKorisnici
        {
            IDKorisnik = editProSportKorisnikRequest.IDKorisnik,
            Ime = editProSportKorisnikRequest.Ime,
            Prezime = editProSportKorisnikRequest.Prezime,
            Spol = editProSportKorisnikRequest.Spol,
            Email = editProSportKorisnikRequest.Email,
            Lozinka = editProSportKorisnikRequest.Lozinka,
            Telefon = editProSportKorisnikRequest.Telefon,
            Adresa = editProSportKorisnikRequest.Adresa,
            Grad = editProSportKorisnikRequest.Grad,
            Drzava = editProSportKorisnikRequest.Drzava,
            PostanskiBroj = editProSportKorisnikRequest.PostanskiBroj,
            DatumRodenja = editProSportKorisnikRequest.DatumRodenja,
            DatumRegistracije = editProSportKorisnikRequest.DatumRegistracije,
            Verificiran = editProSportKorisnikRequest.Verificiran,
            Status = editProSportKorisnikRequest.Status
        };

        ValidateProSportKorisnikForEdit(proSportKorisnik);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _proSportKorisniciRepository.UpdateAsync(proSportKorisnik);
        return RedirectToAction("Index", new { id = proSportKorisnik.IDKorisnik });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var proSportKorisnik = await _proSportKorisniciRepository.GetAsync(id);
        if (proSportKorisnik == null)
        {
            return NotFound();
        }

        return View(proSportKorisnik);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(EditProSportKorisnikRequest editProSportKorisnikRequest)
    {
        var proSportKorisnik = await _proSportKorisniciRepository.DeleteAsync(editProSportKorisnikRequest.IDKorisnik);

        if (proSportKorisnik == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index", new { id = editProSportKorisnikRequest.IDKorisnik });
    }


    private void ValidateProSportKorisnikForAdd(AddProSportKorisnikRequest addProSportKorisnikRequest)
    {
        if (addProSportKorisnikRequest.DatumRegistracije <= addProSportKorisnikRequest.DatumRodenja)
        {
            ModelState.AddModelError("DatumRegistracije", "Datum registracije mora biti veći od datuma rođenja!");
        }
    }

    private void ValidateProSportKorisnikForEdit(ProSportKorisnici proSportKorisnik)
    {
        if (proSportKorisnik.DatumRegistracije <= proSportKorisnik.DatumRodenja)
        {
            ModelState.AddModelError("DatumRegistracije", "Datum registracije mora biti veći od datuma rođenja!");
        }
    }
}
