using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
[Authorize(Roles = "Uposlenik")]
public class NarudzbeController : Controller
{
    private readonly INarudzbeRepository _narudzbeRepository;
    private readonly IDobavljaciRepository _dobavljaciRepository;

    public NarudzbeController(INarudzbeRepository narudzbeRepository, IDobavljaciRepository dobavljaciRepository)
    {
        _narudzbeRepository = narudzbeRepository;
        _dobavljaciRepository = dobavljaciRepository;
    }

    /// <summary>
    /// Prikaz svih narudzbi
    /// </summary>
    /// <param name="naziv"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="status"></param>
    /// <param name="sortBy"></param>
    /// <param name="sortDirection"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Narudzbe>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Index(string? naziv, DateTime? startDate, DateTime? endDate, string? status, string? sortBy, string? sortDirection, int pageSize = 5, int pageNumber = 1)
    {
        var totalRecords = await _narudzbeRepository.CountAsync();
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
        ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
        ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");
        ViewBag.Status = status;

        var statusList = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "Svi" },
            new SelectListItem { Value = "Na čekanju", Text = "Na čekanju" },
            new SelectListItem { Value = "U obradi", Text = "U obradi" },
            new SelectListItem { Value = "Odbijeno", Text = "Odbijeno" },
            new SelectListItem { Value = "Završeno", Text = "Završeno" }
        };

        ViewBag.StatusList = new SelectList(statusList, "Value", "Text", status);

        ViewBag.SortBy = sortBy;
        ViewBag.SortDirection = sortDirection;

        var narudzbe = await _narudzbeRepository.GetAllAsync(naziv, startDate, endDate, status, sortBy, sortDirection, pageNumber, pageSize);
        var dobavljaci = await _dobavljaciRepository.GetAllSecAsync();

        ViewData["Dobavljaci"] = dobavljaci;

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(narudzbe);
        }

        return View(narudzbe);
    }

    /// <summary>
    /// Dohvaćanje view-a za dodavanje narudzbe
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var model = new AddNarudzbaRequest
        {
            Dobavljacis = await _dobavljaciRepository.GetAllSecAsync()
        };

        return View(model);
    }

    /// <summary>
    /// Dodavanje narudzbe
    /// </summary>
    /// <param name="addNarudzbaRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Narudzbe>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(AddNarudzbaRequest addNarudzbaRequest)
    {
        ValidateNarudzbaForAdd(addNarudzbaRequest);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        addNarudzbaRequest.UkupniTrosak = (addNarudzbaRequest.Kolicina * addNarudzbaRequest.CijenaPoKomadu) + addNarudzbaRequest.CijenaDostave + (addNarudzbaRequest.Porez ?? 0) + (addNarudzbaRequest.DodatneNaknade ?? 0);

        var narudzba = new Narudzbe
        {
            NazivArtikla = addNarudzbaRequest.NazivArtikla,
            DatumNabave = addNarudzbaRequest.DatumNabave,
            DatumIsporuke = addNarudzbaRequest.DatumIsporuke,
            Kolicina = addNarudzbaRequest.Kolicina,
            CijenaPoKomadu = addNarudzbaRequest.CijenaPoKomadu,
            CijenaDostave = addNarudzbaRequest.CijenaDostave,
            Porez = addNarudzbaRequest.Porez,
            DodatneNaknade = addNarudzbaRequest.DodatneNaknade,
            UkupniTrosak = addNarudzbaRequest.UkupniTrosak,
            Status = addNarudzbaRequest.Status,
            Napomene = addNarudzbaRequest.Napomene,
            DobavljacIDDobavljac = addNarudzbaRequest.DobavljacIDDobavljac
        };

        await _narudzbeRepository.AddAsync(narudzba);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(narudzba);
        }

        return RedirectToAction("Index", new { id = narudzba.IDNarudzba });
    }

    /// <summary>
    /// Dohvaćanje view-a za uređivanje narudzbe
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

        var narudzba = await _narudzbeRepository.GetAsync(id);

        if (narudzba == null)
        {
            return NotFound();
        }

        var model = new EditNarudzbaRequest
        {
            IDNarudzba = narudzba.IDNarudzba,
            NazivArtikla = narudzba.NazivArtikla,
            DatumNabave = narudzba.DatumNabave,
            DatumIsporuke = narudzba.DatumIsporuke,
            Kolicina = narudzba.Kolicina,
            CijenaPoKomadu = narudzba.CijenaPoKomadu,
            CijenaDostave = narudzba.CijenaDostave,
            Porez = narudzba.Porez,
            DodatneNaknade = narudzba.DodatneNaknade,
            UkupniTrosak = narudzba.UkupniTrosak,
            Status = narudzba.Status,
            Napomene = narudzba.Napomene,
            DobavljacIDDobavljac = narudzba.DobavljacIDDobavljac,
            Dobavljacis = await _dobavljaciRepository.GetAllAsync()
        };

        return View(model);
    }

    /// <summary>
    /// Uređivanje narudzbe
    /// </summary>
    /// <param name="editNarudzbaRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Narudzbe>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Edit(EditNarudzbaRequest editNarudzbaRequest)
    {

        editNarudzbaRequest.UkupniTrosak = (editNarudzbaRequest.Kolicina * editNarudzbaRequest.CijenaPoKomadu) + editNarudzbaRequest.CijenaDostave + (editNarudzbaRequest.Porez ?? 0) + (editNarudzbaRequest.DodatneNaknade ?? 0);

        var narudzba = new Narudzbe
        {
            IDNarudzba = editNarudzbaRequest.IDNarudzba,
            NazivArtikla = editNarudzbaRequest.NazivArtikla,
            DatumNabave = editNarudzbaRequest.DatumNabave,
            DatumIsporuke = editNarudzbaRequest.DatumIsporuke,
            Kolicina = editNarudzbaRequest.Kolicina,
            CijenaPoKomadu = editNarudzbaRequest.CijenaPoKomadu,
            CijenaDostave = editNarudzbaRequest.CijenaDostave,
            Porez = editNarudzbaRequest.Porez,
            DodatneNaknade = editNarudzbaRequest.DodatneNaknade,
            UkupniTrosak = editNarudzbaRequest.UkupniTrosak,
            Status = editNarudzbaRequest.Status,
            Napomene = editNarudzbaRequest.Napomene,
            DobavljacIDDobavljac = editNarudzbaRequest.DobavljacIDDobavljac
        };

        ValidateNarudzbaForEdit(narudzba);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }


        await _narudzbeRepository.UpdateAsync(narudzba);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(narudzba);
        }

        return RedirectToAction("Index", new { id = narudzba.IDNarudzba });
    }

    /// <summary>
    /// Dohvaćanje view-a za brisanje narudzbe
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

        var narudzba = await _narudzbeRepository.GetAsync(id);
        var dobavljaci = await _dobavljaciRepository.GetAllAsync();

        ViewData["Dobavljaci"] = dobavljaci;

        if (narudzba == null)
        {
            return NotFound();
        }

        return View(narudzba);
    }

    /// <summary>
    /// Brisanje narudzbe
    /// </summary>
    /// <param name="editNarudzbaRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Narudzbe>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(EditNarudzbaRequest editNarudzbaRequest)
    {
        var narudzba = await _narudzbeRepository.DeleteAsync(editNarudzbaRequest.IDNarudzba);

        if (narudzba == null)
        {
            return NotFound();
        }

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok("Narudžba je uspješno uklonjena!");
        }

        return RedirectToAction("Index", new { id = editNarudzbaRequest.IDNarudzba });

    }

    private void ValidateNarudzbaForAdd(AddNarudzbaRequest addNarudzbaRequest)
    {
        if (addNarudzbaRequest.DatumNabave >= addNarudzbaRequest.DatumIsporuke)
        {
            ModelState.AddModelError("DatumIsporuke", "Datum isporuke ne može biti prije datuma nabave.");
        }
    }

    private void ValidateNarudzbaForEdit(Narudzbe narudzba)
    {
        if (narudzba.DatumNabave >= narudzba.DatumIsporuke)
        {
            ModelState.AddModelError("DatumIsporuke", "Datum isporuke ne može biti prije datuma nabave.");
        }
    }
}
