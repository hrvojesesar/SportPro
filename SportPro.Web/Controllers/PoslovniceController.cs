using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;
using Syncfusion.EJ2.HeatMap;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
[Authorize(Roles = "Uposlenik")]
public class PoslovniceController : Controller
{
    private readonly IPoslovniceRepository _poslovniceRepository;

    public PoslovniceController(IPoslovniceRepository poslovniceRepository)
    {
        _poslovniceRepository = poslovniceRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? naziv, string? grad, string? status, string? sortBy, string? sortDirection, int pageSize = 5, int pageNumber = 1)
    {
        var totalRecords = await _poslovniceRepository.CountAsync();
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
        ViewBag.Status = status;

        var statusList = new List<SelectListItem>
    {
        new SelectListItem { Value = "", Text = "Svi" },
        new SelectListItem { Value = "Aktivni", Text = "Aktivni" },
        new SelectListItem { Value = "Neaktivni", Text = "Neaktivni" },
    };
        ViewBag.StatusList = new SelectList(statusList, "Value", "Text", status);

        ViewBag.SortBy = sortBy;
        ViewBag.SortDirection = sortDirection;


        var poslovnice = await _poslovniceRepository.GetAllAsync(naziv, grad, status, sortBy, sortDirection, pageNumber, pageSize);
        return View(poslovnice);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddPoslovnicaRequest addPoslovnicaRequest)
    {
        ValidatePoslovnicaForAdd(addPoslovnicaRequest);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var poslovnica = new Poslovnice
        {
            Naziv = addPoslovnicaRequest.Naziv,
            Adresa = addPoslovnicaRequest.Adresa,
            Grad = addPoslovnicaRequest.Grad,
            Drzava = addPoslovnicaRequest.Drzava,
            Telefon = addPoslovnicaRequest.Telefon,
            Email = addPoslovnicaRequest.Email,
            RadnoVrijemeOd = addPoslovnicaRequest.RadnoVrijemeOd,
            RadnoVrijemeDo = addPoslovnicaRequest.RadnoVrijemeDo,
            DatumOtvaranja = addPoslovnicaRequest.DatumOtvaranja,
            Status = addPoslovnicaRequest.Status
        };

        await _poslovniceRepository.AddAsync(poslovnica);
        return RedirectToAction("Index", new { id = poslovnica.IDPoslovnica });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var poslovnica = await _poslovniceRepository.GetAsync(id);
        if (poslovnica == null)
        {
            return NotFound();
        }

        var editPoslovnicaRequest = new EditPoslovnicaRequest
        {
            IDPoslovnica = poslovnica.IDPoslovnica,
            Naziv = poslovnica.Naziv,
            Adresa = poslovnica.Adresa,
            Grad = poslovnica.Grad,
            Drzava = poslovnica.Drzava,
            Telefon = poslovnica.Telefon,
            Email = poslovnica.Email,
            RadnoVrijemeOd = poslovnica.RadnoVrijemeOd,
            RadnoVrijemeDo = poslovnica.RadnoVrijemeDo,
            DatumOtvaranja = poslovnica.DatumOtvaranja,
            Status = poslovnica.Status
        };

        return View(editPoslovnicaRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditPoslovnicaRequest editPoslovnicaRequest)
    {

        var poslovnica = new Poslovnice
        {
            IDPoslovnica = editPoslovnicaRequest.IDPoslovnica,
            Naziv = editPoslovnicaRequest.Naziv,
            Adresa = editPoslovnicaRequest.Adresa,
            Grad = editPoslovnicaRequest.Grad,
            Drzava = editPoslovnicaRequest.Drzava,
            Telefon = editPoslovnicaRequest.Telefon,
            Email = editPoslovnicaRequest.Email,
            RadnoVrijemeOd = editPoslovnicaRequest.RadnoVrijemeOd,
            RadnoVrijemeDo = editPoslovnicaRequest.RadnoVrijemeDo,
            DatumOtvaranja = editPoslovnicaRequest.DatumOtvaranja,
            Status = editPoslovnicaRequest.Status
        };

        ValidatePoslovnicaForEdit(poslovnica);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _poslovniceRepository.UpdateAsync(poslovnica);
        return RedirectToAction("Index", new { id = poslovnica.IDPoslovnica });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var poslovnica = await _poslovniceRepository.GetAsync(id);
        if (poslovnica == null)
        {
            return NotFound();
        }

        return View(poslovnica);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(EditPoslovnicaRequest editPoslovnicaRequest)
    {
        var poslovnica = await _poslovniceRepository.DeleteAsync(editPoslovnicaRequest.IDPoslovnica);

        if (poslovnica == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index", new { id = editPoslovnicaRequest.IDPoslovnica });
    }

    private void ValidatePoslovnicaForAdd(AddPoslovnicaRequest addPoslovnicaRequest)
    {
        if (addPoslovnicaRequest.Naziv.Length > 50)
        {
            ModelState.AddModelError("Naziv", "Naziv ne smije biti duži od 50 znakova");
        }
        if (addPoslovnicaRequest.Adresa.Length > 50)
        {
            ModelState.AddModelError("Adresa", "Adresa ne smije biti duža od 50 znakova");
        }
        if (addPoslovnicaRequest.Grad.Length > 50)
        {
            ModelState.AddModelError("Grad", "Grad ne smije biti duži od 50 znakova");
        }
        if (addPoslovnicaRequest.Drzava.Length > 50)
        {
            ModelState.AddModelError("Drzava", "Država ne smije biti duža od 50 znakova");
        }
        if (addPoslovnicaRequest.Telefon.Length > 20)
        {
            ModelState.AddModelError("Telefon", "Telefon ne smije biti duži od 20 znakova");
        }
        if (addPoslovnicaRequest.Email.Length > 50)
        {
            ModelState.AddModelError("Email", "Email ne smije biti duži od 50 znakova");
        }
        if (addPoslovnicaRequest.RadnoVrijemeOd != null && addPoslovnicaRequest.RadnoVrijemeDo != null && addPoslovnicaRequest.RadnoVrijemeDo <= addPoslovnicaRequest.RadnoVrijemeOd)
        {
            ModelState.AddModelError("RadnoVrijemeDo", "Kraj radnog vremena mora biti poslije početka radnog vremena!");
        }
        if (addPoslovnicaRequest.Status.Length > 10)
        {
            ModelState.AddModelError("Status", "Status ne smije biti duži od 10 znakova");
        }

    }

    private void ValidatePoslovnicaForEdit(Poslovnice poslovnica)
    {
        if (poslovnica.Naziv != null && poslovnica.Naziv.Length > 50)
        {
            ModelState.AddModelError("Naziv", "Naziv ne smije biti duži od 50 znakova");
        }
        if (poslovnica.Adresa != null && poslovnica.Adresa.Length > 50)
        {
            ModelState.AddModelError("Adresa", "Adresa ne smije biti duža od 50 znakova");
        }
        if (poslovnica.Grad != null && poslovnica.Grad.Length > 50)
        {
            ModelState.AddModelError("Grad", "Grad ne smije biti duži od 50 znakova");
        }
        if (poslovnica.Drzava != null && poslovnica.Drzava.Length > 50)
        {
            ModelState.AddModelError("Drzava", "Država ne smije biti duža od 50 znakova");
        }
        if (poslovnica.Telefon != null && poslovnica.Telefon.Length > 20)
        {
            ModelState.AddModelError("Telefon", "Telefon ne smije biti duži od 20 znakova");
        }
        if (poslovnica.Email != null && poslovnica.Email.Length > 50)
        {
            ModelState.AddModelError("Email", "Email ne smije biti duži od 50 znakova");
        }
        if (poslovnica.RadnoVrijemeOd != null && poslovnica.RadnoVrijemeDo != null && poslovnica.RadnoVrijemeDo <= poslovnica.RadnoVrijemeOd)
        {
            ModelState.AddModelError("RadnoVrijemeDo", "Kraj radnog vremena mora biti poslije početka radnog vremena!");
        }
        if (poslovnica.Status != null && poslovnica.Status.Length > 10)
        {
            ModelState.AddModelError("Status", "Status ne smije biti duži od 10 znakova");
        }
    }
}


