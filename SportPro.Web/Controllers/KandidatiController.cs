using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
[Authorize(Roles = "Menadzer")]
[ApiController]
public class KandidatiController : Controller
{
    private readonly IKandidatiRepository _kandidatiRepository;
    private readonly INatjecajiRepository _natjecajiRepository;

    public KandidatiController(IKandidatiRepository kandidatiRepository, INatjecajiRepository natjecajiRepository)
    {
        _kandidatiRepository = kandidatiRepository;
        _natjecajiRepository = natjecajiRepository;
    }

    /// <summary>
    /// Prikaz svih kandidata
    /// </summary>
    /// <param name="ime"></param>
    /// <param name="prezime"></param>
    /// <param name="grad"></param>
    /// <param name="natjecaj"></param>
    /// <param name="sortBy"></param>
    /// <param name="sortDirection"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Boje>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Index(string? ime, string? prezime, string? grad, string? natjecaj, string? sortBy, string? sortDirection, int pageSize = 3, int pageNumber = 1)
    {
        var totalRecords = await _kandidatiRepository.CountAsync();
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

        ViewBag.Ime = ime;
        ViewBag.Prezime = prezime;
        ViewBag.Grad = grad;
        ViewBag.Natjecaj = natjecaj;

        ViewBag.SortBy = sortBy;
        ViewBag.SortDirection = sortDirection;

        ViewBag.PageSize = pageSize;
        ViewBag.PageNumber = pageNumber;

        var kandidati = await _kandidatiRepository.GetAllAsync(ime, prezime, grad, natjecaj, sortBy, sortDirection, pageNumber, pageSize);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(kandidati);
        }

        return View(kandidati);
    }

    /// <summary>
    /// Dohvaćanje view-a za dodavanje kandidata
    /// </summary>
    /// <returns></returns>
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


    /// <summary>
    /// Dodavanje kandidata
    /// </summary>
    /// <param name="addKandidatRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Kandidati), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(kandidat);
        }

        return RedirectToAction("Index", new { id = kandidat.IDKandidat });
    }

    /// <summary>
    /// Dohvaćanje view-a za uređivanje kandidata
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Uređivanje kandidata
    /// </summary>
    /// <param name="editKandidatRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Kandidati), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(kandidat);
        }

        return RedirectToAction("Index", new { id = kandidat.IDKandidat });
    }

    /// <summary>
    /// Dohvaćanje view-a za brisanje kandidata
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Brisanje kandidata
    /// </summary>
    /// <param name="editKandidatRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Kandidati), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(EditKandidatRequest editKandidatRequest)
    {
        var kandidat = await _kandidatiRepository.DeleteAsync(editKandidatRequest.IDKandidat);

        if (kandidat == null)
        {
            return NotFound();
        }

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok("Kandidat je uspješno uklonjen!");
        }

        return RedirectToAction("Index", new { id = editKandidatRequest.IDKandidat });
    }

    /// <summary>
    /// Dohvaćanje kandidata po natječaju
    /// </summary>
    /// <param name="idNatjecaj"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetKandidatByNatjecaj(int idNatjecaj)
    {
        var imePrezimeList = await _kandidatiRepository.GetByNatjecajAsync(idNatjecaj);
        return Ok(imePrezimeList);
    }

}
