using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
public class DobavljaciController : Controller
{
    private readonly IDobavljaciRepository dobavljaciRepository;

    public DobavljaciController(IDobavljaciRepository dobavljaciRepository)
    {
        this.dobavljaciRepository = dobavljaciRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var dobavljaci = await dobavljaciRepository.GetAllAsync();
        return View(dobavljaci);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddDobavljacRequest addDobavljacRequest)
    {
        //ValidateDobavljacForAdd(addDobavljacRequest);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var dobavljac = new Dobavljaci
        {
            Naziv = addDobavljacRequest.Naziv,
            Adresa = addDobavljacRequest.Adresa,
            Grad = addDobavljacRequest.Grad,
            Drzava = addDobavljacRequest.Drzava,
            Telefon = addDobavljacRequest.Telefon,
            Email = addDobavljacRequest.Email,
            PocetakSuradnje = addDobavljacRequest.PocetakSuradnje,
            KrajSuradnje = addDobavljacRequest.KrajSuradnje,
            SuradnjaAktivna = addDobavljacRequest.SuradnjaAktivna
        };

        await dobavljaciRepository.AddAsync(dobavljac);
        return RedirectToAction("Index", new { id = addDobavljacRequest.IDDobavljac });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var dobavljac = await dobavljaciRepository.GetAsync(id);

        if (dobavljac == null)
        {
            return NotFound();
        }

        var editDobavljacRequest = new EditDobavljacRequest
        {
            IDDobavljac = dobavljac.IDDobavljac,
            Naziv = dobavljac.Naziv,
            Adresa = dobavljac.Adresa,
            Grad = dobavljac.Grad,
            Drzava = dobavljac.Drzava,
            Telefon = dobavljac.Telefon,
            Email = dobavljac.Email,
            PocetakSuradnje = dobavljac.PocetakSuradnje,
            KrajSuradnje = dobavljac.KrajSuradnje,
            SuradnjaAktivna = dobavljac.SuradnjaAktivna
        };

        return View(editDobavljacRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditDobavljacRequest editDobavljacRequest)
    {
        var dobavljac = new Dobavljaci
        {
            IDDobavljac = editDobavljacRequest.IDDobavljac,
            Naziv = editDobavljacRequest.Naziv,
            Adresa = editDobavljacRequest.Adresa,
            Grad = editDobavljacRequest.Grad,
            Drzava = editDobavljacRequest.Drzava,
            Telefon = editDobavljacRequest.Telefon,
            Email = editDobavljacRequest.Email,
            PocetakSuradnje = editDobavljacRequest.PocetakSuradnje,
            KrajSuradnje = editDobavljacRequest.KrajSuradnje,
            SuradnjaAktivna = editDobavljacRequest.SuradnjaAktivna
        };

        //ValidateDobavljacForEdit(dobavljac);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await dobavljaciRepository.UpdateAsync(dobavljac);
        return RedirectToAction("Index", new { id = editDobavljacRequest.IDDobavljac });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var dobavljac = await dobavljaciRepository.GetAsync(id);

        if (dobavljac == null)
        {
            return NotFound();
        }

        return View(dobavljac);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(EditDobavljacRequest editDobavljacRequest)
    {
        var dobavljac = await dobavljaciRepository.DeleteAsync(editDobavljacRequest.IDDobavljac);

        if (dobavljac == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index", new { id = editDobavljacRequest.IDDobavljac });
    }

    [HttpGet]
    public async Task<IActionResult> GetActiveDobavljaci()
    {
        var dobavljaci = await dobavljaciRepository.GetActiveDobavljaci();
        return Ok(dobavljaci);
    }

    //private void ValidateDobavljacForAdd(AddDobavljacRequest addDobavljacRequest)
    //{
    //    if (addDobavljacRequest.PocetakSuradnje >= addDobavljacRequest.KrajSuradnje)
    //    {
    //        ModelState.AddModelError("PocetakSuradnje", "Datum početka suradnje ne može biti veći od datuma završetka suradnje!");
    //    }
    //}

    //private void ValidateDobavljacForEdit(Dobavljaci dobavljac)
    //{
    //    if (dobavljac.PocetakSuradnje >= dobavljac.KrajSuradnje)
    //    {
    //        ModelState.AddModelError("PocetakSuradnje", "Datum početka suradnje ne može biti veći od datuma završetka suradnje!");
    //    }
    //}
}
