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
        ValidatePonudaPoslovaForAdd(addPonudaPoslovaRequest);

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
        return RedirectToAction("Index", new { ponudaPoslova.IDPonudaPoslova });
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

        ValidatePonudaPoslovaForEdit(ponudaPoslova);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await ponudePoslovaRepository.UpdateAsync(ponudaPoslova);
        return RedirectToAction("Index", new { id = editPonudaPoslovaRequest.IDPonudaPoslova });
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
    public async Task<IActionResult> Delete(EditPonudaPoslovaRequest editPonudaPoslovaRequest)
    {
        var ponudaPoslova = await ponudePoslovaRepository.DeleteAsync(editPonudaPoslovaRequest.IDPonudaPoslova);

        if (ponudaPoslova == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index", new { id = editPonudaPoslovaRequest.IDPonudaPoslova });
        
    }

    private void ValidatePonudaPoslovaForAdd(AddPonudaPoslovaRequest addPonudaPoslovaRequest)
    {
        if (addPonudaPoslovaRequest.PocetakRadova >= addPonudaPoslovaRequest.KrajRadova)
        {
            ModelState.AddModelError("PocetakRadova", "Datum početka mora biti prije datuma kraja radova!");
        }
        if (addPonudaPoslovaRequest.BrojOsoba <= 0)
        {
            ModelState.AddModelError("BrojOsoba", "Broj osoba mora biti veći od 0!");
        }
        if (addPonudaPoslovaRequest.Naziv.Length > 200)
        {
            ModelState.AddModelError("Naziv", "Naziv ne može biti duži od 200 znakova!");
        }
        if (addPonudaPoslovaRequest.Lokacija.Length > 100)
        {
            ModelState.AddModelError("Lokacija", "Lokacija ne može biti duža od 100 znakova!");
        }
        if (addPonudaPoslovaRequest.PotrebnaOprema != null && addPonudaPoslovaRequest.PotrebnaOprema.Length > 200)
        {
            ModelState.AddModelError("PotrebnaOprema", "Potrebna oprema ne može biti duža od 200 znakova!");
        }
    }

    private void ValidatePonudaPoslovaForEdit(PonudePoslova ponudaPoslova)
    {
        if (ponudaPoslova.PocetakRadova >= ponudaPoslova.KrajRadova)
        {
            ModelState.AddModelError("PocetakRadova", "Datum početka mora biti prije datuma kraja radova!");
        }
        if (ponudaPoslova.BrojOsoba != null && ponudaPoslova.BrojOsoba <= 0)
        {
            ModelState.AddModelError("BrojOsoba", "Broj osoba mora biti veći od 0!");
        }
        if (ponudaPoslova.Naziv != null && ponudaPoslova.Naziv.Length > 200)
        {
            ModelState.AddModelError("Naziv", "Naziv ne može biti duži od 200 znakova!");
        }
        if (ponudaPoslova.Lokacija != null && ponudaPoslova.Lokacija.Length > 100)
        {
            ModelState.AddModelError("Lokacija", "Lokacija ne može biti duža od 100 znakova!");
        }
        if (ponudaPoslova.PotrebnaOprema != null && ponudaPoslova.PotrebnaOprema.Length > 200)
        {
            ModelState.AddModelError("PotrebnaOprema", "Potrebna oprema ne može biti duža od 200 znakova!");
        }
    }
}
