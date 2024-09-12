using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;
using SportPro.Web.Repositories;
using System.Net;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
[Authorize(Roles = "Uposlenik")]
public class PromocijeController : Controller
{
    private readonly IPromocijeRepository promocijeRepository;
    private readonly ITipoviPromocijaRepository tipoviPromocijaRepository;
    private readonly ApplicationDbContext applicationDbContext;
    private readonly IImagesRepository imagesRepository;

    public PromocijeController(IPromocijeRepository promocijeRepository, ITipoviPromocijaRepository tipoviPromocijaRepository, ApplicationDbContext applicationDbContext, IImagesRepository imagesRepository)
    {
        this.promocijeRepository = promocijeRepository;
        this.tipoviPromocijaRepository = tipoviPromocijaRepository;
        this.applicationDbContext = applicationDbContext;
        this.imagesRepository = imagesRepository;
    }

    /// <summary>
    /// Prikaz svih promocija
    /// </summary>
    /// <param name="naziv"></param>
    /// <param name="tipPromocije"></param>
    /// <param name="aktivan"></param>
    /// <param name="sortBy"></param>
    /// <param name="sortDirection"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Promocije>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Index(string? naziv, string? tipPromocije, string? aktivan, string? sortBy, string? sortDirection, int pageSize = 3, int pageNumber = 1)
    {
        var totalRecords = await promocijeRepository.CountAsync();
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
        ViewBag.TipPromocije = tipPromocije;
        ViewBag.Aktivan = aktivan;

        ViewBag.SortBy = sortBy;
        ViewBag.SortDirection = sortDirection;

        ViewBag.PageSize = pageSize;
        ViewBag.PageNumber = pageNumber;

        var statusList = new SelectList(new[]
        {
        new { Value = "", Text = "Svi" },
        new { Value = "1", Text = "Aktivni" },
        new { Value = "0", Text = "Neaktivni" },
    }, "Value", "Text", aktivan);

        ViewBag.AktivanList = statusList;

        var tipoviPromocija = await tipoviPromocijaRepository.GetAllSecAsync();

        var tipPromocijeList = new List<SelectListItem>
    {
        new SelectListItem { Value = "", Text = "Svi" }
    };

        tipPromocijeList.AddRange(tipoviPromocija.Select(tp => new SelectListItem
        {
            Value = tp.IDTipPromocije.ToString(),
            Text = tp.Naziv
        }));

        ViewBag.TipPromocijeList = new SelectList(tipPromocijeList, "Value", "Text", tipPromocije);

        var promocije = await promocijeRepository.GetAllAsync(naziv, tipPromocije, aktivan, sortBy, sortDirection, pageNumber, pageSize);

        ViewData["TipoviPromocija"] = tipoviPromocija;

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(promocije);
        }

        return View(promocije);
    }


    /// <summary>
    /// Dohvaćanje view-a za dodavanje promocije
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var model = new AddPromocijaRequest
        {
            TipoviPromocijas = applicationDbContext.TipoviPromocija.ToList()
        };
        return View(model);
    }

    /// <summary>
    /// Dodavanje nove promocije
    /// </summary>
    /// <param name="addPromocijaRequest"></param>
    /// <param name="slike"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Promocije>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add(AddPromocijaRequest addPromocijaRequest, IEnumerable<IFormFile> slike)
    {
        ValidatePromocijaForAdd(addPromocijaRequest);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }


        var promocija = new Promocije
        {
            Naziv = addPromocijaRequest.Naziv,
            Opis = addPromocijaRequest.Opis,
            DatumPocetka = addPromocijaRequest.DatumPocetka,
            DatumZavrsetka = addPromocijaRequest.DatumZavrsetka,
            Aktivna = addPromocijaRequest.Aktivna,
            DodatniUvjeti = addPromocijaRequest.DodatniUvjeti,
            Slika = addPromocijaRequest.Slika,
            TipoviPromocijaIDTipPromocije = addPromocijaRequest.TipoviPromocijaIDTipPromocije
        };

        await promocijeRepository.AddAsync(promocija);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(promocija);
        }

        return RedirectToAction("Index", new { id = promocija.IDPromocije });
    }

    /// <summary>
    /// Dohvaćanje view-a za uređivanje promocije
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

        var promocija = await promocijeRepository.GetAsync(id);

        if (promocija == null)
        {
            return NotFound();
        }

        var model = new EditPromocijaRequest
        {
            IDPromocije = promocija.IDPromocije,
            Naziv = promocija.Naziv,
            Opis = promocija.Opis,
            DatumPocetka = promocija.DatumPocetka,
            DatumZavrsetka = promocija.DatumZavrsetka,
            Aktivna = promocija.Aktivna,
            DodatniUvjeti = promocija.DodatniUvjeti,
            Slika = promocija.Slika,
            TipoviPromocijaIDTipPromocije = promocija.TipoviPromocijaIDTipPromocije,
            TipoviPromocijas = applicationDbContext.TipoviPromocija.ToList()
        };

        return View(model);
    }

    /// <summary>
    /// Uređivanje promocije
    /// </summary>
    /// <param name="editPromocijaRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Promocije>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Edit(EditPromocijaRequest editPromocijaRequest)
    {
        var promocija = new Promocije
        {
            IDPromocije = editPromocijaRequest.IDPromocije,
            Naziv = editPromocijaRequest.Naziv,
            Opis = editPromocijaRequest.Opis,
            DatumPocetka = editPromocijaRequest.DatumPocetka,
            DatumZavrsetka = editPromocijaRequest.DatumZavrsetka,
            Aktivna = editPromocijaRequest.Aktivna,
            DodatniUvjeti = editPromocijaRequest.DodatniUvjeti,
            Slika = editPromocijaRequest.Slika,
            TipoviPromocijaIDTipPromocije = editPromocijaRequest.TipoviPromocijaIDTipPromocije
        };

        ValidatePromocijaForEdit(promocija);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await promocijeRepository.UpdateAsync(promocija);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(promocija);
        }

        return RedirectToAction("Index", new { id = promocija.IDPromocije });
    }

    /// <summary>
    /// Dohvaćanje view-a za brisanje promocije
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

        var promocija = await promocijeRepository.GetAsync(id);
        var tipoviPromocija = await tipoviPromocijaRepository.GetAllAsync();

        ViewData["TipoviPromocija"] = tipoviPromocija;

        if (promocija == null)
        {
            return NotFound();
        }

        return View(promocija);
    }

    /// <summary>
    /// Brisanje promocije
    /// </summary>
    /// <param name="editPromocijaRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Promocije>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(EditPromocijaRequest editPromocijaRequest)
    {
        var promocija = await promocijeRepository.DeleteAsync(editPromocijaRequest.IDPromocije);

        if (promocija == null)
        {
            return NotFound();
        }

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok("Promocija je uspješno uklonjena!");
        }

        return RedirectToAction("Index", new { id = editPromocijaRequest.IDPromocije });
    }

    private void ValidatePromocijaForAdd(AddPromocijaRequest addPromocijaRequest)
    {
        if (addPromocijaRequest.DatumPocetka >= addPromocijaRequest.DatumZavrsetka)
        {
            ModelState.AddModelError("DatumPocetka", "Datum početka promocije mora početi prije datuma završetka!");
        }
    }

    private void ValidatePromocijaForEdit(Promocije promocije)
    {
        if (promocije.DatumPocetka >= promocije.DatumZavrsetka)
        {
            ModelState.AddModelError("DatumPocetka", "Datum početka promocije mora početi prije datuma završetka!");
        }
    }
}