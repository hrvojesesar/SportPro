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

    [HttpGet]
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

        return View(promocije);
    }



    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var model = new AddPromocijaRequest
        {
            TipoviPromocijas = applicationDbContext.TipoviPromocija.ToList()
        };
        return View(model);
    }

    [HttpPost]
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
        return RedirectToAction("Index", new { id = promocija.IDPromocije });
    }

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

    [HttpPost]
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
        return RedirectToAction("Index", new { id = promocija.IDPromocije });
    }

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

    [HttpPost]
    public async Task<IActionResult> Delete(EditPromocijaRequest editPromocijaRequest)
    {
        var promocija = await promocijeRepository.DeleteAsync(editPromocijaRequest.IDPromocije);

        if (promocija == null)
        {
            return NotFound();
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