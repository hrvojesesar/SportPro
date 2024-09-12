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

    /// <summary>
    /// Prikaz svih veličina
    /// </summary>
    /// <param name="searchQuery"></param>
    /// <param name="sortBy"></param>
    /// <param name="sortDirection"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Velicine>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Index(string? searchQuery, string? sortBy, string? sortDirection, int pageSize = 5, int pageNumber = 1)
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


        ViewBag.SearchQuery = searchQuery;
        ViewBag.SortBy = sortBy;
        ViewBag.SortDirection = sortDirection;

        ViewBag.PageSize = pageSize;
        ViewBag.PageNumber = pageNumber;

        var velicine = await _velicineRepository.GetAllAsync(searchQuery, sortBy, sortDirection, pageNumber, pageSize);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(velicine);
        }

        return View(velicine);
    }

    /// <summary>
    /// Dohvaćanje view-a za dodavanje nove veličine
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    /// <summary>
    /// Dodavanje nove veličine
    /// </summary>
    /// <param name="addVelicinaRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Velicine>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(velicina);
        }

        return RedirectToAction("Index", new { id = velicina.IDVelicina });
    }

    /// <summary>
    /// Dohvaćanje view-a za uređivanje veličine
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

    /// <summary>
    /// Uređivanje veličine
    /// </summary>
    /// <param name="editVelicinaRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Velicine>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(velicina);
        }

        return RedirectToAction("Index", new { id = velicina.IDVelicina });
    }

    /// <summary>
    /// Dohvaćanje view-a za brisanje veličine
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

        var velicina = await _velicineRepository.GetAsync(id);
        if (velicina == null)
        {
            return NotFound();
        }

        return View(velicina);
    }

    /// <summary>
    /// Brisanje veličine
    /// </summary>
    /// <param name="editVelicinaRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Velicine>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(EditVelicinaRequest editVelicinaRequest)
    {
        var velicina = await _velicineRepository.DeleteAsync(editVelicinaRequest.IDVelicina);

        if (velicina == null)
        {
            return NotFound();
        }

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok("Veličina je uspješno uklonjena!");
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