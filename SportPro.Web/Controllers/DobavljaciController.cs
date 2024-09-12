using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;
using SportPro.Web.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
[ApiController]
[Authorize(Roles = "Uposlenik")]
public class DobavljaciController : Controller
{
    private readonly IDobavljaciRepository dobavljaciRepository;

    public DobavljaciController(IDobavljaciRepository dobavljaciRepository)
    {
        this.dobavljaciRepository = dobavljaciRepository;
    }

    /// <summary>
    /// Prikaz svih dobavljača
    /// </summary>
    /// <param name="naziv"></param>
    /// <param name="grad"></param>
    /// <param name="aktivnaSuradnja"></param>
    /// <param name="sortBy"></param>
    /// <param name="sortDirection"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Dobavljaci>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Index(string? naziv, string? grad, string? aktivnaSuradnja, string? sortBy, string? sortDirection, int pageSize = 5, int pageNumber = 1)
    {
        var totalRecords = await dobavljaciRepository.CountAsync();
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

        ViewBag.PageSize = pageSize;
        ViewBag.PageNumber = pageNumber;

        ViewBag.Naziv = naziv;
        ViewBag.Grad = grad;
        ViewBag.AktivnaSuradnja = aktivnaSuradnja;

        var suradnjaList = new List<string> { "Svi", "Aktivna suradnja", "Neaktivna suradnja" };

        ViewBag.AktivnaSuradnjaList = new SelectList(suradnjaList);

        ViewBag.SortBy = sortBy;
        ViewBag.SortDirection = sortDirection;

        var dobavljaci = await dobavljaciRepository.GetAllAsync(naziv, grad, aktivnaSuradnja, sortBy, sortDirection, pageNumber, pageSize);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(dobavljaci);
        }

        return View(dobavljaci);
    }

    /// <summary>
    /// Dohvaćanje view-a za dodavanje dobavljača
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    /// <summary>
    /// Dodavanje dobavljača
    /// </summary>
    /// <param name="addDobavljacRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Dobavljaci>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(dobavljac);
        }

        return RedirectToAction("Index", new { id = addDobavljacRequest.IDDobavljac });
    }

    /// <summary>
    /// Dohvaćanje view-a za uređivanje dobavljača
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

    /// <summary>
    /// Uređivanje dobavljača
    /// </summary>
    /// <param name="editDobavljacRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Dobavljaci>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(dobavljac);
        }

        return RedirectToAction("Index", new { id = editDobavljacRequest.IDDobavljac });
    }

    /// <summary>
    /// Dohvaćanje view-a za brisanje dobavljača
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

        var dobavljac = await dobavljaciRepository.GetAsync(id);

        if (dobavljac == null)
        {
            return NotFound();
        }

        return View(dobavljac);
    }

    /// <summary>
    /// Brisanje dobavljača
    /// </summary>
    /// <param name="editDobavljacRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Dobavljaci>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(EditDobavljacRequest editDobavljacRequest)
    {
        var dobavljac = await dobavljaciRepository.DeleteAsync(editDobavljacRequest.IDDobavljac);

        if (dobavljac == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index", new { id = editDobavljacRequest.IDDobavljac });
    }


    /// <summary>
    /// Dohvaćanje aktivnih dobavljača
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Dobavljaci>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
