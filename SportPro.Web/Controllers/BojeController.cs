using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
[Authorize(Roles = "Uposlenik")]
public class BojeController : Controller
{
    private readonly IBojeRepository bojeRepository;

    public BojeController(IBojeRepository bojeRepository)
    {
        this.bojeRepository = bojeRepository;
    }

    /// <summary>
    /// Prikaz svih boja
    /// </summary>
    /// <param name="searchQuery"></param>
    /// <param name="sortBy"></param>
    /// <param name="sortDirection"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Boje>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Index(string? searchQuery, string? sortBy, string? sortDirection, int pageSize = 5, int pageNumber = 1)
    {
        var totalRecords = await bojeRepository.CountAsync();
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

        var boje = await bojeRepository.GetAllAsync(searchQuery, sortBy, sortDirection, pageNumber, pageSize);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(boje);
        }

        return View(boje);
    }

    /// <summary>
    /// Dohvaćanje view-a za dodavanje nove boje
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    /// <summary>
    /// Dodavanje nove boje
    /// </summary>
    /// <param name="addBojaRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Boje), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add(AddBojaRequest addBojaRequest)
    {
        ValidateBojaForAdd(addBojaRequest);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var boja = new Boje
        {
            NazivBoje = addBojaRequest.NazivBoje
        };

        await bojeRepository.AddAsync(boja);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(boja);
        }

        return RedirectToAction("Index", new { id = addBojaRequest.IDBoja });
    }


    /// <summary>
    /// Dohvaćanje view-a za uređivanje boje
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

        var boja = await bojeRepository.GetAsync(id);

        if (boja == null)
        {
            return NotFound();
        }

        var editBojaRequest = new EditBojaRequest
        {
            IDBoja = boja.IDBoja,
            NazivBoje = boja.NazivBoje
        };

        return View(editBojaRequest);
    }

    /// <summary>
    /// Uređivanje boje
    /// </summary>
    /// <param name="editBojaRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Boje), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Edit(EditBojaRequest editBojaRequest)
    {
        var boja = new Boje
        {
            IDBoja = editBojaRequest.IDBoja,
            NazivBoje = editBojaRequest.NazivBoje
        };

        ValidateBojaForEdit(editBojaRequest);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await bojeRepository.UpdateAsync(boja);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(boja);
        }

        return RedirectToAction("Index", new { id = editBojaRequest.IDBoja });
    }

    /// <summary>
    /// Dohvaćanje view-a za brisanje boje
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

        var boja = await bojeRepository.GetAsync(id);

        if (boja == null)
        {
            return NotFound();
        }

        return View(boja);
    }

    /// <summary>
    /// Brisanje boje
    /// </summary>
    /// <param name="editBojaRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Boje), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(EditBojaRequest editBojaRequest)
    {
        var boja = await bojeRepository.DeleteAsync(editBojaRequest.IDBoja);

        if (boja == null)
        {
            return NotFound();
        }

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok("Boja je uspješno uklonjena!");
        }

        return RedirectToAction("Index", new { id = editBojaRequest.IDBoja });

    }

    private void ValidateBojaForAdd(AddBojaRequest addBojaRequest)
    {
        if (addBojaRequest.NazivBoje.Length > 20)
        {
            ModelState.AddModelError("NazivBoje", "Naziv boje ne smije biti duži od 20 znakova!");
        }
    }

    private void ValidateBojaForEdit(EditBojaRequest editBojaRequest)
    {
        if (editBojaRequest.NazivBoje != null && editBojaRequest.NazivBoje.Length > 20)
        {
            ModelState.AddModelError("NazivBoje", "Naziv boje ne smije biti duži od 20 znakova!");
        }
    }
}
