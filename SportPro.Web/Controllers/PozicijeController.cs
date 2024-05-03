using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
public class PozicijeController : Controller
{
    private readonly IPozicijeRepository _pozicijeRepository;

    public PozicijeController(IPozicijeRepository pozicijeRepository)
    {
        _pozicijeRepository = pozicijeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var pozicije = await _pozicijeRepository.GetAllAsync();
        return View(pozicije);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddPozicijaRequest addPozicijaRequest)
    {
        ValidatePozicijaForAdd(addPozicijaRequest);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var pozicija = new Pozicije
        {
            Naziv = addPozicijaRequest.Naziv,
            Opis = addPozicijaRequest.Opis
        };

        await _pozicijeRepository.AddAsync(pozicija);
        return RedirectToAction("Index", new { id = pozicija.IDPozicija });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pozicija = await _pozicijeRepository.GetAsync(id);
        if (pozicija == null)
        {
            return NotFound();
        }

        var editPozicijaRequest = new EditPozicijaRequest
        {
            IDPozicija = pozicija.IDPozicija,
            Naziv = pozicija.Naziv,
            Opis = pozicija.Opis
        };

        return View(editPozicijaRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditPozicijaRequest editPozicijaRequest)
    {    
        var pozicija = new Pozicije
        {
            IDPozicija = editPozicijaRequest.IDPozicija,
            Naziv = editPozicijaRequest.Naziv,
            Opis = editPozicijaRequest.Opis
        };

        ValidatePozicijaForEdit(pozicija);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _pozicijeRepository.UpdateAsync(pozicija);
        return RedirectToAction("Index", new { id = pozicija.IDPozicija });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pozicija = await _pozicijeRepository.GetAsync(id);
        if (pozicija == null)
        {
            return NotFound();
        }

        return View(pozicija);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(EditPozicijaRequest editPozicijaRequest)
    {
        var pozicija = await _pozicijeRepository.DeleteAsync(editPozicijaRequest.IDPozicija);

        if (pozicija == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index", new { id = editPozicijaRequest.IDPozicija });
    }

    private void ValidatePozicijaForAdd(AddPozicijaRequest addPozicijaRequest)
    {
        if (addPozicijaRequest.Naziv.Length > 20)
        {
            ModelState.AddModelError("Naziv", "Naziv ne može biti duži od 20 znakova.");
        }
    }

    private void ValidatePozicijaForEdit(Pozicije pozicije)
    {
        if (pozicije.Naziv != null && pozicije.Naziv.Length > 20)
        {
            ModelState.AddModelError("Naziv", "Naziv ne može biti duži od 20 znakova.");
        }
    }
}
