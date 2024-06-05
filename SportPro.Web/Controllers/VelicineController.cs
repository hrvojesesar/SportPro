using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
[Authorize(Roles = "Uposlenik")]
public class VelicineController : Controller
{
    private readonly IVelicineRepository _velicineRepository;

    public VelicineController(IVelicineRepository velicineRepository)
    {
        _velicineRepository = velicineRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int pageSize = 8, int pageNumber = 1)
    {
        var totalRecords = await _velicineRepository.CountAsync();
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

        var velicine = await _velicineRepository.GetAllAsync(pageNumber, pageSize);
        return View(velicine);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddVelicinaRequest addVelicinaRequest)
    {
        ValidateVelicinaForAdd(addVelicinaRequest);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var velicina = new Velicine
        {
            Velicina = addVelicinaRequest.Velicina
        };

        await _velicineRepository.AddAsync(velicina);
        return RedirectToAction("Index", new { id = velicina.IDVelicina });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var velicina = await _velicineRepository.GetAsync(id);
        if (velicina == null)
        {
            return NotFound();
        }

        var editVelicinaRequest = new EditVelicinaRequest
        {
            IDVelicina = velicina.IDVelicina,
            Velicina = velicina.Velicina
        };

        return View(editVelicinaRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditVelicinaRequest editVelicinaRequest)
    {
        var velicina = new Velicine
        {
            IDVelicina = editVelicinaRequest.IDVelicina,
            Velicina = editVelicinaRequest.Velicina
        };

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _velicineRepository.UpdateAsync(velicina);
        return RedirectToAction("Index", new { id = velicina.IDVelicina });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var velicina = await _velicineRepository.GetAsync(id);
        if (velicina == null)
        {
            return NotFound();
        }

        return View(velicina);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(EditVelicinaRequest editVelicinaRequest)
    {
        var velicina = await _velicineRepository.DeleteAsync(editVelicinaRequest.IDVelicina);

        if (velicina == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index", new { id = editVelicinaRequest.IDVelicina });
    }

    private void ValidateVelicinaForAdd(AddVelicinaRequest addVelicinaRequest)
    {
        if (addVelicinaRequest.Velicina.Length > 5)
        {
            ModelState.AddModelError("Velicina", "Veličina ne može imati više od 5 znakova!");
        }
    }
}