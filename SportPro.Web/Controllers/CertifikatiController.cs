using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;


[Route("[controller]/[action]")]
[Authorize(Roles = "Uposlenik")]
public class CertifikatiController : Controller
{
    private readonly ICertifikatiRepository certifikatiRepository;

    public CertifikatiController(ICertifikatiRepository certifikatiRepository)
    {
        this.certifikatiRepository = certifikatiRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var certifikati = await certifikatiRepository.GetAllAsync();
        return View(certifikati);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    [HttpPost]
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
        return RedirectToAction("Index", new { id = certifikat.IDCertifikat });

    }

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

    [HttpPost]
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
        return RedirectToAction("Index", new { id = certifikat.IDCertifikat });
    }
}
