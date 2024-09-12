using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;


[Route("[controller]/[action]")]
[ApiController]
[Authorize(Roles = "Uposlenik")]
public class CertifikatiController : Controller
{
    private readonly ICertifikatiRepository certifikatiRepository;

    public CertifikatiController(ICertifikatiRepository certifikatiRepository)
    {
        this.certifikatiRepository = certifikatiRepository;
    }

    /// <summary>
    /// Prikaz svih certifikata
    /// </summary>
    /// <param name="searchQuery"></param>
    /// <param name="sortBy"></param>
    /// <param name="sortDirection"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Certifikati>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Index(string? searchQuery, string? sortBy, string? sortDirection, int pageSize = 5, int pageNumber = 1)
    {
        var totalRecords = await certifikatiRepository.CountAsync();
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

        ViewBag.SearchQuery = searchQuery;

        ViewBag.SortBy = sortBy;
        ViewBag.SortDirection = sortDirection;

        var certifikati = await certifikatiRepository.GetAllAsync(searchQuery, sortBy, sortDirection, pageNumber, pageSize);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(certifikati);
        }

        return View(certifikati);
    }

    /// <summary>
    /// Dohvaćanje view-a za dodavanje certifikata
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    /// <summary>
    /// Dodavanje certifikata
    /// </summary>
    /// <param name="addCertifikatRequest"></param>
    /// <param name="slike"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Certifikati>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(AddCertifikatRequest addCertifikatRequest, IEnumerable<IFormFile> slike)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var certifikat = new Certifikati
        {
            Naziv = addCertifikatRequest.Naziv,
            Opis = addCertifikatRequest.Opis,
            DatumDodjele = addCertifikatRequest.DatumDodjele,
            Organizacija = addCertifikatRequest.Organizacija,
            Slika = addCertifikatRequest.Slika
        };

        await certifikatiRepository.AddAsync(certifikat);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(certifikat);
        }

        return RedirectToAction("Index", new { id = certifikat.IDCertifikat });

    }

    /// <summary>
    /// Dohvaćanje view-a za uređivanje certifikata
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

        var certifikat = await certifikatiRepository.GetAsync(id);

        if (certifikat == null)
        {
            return NotFound();
        }

        var model = new EditCertifikatRequest
        {
            IDCertifikat = certifikat.IDCertifikat,
            Naziv = certifikat.Naziv,
            Opis = certifikat.Opis,
            DatumDodjele = certifikat.DatumDodjele,
            Organizacija = certifikat.Organizacija,
            Slika = certifikat.Slika
        };

        return View(model);
    }

    /// <summary>
    /// Uređivanje certifikata
    /// </summary>
    /// <param name="editCertifikatRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Certifikati>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Edit(EditCertifikatRequest editCertifikatRequest)
    {
        var certifikat = new Certifikati
        {
            IDCertifikat = editCertifikatRequest.IDCertifikat,
            Naziv = editCertifikatRequest.Naziv,
            Opis = editCertifikatRequest.Opis,
            DatumDodjele = editCertifikatRequest.DatumDodjele,
            Organizacija = editCertifikatRequest.Organizacija,
            Slika = editCertifikatRequest.Slika
        };

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await certifikatiRepository.UpdateAsync(certifikat);

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok(certifikat);
        }

        return RedirectToAction("Index", new { id = certifikat.IDCertifikat });
    }
}
