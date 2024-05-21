using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
public class PravilniciController : Controller
{
    private readonly IPravilniciRepository _pravilniciRepository;

    public PravilniciController(IPravilniciRepository pravilniciRepository)
    {
        _pravilniciRepository = pravilniciRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var pravilnici = await _pravilniciRepository.GetAllAsync();
        return View(pravilnici);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddPravilnikRequest addPravilnikRequest)
    {
        ValidatePravilnikForAdd(addPravilnikRequest);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var pravilnik = new Pravilnici
        {
            Naziv = addPravilnikRequest.Naziv,
            Opis = addPravilnikRequest.Opis,
            DatumObjavljivanja = addPravilnikRequest.DatumObjavljivanja,
            Aktivan = addPravilnikRequest.Aktivan
        };

        await _pravilniciRepository.AddAsync(pravilnik);
        return RedirectToAction("Index", new { id = pravilnik.IDPravilnik });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return BadRequest();
        }

        var pravilnik = await _pravilniciRepository.GetAsync(id);
        if (pravilnik == null)
        {
            return NotFound();
        }

        var editPravilnikRequest = new EditPravilnikRequest
        {
            IDPravilnik = pravilnik.IDPravilnik,
            Naziv = pravilnik.Naziv,
            Opis = pravilnik.Opis,
            DatumObjavljivanja = pravilnik.DatumObjavljivanja,
            Aktivan = pravilnik.Aktivan
        };

        return View(editPravilnikRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditPravilnikRequest editPravilnikRequest)
    {

        var pravilnik = new Pravilnici
        {
            IDPravilnik = editPravilnikRequest.IDPravilnik,
            Naziv = editPravilnikRequest.Naziv,
            Opis = editPravilnikRequest.Opis,
            DatumObjavljivanja = editPravilnikRequest.DatumObjavljivanja,
            Aktivan = editPravilnikRequest.Aktivan
        };

        ValidatePravilnikForEdit(editPravilnikRequest);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _pravilniciRepository.UpdateAsync(pravilnik);
        return RedirectToAction("Index", new { id = pravilnik.IDPravilnik });
    }



    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return BadRequest();
        }

        var pravilnik = await _pravilniciRepository.GetAsync(id);
        if (pravilnik == null)
        {
            return NotFound();
        }

        return View(pravilnik);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(EditPravilnikRequest editPravilnikRequest)
    {
        var pravilnik = await _pravilniciRepository.DeleteAsync(editPravilnikRequest.IDPravilnik);
        if (pravilnik == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index", new { id = editPravilnikRequest.IDPravilnik });
    }

    private void ValidatePravilnikForAdd(AddPravilnikRequest addPravilnikRequest)
    {
        if (addPravilnikRequest.Naziv.Length > 100)
        {
            ModelState.AddModelError("Naziv", "Naziv ne smije biti duži od 100 karaktera!");
        }
    }

    private void ValidatePravilnikForEdit(EditPravilnikRequest editPravilnikRequest)
    {
        if (editPravilnikRequest.Naziv != null && editPravilnikRequest.Naziv.Length > 100)
        {
            ModelState.AddModelError("Naziv", "Naziv ne smije biti duži od 100 karaktera!");
        }
    }
}
