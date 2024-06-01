using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
[Authorize(Roles = "Menadzer")]
public class KandidatiController : Controller
{
    private readonly IKandidatiRepository _kandidatiRepository;
    private readonly INatjecajiRepository _natjecajiRepository;

    public KandidatiController(IKandidatiRepository kandidatiRepository, INatjecajiRepository natjecajiRepository)
    {
        _kandidatiRepository = kandidatiRepository;
        _natjecajiRepository = natjecajiRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var kandidati = await _kandidatiRepository.GetAllAsync();
        return View(kandidati);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var natjecaji = await _natjecajiRepository.GetAllAsync();
      
        var model = new AddKandidatRequest
        {
            Natjecaji = natjecaji.Select(n => new SelectListItem
            {
                Value = n.IDNatjecaj.ToString(),
                Text = n.Naziv
            })
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddKandidatRequest addKandidatRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var kandidat = new Kandidati
        {
            Ime = addKandidatRequest.Ime,
            Prezime = addKandidatRequest.Prezime,
            Adresa = addKandidatRequest.Adresa,
            Grad = addKandidatRequest.Grad,
            Drzava = addKandidatRequest.Drzava,
            PostanskiBroj = addKandidatRequest.PostanskiBroj,
            Email = addKandidatRequest.Email,
            Telefon = addKandidatRequest.Telefon,
            DatumPrijave = addKandidatRequest.DatumPrijave,
            Napomene = addKandidatRequest.Napomene
        };

        var selectedNatjecaji = new List<Natjecaji>();
        foreach (var natjecajId in addKandidatRequest.SelectedNatjecaji)
        {
            var selectedNatjecajId = int.Parse(natjecajId);
            var existingNatjecaj = await _natjecajiRepository.GetAsync(selectedNatjecajId);

            if (existingNatjecaj != null)
            {
                selectedNatjecaji.Add(existingNatjecaj);

                var trajanjeOd = existingNatjecaj.TrajanjeOd;
                var trajanjeDo = existingNatjecaj.TrajanjeDo;

                if (kandidat.DatumPrijave < trajanjeOd || kandidat.DatumPrijave > trajanjeDo)
                {
                    ModelState.AddModelError("DatumPrijave", "Datum prijave mora biti unutar trajanja natječaja.");
                    return BadRequest(ModelState);
                }
            }

        }

        kandidat.Natjecaji = selectedNatjecaji;

        await _kandidatiRepository.AddAsync(kandidat);
        return RedirectToAction("Index", new { id = kandidat.IDKandidat });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var kandidat = await _kandidatiRepository.GetAsync(id);

        if (kandidat == null)
        {
            return NotFound();
        }

        var natjecaji = await _natjecajiRepository.GetAllAsync();
        var model = new EditKandidatRequest
        {
            IDKandidat = kandidat.IDKandidat,
            Ime = kandidat.Ime,
            Prezime = kandidat.Prezime,
            Adresa = kandidat.Adresa,
            Grad = kandidat.Grad,
            Drzava = kandidat.Drzava,
            PostanskiBroj = kandidat.PostanskiBroj,
            Email = kandidat.Email,
            Telefon = kandidat.Telefon,
            DatumPrijave = kandidat.DatumPrijave,
            Napomene = kandidat.Napomene,
            Natjecaji = natjecaji.Select(n => new SelectListItem
            {
                Value = n.IDNatjecaj.ToString(),
                Text = n.Naziv
            }),
            SelectedNatjecaji = kandidat.Natjecaji.Select(n => n.IDNatjecaj.ToString()).ToArray()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditKandidatRequest editKandidatRequest)
    {
        var kandidat = new Kandidati
        {
            IDKandidat = editKandidatRequest.IDKandidat,
            Ime = editKandidatRequest.Ime,
            Prezime = editKandidatRequest.Prezime,
            Adresa = editKandidatRequest.Adresa,
            Grad = editKandidatRequest.Grad,
            Drzava = editKandidatRequest.Drzava,
            PostanskiBroj = editKandidatRequest.PostanskiBroj,
            Email = editKandidatRequest.Email,
            Telefon = editKandidatRequest.Telefon,
            DatumPrijave = editKandidatRequest.DatumPrijave,
            Napomene = editKandidatRequest.Napomene
        };

        var selectedNatjecaji = new List<Natjecaji>();
        foreach (var natjecajId in editKandidatRequest.SelectedNatjecaji)
        {
            var selectedNatjecajId = int.Parse(natjecajId);
            var existingNatjecaj = await _natjecajiRepository.GetAsync(selectedNatjecajId);

            if (existingNatjecaj != null)
            {
                selectedNatjecaji.Add(existingNatjecaj);

                var trajanjeOd = existingNatjecaj.TrajanjeOd;
                var trajanjeDo = existingNatjecaj.TrajanjeDo;

                if (kandidat.DatumPrijave < trajanjeOd || kandidat.DatumPrijave > trajanjeDo)
                {
                    ModelState.AddModelError("DatumPrijave", "Datum prijave mora biti unutar trajanja natječaja.");
                    return BadRequest(ModelState);
                }
            }
        }

        kandidat.Natjecaji = selectedNatjecaji;

        await _kandidatiRepository.UpdateAsync(kandidat);
        return RedirectToAction("Index", new { id = kandidat.IDKandidat });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var kandidat = await _kandidatiRepository.GetAsync(id);

        if (kandidat == null)
        {
            return NotFound();
        }

        return View(kandidat);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(EditKandidatRequest editKandidatRequest)
    {
        var kandidat = await _kandidatiRepository.DeleteAsync(editKandidatRequest.IDKandidat);

        if (kandidat == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index", new { id = editKandidatRequest.IDKandidat });
    }

    [HttpGet]
    public async Task<IActionResult> GetKandidatByNatjecaj(int idNatjecaj)
    {
        var imePrezimeList = await _kandidatiRepository.GetByNatjecajAsync(idNatjecaj);
        return Ok(imePrezimeList);
    }

}
