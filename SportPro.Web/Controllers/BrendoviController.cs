using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
[Authorize(Roles = "Uposlenik")]
public class BrendoviController : Controller
{
    private readonly IBrendoviRepository brendoviRepository;

    public BrendoviController(IBrendoviRepository brendoviRepository)
    {
        this.brendoviRepository = brendoviRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? nazivBrenda, string? vrsta, string? status, string? sortBy, string? sortDirection, int pageSize = 4, int pageNumber = 1)
    {
        var totalRecords = await brendoviRepository.CountAsync();
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

        ViewBag.NazivBrenda = nazivBrenda;
        ViewBag.Vrsta = vrsta;
        ViewBag.Status = status;

        var vrstaList = new List<SelectListItem>
    {
        new SelectListItem { Value = "", Text = "Svi" },
        new SelectListItem { Value = "Javni", Text = "Javni" },
        new SelectListItem { Value = "Privatni", Text = "Privatni" },
    };
        ViewBag.VrstaList = new SelectList(vrstaList, "Value", "Text", vrsta);

        var statusList = new List<SelectListItem>
    {
        new SelectListItem { Value = "", Text = "Svi" },
        new SelectListItem { Value = "Dostupni", Text = "Dostupni" },
        new SelectListItem { Value = "Nedostupni", Text = "Nedostupni" },
    };
        ViewBag.StatusList = new SelectList(statusList, "Value", "Text", status);


        ViewBag.SortBy = sortBy;
        ViewBag.SortDirection = sortDirection;

        var brendovi = await brendoviRepository.GetAllAsync(nazivBrenda, vrsta, status, sortBy, sortDirection, pageNumber, pageSize);
        return View(brendovi);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddBrendRequest addBrendRequest)
    {
        ValidateBrendForAdd(addBrendRequest);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var brend = new Brendovi
        {
            NazivBrenda = addBrendRequest.NazivBrenda,
            Vrsta = addBrendRequest.Vrsta,
            Osnivac = addBrendRequest.Osnivac,
            GodinaOsnivanja = addBrendRequest.GodinaOsnivanja,
            Sjediste = addBrendRequest.Sjediste,
            Predsjednik = addBrendRequest.Predsjednik,
            Status = addBrendRequest.Status
        };

        await brendoviRepository.AddAsync(brend);
        return RedirectToAction("Index", new { id = brend.IDBrend });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var brend = await brendoviRepository.GetAsync(id);
        if (brend == null)
        {
            return NotFound();
        }

        var editBrendRequest = new EditBrendRequest
        {
            IDBrend = brend.IDBrend,
            NazivBrenda = brend.NazivBrenda,
            Vrsta = brend.Vrsta,
            Osnivac = brend.Osnivac,
            GodinaOsnivanja = brend.GodinaOsnivanja,
            Sjediste = brend.Sjediste,
            Predsjednik = brend.Predsjednik,
            Status = brend.Status
        };

        return View(editBrendRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditBrendRequest editBrendRequest)
    {
        var brend = new Brendovi
        {
            IDBrend = editBrendRequest.IDBrend,
            NazivBrenda = editBrendRequest.NazivBrenda,
            Vrsta = editBrendRequest.Vrsta,
            Osnivac = editBrendRequest.Osnivac,
            GodinaOsnivanja = editBrendRequest.GodinaOsnivanja,
            Sjediste = editBrendRequest.Sjediste,
            Predsjednik = editBrendRequest.Predsjednik,
            Status = editBrendRequest.Status
        };

        ValidateBrendForEdit(brend);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await brendoviRepository.UpdateAsync(brend);
        return RedirectToAction("Index", new { id = brend.IDBrend });
    }


    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var brend = await brendoviRepository.GetAsync(id);
        if (brend == null)
        {
            return NotFound();
        }

        return View(brend);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(EditBrendRequest editBrendRequest)
    {
        var brend = await brendoviRepository.DeleteAsync(editBrendRequest.IDBrend);

        if (brend == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index", new { id = editBrendRequest.IDBrend });
    }

    private void ValidateBrendForAdd(AddBrendRequest addBrendRequest)
    {
        if (addBrendRequest.NazivBrenda.Length > 20)
        {
            ModelState.AddModelError("NazivBrenda", "Naziv brenda ne smije biti duži od 20 karaktera.");
        }
        if (addBrendRequest.Vrsta.Length > 10)
        {
            ModelState.AddModelError("Vrsta", "Vrsta brenda ne smije biti duža od 10 karaktera.");
        }
        if (addBrendRequest.Osnivac.Length > 50)
        {
            ModelState.AddModelError("Osnivac", "Osnivač brenda ne smije biti duži od 50 karaktera.");
        }
        if (addBrendRequest.Sjediste.Length > 100)
        {
            ModelState.AddModelError("Sjediste", "Sjedište brenda ne smije biti duže od 100 karaktera.");
        }
        if (addBrendRequest.Predsjednik.Length > 50)
        {
            ModelState.AddModelError("Predsjednik", "Predsjednik brenda ne smije biti duži od 50 karaktera.");
        }
        if (addBrendRequest.Status.Length > 10)
        {
            ModelState.AddModelError("Status", "Status brenda ne smije biti duži od 10 karaktera.");
        }
    }

    private void ValidateBrendForEdit(Brendovi brend)
    {
        if (brend.NazivBrenda != null && brend.NazivBrenda.Length > 20)
        {
            ModelState.AddModelError("NazivBrenda", "Naziv brenda ne smije biti duži od 20 karaktera.");
        }
        if (brend.Vrsta != null && brend.Vrsta.Length > 10)
        {
            ModelState.AddModelError("Vrsta", "Vrsta brenda ne smije biti duža od 10 karaktera.");
        }
        if (brend.Osnivac != null && brend.Osnivac.Length > 50)
        {
            ModelState.AddModelError("Osnivac", "Osnivač brenda ne smije biti duži od 50 karaktera.");
        }
        if (brend.Sjediste != null && brend.Sjediste.Length > 100)
        {
            ModelState.AddModelError("Sjediste", "Sjedište brenda ne smije biti duže od 100 karaktera.");
        }
        if (brend.Predsjednik != null && brend.Predsjednik.Length > 50)
        {
            ModelState.AddModelError("Predsjednik", "Predsjednik brenda ne smije biti duži od 50 karaktera.");
        }
        if (brend.Status != null && brend.Status.Length > 10)
        {
            ModelState.AddModelError("Status", "Status brenda ne smije biti duži od 10 karaktera.");
        }

    }
}
