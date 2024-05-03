using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
public class BojeController : Controller
{
    private readonly IBojeRepository bojeRepository;

    public BojeController(IBojeRepository bojeRepository)
    {
        this.bojeRepository = bojeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var boje = await bojeRepository.GetAllAsync();
        return View(boje);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
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
        return RedirectToAction("Index", new { id = addBojaRequest.IDBoja });
    }

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

    [HttpPost]
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
