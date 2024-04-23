using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

public class RasporedController : Controller
{
    private readonly IRasporedRepository rasporedRepository;

    public RasporedController(IRasporedRepository rasporedRepository)
    {
        this.rasporedRepository = rasporedRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var raspored = await rasporedRepository.GetAllAsync();
        return View(raspored);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddRasporedRequest addRasporedRequest)
    {
        ValidateRasporedForAdd(addRasporedRequest);

        if (!ModelState.IsValid)
        {
            return View();
        }
        var raspored = new Raspored
        {
            Zaposlenik = addRasporedRequest.Zaposlenik,
            Posao = addRasporedRequest.Posao,
            Napomena = addRasporedRequest.Napomena,
            PocetakRada = addRasporedRequest.PocetakRada,
            KrajRada = addRasporedRequest.KrajRada,
            BrojSati = addRasporedRequest.BrojSati,
        };

        await rasporedRepository.AddAsync(raspored);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var raspored = await rasporedRepository.GetAsync(id);
        if (raspored == null)
        {
            return NotFound();
        }

        var editRasporedRequest = new EditRasporedRequest
        {
            IDRaspored = raspored.IDRaspored,
            Zaposlenik = raspored.Zaposlenik,
            Posao = raspored.Posao,
            Napomena = raspored.Napomena,
            PocetakRada = raspored.PocetakRada,
            KrajRada = raspored.KrajRada,
            BrojSati = raspored.BrojSati,
        };

        return View(editRasporedRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditRasporedRequest editRasporedRequest)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var raspored = new Raspored
        {
            IDRaspored = editRasporedRequest.IDRaspored,
            Zaposlenik = editRasporedRequest.Zaposlenik,
            Posao = editRasporedRequest.Posao,
            Napomena = editRasporedRequest.Napomena,
            PocetakRada = editRasporedRequest.PocetakRada,
            KrajRada = editRasporedRequest.KrajRada,
            BrojSati = editRasporedRequest.BrojSati,
        };

        ValidateRasporedForEdit(raspored);

        if (!ModelState.IsValid)
        {
            return View();
        }

        await rasporedRepository.UpdateAsync(raspored);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var raspored = await rasporedRepository.GetAsync(id);
        if (raspored == null)
        {
            return NotFound();
        }

        return View(raspored);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var raspored = await rasporedRepository.DeleteAsync(id);
        if (raspored == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index");
    }
  

    private void ValidateRasporedForAdd(AddRasporedRequest addRasporedRequest)
    {
        if (addRasporedRequest.PocetakRada >= addRasporedRequest.KrajRada)
        {
            ModelState.AddModelError("PocetakRada", "PocetakRada has to be before KrajRada!");
        }
    }

    private void ValidateRasporedForEdit(Raspored raspored)
    {
        if (raspored.PocetakRada >= raspored.KrajRada)
        {
            ModelState.AddModelError("PocetakRada", "PocetakRada has to be before KrajRada!");
        }
    }
}
