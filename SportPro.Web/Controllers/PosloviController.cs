using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

public class PosloviController : Controller
{
    private readonly IPosloviRepository _posloviRepository;

    public PosloviController(IPosloviRepository posloviRepository)
    {
        _posloviRepository = posloviRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var poslovi = await _posloviRepository.GetAllAsync();
        return View(poslovi);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddPosaoRequest addPosaoRequest)
    {
        ValidatePosloviForAdd(addPosaoRequest);

        if (!ModelState.IsValid)
        {
            return View();
        }
        var posao = new Poslovi
        {
            Naziv = addPosaoRequest.Naziv,
            OpisPosla = addPosaoRequest.OpisPosla,
            BrojOsoba = addPosaoRequest.BrojOsoba,
            BrojSati = addPosaoRequest.BrojSati,
            CijenaSata = addPosaoRequest.CijenaSata,
            PotrebnaOprema = addPosaoRequest.PotrebnaOprema,
            PocetakRadova = addPosaoRequest.PocetakRadova,
            KrajRadova = addPosaoRequest.KrajRadova  ,
            Trosak = addPosaoRequest.Trosak,
            Profit = addPosaoRequest.Profit
        };

        await _posloviRepository.AddAsync(posao);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var posao = await _posloviRepository.GetAsync(id);
        if (posao == null)
        {
            return NotFound();
        }


        var editPosaoRequest = new EditPosaoRequest
        {
            IDPosla = posao.IDPosla,
            Naziv = posao.Naziv,
            OpisPosla = posao.OpisPosla,
            BrojOsoba = posao.BrojOsoba,
            BrojSati = posao.BrojSati,
            CijenaSata = posao.CijenaSata,
            PotrebnaOprema = posao.PotrebnaOprema,
            PocetakRadova = posao.PocetakRadova,
            KrajRadova = posao.KrajRadova,
            Trosak = posao.Trosak,
            Profit = posao.Profit
        };

        return View(editPosaoRequest);
     
    }


    [HttpPost]
    public async Task<IActionResult> Edit(EditPosaoRequest editPosaoRequest)
    {

        var posao = new Poslovi
        {
            IDPosla = editPosaoRequest.IDPosla,
            Naziv = editPosaoRequest.Naziv,
            OpisPosla = editPosaoRequest.OpisPosla,
            BrojOsoba = editPosaoRequest.BrojOsoba,
            BrojSati = editPosaoRequest.BrojSati,
            CijenaSata = editPosaoRequest.CijenaSata,
            PotrebnaOprema = editPosaoRequest.PotrebnaOprema,
            PocetakRadova = editPosaoRequest.PocetakRadova,
            KrajRadova = editPosaoRequest.KrajRadova,
            Trosak = editPosaoRequest.Trosak,
            Profit = editPosaoRequest.Profit
        };

        ValidatePosloviForEdit(posao);

        if (!ModelState.IsValid)
        {
            return View();
        }

        await _posloviRepository.UpdateAsync(posao);
        return RedirectToAction("Index");
    }


    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var posao = await _posloviRepository.GetAsync(id);
        if (posao == null)
        {
            return NotFound();
        }

        return View(posao);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _posloviRepository.DeleteAsync(id);
        return RedirectToAction("Index");
    }
   

    private void ValidatePosloviForAdd(AddPosaoRequest addPosaoRequest)
    {
        if (addPosaoRequest.PocetakRadova >= addPosaoRequest.KrajRadova)
        {
            ModelState.AddModelError("PocetakRadova", "PocetakRadova has to be before KrajRadova!");
        }
    }

    private void ValidatePosloviForEdit(Poslovi posao)
    {
        if (posao.PocetakRadova >= posao.KrajRadova)
        {
            ModelState.AddModelError("PocetakRadova", "PocetakRadova has to be before KrajRadova!");
        }
    }
}
