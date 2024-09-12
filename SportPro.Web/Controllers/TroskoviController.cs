using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
[Authorize(Roles = "Uposlenik")]
public class TroskoviController : Controller
{
    private readonly ITroskoviRepository _troskoviRepository;
    private readonly IKategorijeTroskovaRepository _kategorijeTroskovaRepository;
    private readonly ApplicationDbContext applicationDbContext;

    public TroskoviController(ITroskoviRepository troskoviRepository, IKategorijeTroskovaRepository kategorijeTroskovaRepository, ApplicationDbContext applicationDbContext)
    {
        _troskoviRepository = troskoviRepository;
        _kategorijeTroskovaRepository = kategorijeTroskovaRepository;
        this.applicationDbContext = applicationDbContext;
    }

    /// <summary>
    /// Prikaz svih troškova
    /// </summary>
    /// <param name="naziv"></param>
    /// <param name="kategorijaTroska"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <param name="sortBy"></param>
    /// <param name="sortDirection"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Troskovi>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Index(string? naziv, string? kategorijaTroska, int? minValue, int? maxValue, string? sortBy, string? sortDirection, int pageSize = 5, int pageNumber = 1)
    {
        var totalRecords = await _troskoviRepository.CountAsync();
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

        ViewBag.Naziv = naziv;
        ViewBag.KategorijaTroska = kategorijaTroska;
        ViewBag.MinValue = minValue ?? 0;
        ViewBag.MaxValue = maxValue ?? 10000;

        ViewBag.SortBy = sortBy;
        ViewBag.SortDirection = sortDirection;

        ViewBag.PageSize = pageSize;
        ViewBag.PageNumber = pageNumber;

        var kategorijeTroskova = await _kategorijeTroskovaRepository.GetAllSecAsync();

        var kategorijeTroskovaList = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "Svi" }
        };

        kategorijeTroskovaList.AddRange(kategorijeTroskova.Select(x => new SelectListItem { Value = x.IDKategorijaTroska.ToString(), Text = x.Naziv }));

        ViewBag.KategorijeTroskovaList = new SelectList(kategorijeTroskovaList, "Value", "Text", kategorijaTroska);

        var troskovi = await _troskoviRepository.GetAllAsync(naziv, kategorijaTroska, minValue, maxValue, sortBy, sortDirection, pageNumber, pageSize);

        ViewData["KategorijeTroskova"] = kategorijeTroskova;

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(troskovi);
        }

        return View(troskovi);
    }

    /// <summary>
    /// Dohvaćanje view-a za dodavanje troška
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var model = new AddTrosakRequest
        {
            KategorijeTroskovas = await _kategorijeTroskovaRepository.GetAllSecAsync()
        };
        return View(model);
    }

    /// <summary>
    /// Dodavanje troška
    /// </summary>
    /// <param name="addTrosakRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Troskovi>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add(AddTrosakRequest addTrosakRequest)
    {
        ValidateTrosakForAdd(addTrosakRequest);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var trosak = new Troskovi
        {
            Naziv = addTrosakRequest.Naziv,
            Opis = addTrosakRequest.Opis,
            Datum = addTrosakRequest.Datum,
            Iznos = addTrosakRequest.Iznos,
            Mjesec = addTrosakRequest.Mjesec,
            Godina = addTrosakRequest.Godina,
            KategorijeTroskovaIDKategorijaTroska = addTrosakRequest.KategorijeTroskovaIDKategorijaTroska
        };

        await _troskoviRepository.AddAsync(trosak);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(trosak);
        }

        return RedirectToAction("Index", new { id = trosak.IDTrosak });
    }

    /// <summary>
    /// Dohvaćanje view-a za uređivanje troška
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

        var trosak = await _troskoviRepository.GetAsync(id);

        if (trosak == null)
        {
            return NotFound();
        }

        var model = new EditTrosakRequest
        {
            IDTrosak = trosak.IDTrosak,
            Naziv = trosak.Naziv,
            Opis = trosak.Opis,
            Datum = trosak.Datum,
            Iznos = trosak.Iznos,
            Mjesec = trosak.Mjesec,
            Godina = trosak.Godina,
            KategorijeTroskovaIDKategorijaTroska = trosak.KategorijeTroskovaIDKategorijaTroska,
            KategorijeTroskovas = applicationDbContext.KategorijeTroskova.ToList()
        };

        return View(model);
    }

    /// <summary>
    /// Uređivanje troška
    /// </summary>
    /// <param name="editTrosakRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Troskovi>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Edit(EditTrosakRequest editTrosakRequest)
    {
        var trosak = new Troskovi
        {
            IDTrosak = editTrosakRequest.IDTrosak,
            Naziv = editTrosakRequest.Naziv,
            Opis = editTrosakRequest.Opis,
            Datum = editTrosakRequest.Datum,
            Iznos = editTrosakRequest.Iznos,
            Mjesec = editTrosakRequest.Mjesec,
            Godina = editTrosakRequest.Godina,
            KategorijeTroskovaIDKategorijaTroska = editTrosakRequest.KategorijeTroskovaIDKategorijaTroska
        };

        ValidateTrosakForEdit(trosak);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _troskoviRepository.UpdateAsync(trosak);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(trosak);
        }

        return RedirectToAction("Index", new { id = trosak.IDTrosak });
    }

    /// <summary>
    /// Dohvaćanje view-a za brisanje troška
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

        var trosak = await _troskoviRepository.GetAsync(id);

        if (trosak == null)
        {
            return NotFound();
        }

        var kategorijaTroska = await _kategorijeTroskovaRepository.GetAsync(trosak.KategorijeTroskovaIDKategorijaTroska);
        trosak.KategorijaTroskaNaziv = kategorijaTroska?.Naziv;

        return View(trosak);
    }

    /// <summary>
    /// Brisanje troška
    /// </summary>
    /// <param name="editTrosakRequest"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Delete(EditTrosakRequest editTrosakRequest)
    {
        var trosak = await _troskoviRepository.DeleteAsync(editTrosakRequest.IDTrosak);

        if (trosak == null)
        {
            return NotFound();
        }

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok("Trošak je uspješno uklonjen!");
        }

        return RedirectToAction("Index", new { id = editTrosakRequest.IDTrosak });

    }

    private void ValidateTrosakForAdd(AddTrosakRequest addTrosakRequest)
    {
        if (addTrosakRequest.Iznos < 0)
        {
            ModelState.AddModelError("Iznos", "Iznos mora biti veći od 0");
        }

        if (addTrosakRequest.Mjesec != null)
        {
            if (addTrosakRequest.Mjesec > 12 || addTrosakRequest.Mjesec < 1)
            {
                ModelState.AddModelError("Mjesec", "Mjesec mora biti između 1 i 12");
            }
        }
    }

    private void ValidateTrosakForEdit(Troskovi troskovi)
    {
        if (troskovi.Iznos < 0)
        {
            ModelState.AddModelError("Iznos", "Iznos mora biti veći od 0");
        }

        if (troskovi.Mjesec != null)
        {
            if (troskovi.Mjesec > 12 || troskovi.Mjesec < 1)
            {
                ModelState.AddModelError("Mjesec", "Mjesec mora biti između 1 i 12");
            }
        }
    }
}

