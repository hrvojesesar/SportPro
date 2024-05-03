using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
public class VrstePlacanjaController : Controller
{
    private readonly IVrstePlacanjaRepository _vrstePlacanjaRepository;

    public VrstePlacanjaController(IVrstePlacanjaRepository vrstePlacanjaRepository)
    {
        _vrstePlacanjaRepository = vrstePlacanjaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var vrstePlacanja = await _vrstePlacanjaRepository.GetAllAsync();
        return View(vrstePlacanja);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddVrstaPlacanjaRequest addVrstaPlacanjaRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var vrstaPlacanja = new VrstePlacanja
        {
            Naziv = addVrstaPlacanjaRequest.Naziv,
            Opis = addVrstaPlacanjaRequest.Opis,
            Aktivno = addVrstaPlacanjaRequest.Aktivno
        };

        await _vrstePlacanjaRepository.AddAsync(vrstaPlacanja);
        return RedirectToAction("Index", new { id = vrstaPlacanja.IDVrstaPlacanja });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vrstaPlacanja = await _vrstePlacanjaRepository.GetAsync(id);
        if (vrstaPlacanja == null)
        {
            return NotFound();
        }

        var editVrstaPlacanjaRequest = new EditVrstaPlacanjaRequest
        {
            IDVrstaPlacanja = vrstaPlacanja.IDVrstaPlacanja,
            Naziv = vrstaPlacanja.Naziv,
            Opis = vrstaPlacanja.Opis,
            Aktivno = vrstaPlacanja.Aktivno
        };

        return View(editVrstaPlacanjaRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditVrstaPlacanjaRequest editVrstaPlacanjaRequest)
    {


        var vrstaPlacanja = new VrstePlacanja
        {
            IDVrstaPlacanja = editVrstaPlacanjaRequest.IDVrstaPlacanja,
            Naziv = editVrstaPlacanjaRequest.Naziv,
            Opis = editVrstaPlacanjaRequest.Opis,
            Aktivno = editVrstaPlacanjaRequest.Aktivno
        };

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _vrstePlacanjaRepository.UpdateAsync(vrstaPlacanja);
        return RedirectToAction("Index", new { id = vrstaPlacanja.IDVrstaPlacanja });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vrstaPlacanja = await _vrstePlacanjaRepository.GetAsync(id);
        if (vrstaPlacanja == null)
        {
            return NotFound();
        }

        return View(vrstaPlacanja);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(EditVrstaPlacanjaRequest editVrstaPlacanjaRequest)
    {
        var vrstaPlacanja = await _vrstePlacanjaRepository.DeleteAsync(editVrstaPlacanjaRequest.IDVrstaPlacanja);

        if (vrstaPlacanja == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index", new { id = editVrstaPlacanjaRequest.IDVrstaPlacanja });

    }
}
