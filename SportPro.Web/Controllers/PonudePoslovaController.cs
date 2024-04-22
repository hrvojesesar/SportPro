using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

public class PonudePoslovaController : Controller
{
    private readonly IPonudePoslovaRepository ponudePoslovaRepository;

    public PonudePoslovaController(IPonudePoslovaRepository ponudePoslovaRepository)
    {
        this.ponudePoslovaRepository = ponudePoslovaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var ponudePoslova = await ponudePoslovaRepository.GetAllAsync();
        return View(ponudePoslova);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddPonudaPoslovaRequest addPonudaPoslovaRequest)
    {

        if (!ModelState.IsValid)
        {
            return View();
        }
        var ponudaPoslova = new PonudePoslova
        {
            Naziv = addPonudaPoslovaRequest.Naziv,
            OpisPosla = addPonudaPoslovaRequest.OpisPosla,
            BrojOsoba = addPonudaPoslovaRequest.BrojOsoba,
            BrojSati = addPonudaPoslovaRequest.BrojSati,
            CijenaSata = addPonudaPoslovaRequest.CijenaSata,
            PotrebnaOprema = addPonudaPoslovaRequest.PotrebnaOprema,
            PocetakRadova = addPonudaPoslovaRequest.PocetakRadova,
            KrajRadova = addPonudaPoslovaRequest.KrajRadova,
            Lokacija = addPonudaPoslovaRequest.Lokacija
        };

        await ponudePoslovaRepository.AddAsync(ponudaPoslova);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ponudaPoslova = await ponudePoslovaRepository.GetAsync(id);
        if (ponudaPoslova == null)
        {
            return NotFound();
        }

        var editPonudaPoslovaRequest = new EditPonudaPoslovaRequest
        {
            IDPonudaPoslova = ponudaPoslova.IDPonudaPoslova,
            Naziv = ponudaPoslova.Naziv,
            OpisPosla = ponudaPoslova.OpisPosla,
            BrojOsoba = ponudaPoslova.BrojOsoba,
            BrojSati = ponudaPoslova.BrojSati,
            CijenaSata = ponudaPoslova.CijenaSata,
            PotrebnaOprema = ponudaPoslova.PotrebnaOprema,
            PocetakRadova = ponudaPoslova.PocetakRadova,
            KrajRadova = ponudaPoslova.KrajRadova,
            Lokacija = ponudaPoslova.Lokacija
        };

        return View(editPonudaPoslovaRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditPonudaPoslovaRequest editPonudaPoslovaRequest)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var ponudaPoslova = new PonudePoslova
        {
            IDPonudaPoslova = editPonudaPoslovaRequest.IDPonudaPoslova,
            Naziv = editPonudaPoslovaRequest.Naziv,
            OpisPosla = editPonudaPoslovaRequest.OpisPosla,
            BrojOsoba = editPonudaPoslovaRequest.BrojOsoba,
            BrojSati = editPonudaPoslovaRequest.BrojSati,
            CijenaSata = editPonudaPoslovaRequest.CijenaSata,
            PotrebnaOprema = editPonudaPoslovaRequest.PotrebnaOprema,
            PocetakRadova = editPonudaPoslovaRequest.PocetakRadova,
            KrajRadova = editPonudaPoslovaRequest.KrajRadova,
            Lokacija = editPonudaPoslovaRequest.Lokacija
        };

        await ponudePoslovaRepository.UpdateAsync(ponudaPoslova);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ponudaPoslova = await ponudePoslovaRepository.GetAsync(id);
        if (ponudaPoslova == null)
        {
            return NotFound();
        }

        return View(ponudaPoslova);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await ponudePoslovaRepository.DeleteAsync(id);
        return RedirectToAction("Index");
    }

   
}
