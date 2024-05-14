using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
public class ArtikliController : Controller
{
    private readonly IArtikliRepository artikliRepository;
    private readonly IDobavljaciRepository dobavljaciRepository;
    private readonly IBrendoviRepository brendoviRepository;
    private readonly IKategorijeRepository kategorijeRepository;
    private readonly IBojeRepository bojeRepository;
    private readonly IVelicineRepository velicineRepository;
    private readonly IPoslovniceRepository poslovniceRepository;
    private readonly ApplicationDbContext applicationDbContext;

    public ArtikliController(IArtikliRepository artikliRepository, IDobavljaciRepository dobavljaciRepository, IBrendoviRepository brendoviRepository, IKategorijeRepository kategorijeRepository, IBojeRepository bojeRepository, IVelicineRepository velicineRepository, IPoslovniceRepository poslovniceRepository, ApplicationDbContext applicationDbContext)
    {
        this.artikliRepository = artikliRepository;
        this.dobavljaciRepository = dobavljaciRepository;
        this.brendoviRepository = brendoviRepository;
        this.kategorijeRepository = kategorijeRepository;
        this.bojeRepository = bojeRepository;
        this.velicineRepository = velicineRepository;
        this.poslovniceRepository = poslovniceRepository;
        this.applicationDbContext = applicationDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var brend = await brendoviRepository.GetAllAsync();
        // var dobavljaci = await dobavljaciRepository.GetAllAsync();
        var dobavljaci = await dobavljaciRepository.GetActiveDobavljaci();

        ViewData["Brendovi"] = brend;
        ViewData["Dobavljaci"] = dobavljaci;

        var artikli = await artikliRepository.GetAllAsync();
        return View(artikli);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var kategorije = await kategorijeRepository.GetAllAsync();
        var boje = await bojeRepository.GetAllAsync();
        var velicine = await velicineRepository.GetAllAsync();
        var poslovnice = await poslovniceRepository.GetAllAsync();


        var model = new AddArtiklRequest
        {
            // Dobavljacis = applicationDbContext.Dobavljaci.ToList(),
            Dobavljacis = applicationDbContext.Dobavljaci.Where(d => d.SuradnjaAktivna == "Da").ToList(),
            Brendovis = applicationDbContext.Brendovi.ToList(),
            Kategorije = kategorije.Select(k => new SelectListItem
            {
                Text = k.Naziv,
                Value = k.IDKategorija.ToString()
            }),
            Boje = boje.Select(b => new SelectListItem
            {
                Text = b.NazivBoje,
                Value = b.IDBoja.ToString()
            }),
            Velicine = velicine.Select(v => new SelectListItem
            {
                Text = v.Velicina,
                Value = v.IDVelicina.ToString()
            }),
            Poslovnice = poslovnice.Select(p => new SelectListItem
            {
                Text = p.Naziv,
                Value = p.IDPoslovnica.ToString()
            })
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddArtiklRequest addArtiklRequest)
    {
        var artikl = new Artikli
        {
            Naziv = addArtiklRequest.Naziv,
            Opis = addArtiklRequest.Opis,
            Cijena = addArtiklRequest.Cijena,
            Snizen = addArtiklRequest.Snizen,
            SnizenaCijena = addArtiklRequest.SnizenaCijena,
            NabavnaKolicina = addArtiklRequest.NabavnaKolicina,
            TrenutnaKolicina = addArtiklRequest.TrenutnaKolicina,
            NaStanju = addArtiklRequest.NaStanju,
            DatumNabave = addArtiklRequest.DatumNabave,
            NabavnaCijena = addArtiklRequest.NabavnaCijena,
            CijenaDostave = addArtiklRequest.CijenaDostave,
            UkupniTrosak = addArtiklRequest.CijenaDostave + (addArtiklRequest.NabavnaCijena * addArtiklRequest.NabavnaKolicina),
            DobavljacIDDobavljac = addArtiklRequest.DobavljacIDDobavljac,
            BrendIDBrend = addArtiklRequest.BrendIDBrend,
        };

        var selectedKategorije = new List<Kategorije>();
        foreach (var kategorija in addArtiklRequest.SelectedKategorije)
        {
            var selectedKategorijaID = int.Parse(kategorija);
            var existingKategorija = await kategorijeRepository.GetAsync(selectedKategorijaID);

            if (existingKategorija != null)
            {
                selectedKategorije.Add(existingKategorija);
            }
        }

        artikl.Kategorije = selectedKategorije;


        var selectedBoje = new List<Boje>();
        foreach (var boja in addArtiklRequest.SelectedBoje)
        {
            var selectedBojaID = int.Parse(boja);
            var existingBoja = await bojeRepository.GetAsync(selectedBojaID);

            if (existingBoja != null)
            {
                selectedBoje.Add(existingBoja);
            }
        }

        artikl.Boje = selectedBoje;



        var selectedVelicine = new List<Velicine>();
        foreach (var velicina in addArtiklRequest.SelectedVelicine)
        {
            var selectedVelicinaID = int.Parse(velicina);
            var existingVelicina = await velicineRepository.GetAsync(selectedVelicinaID);

            if (existingVelicina != null)
            {
                selectedVelicine.Add(existingVelicina);
            }
        }

        artikl.Velicine = selectedVelicine;


        var selectedPoslovnice = new List<Poslovnice>();
        foreach (var poslovnica in addArtiklRequest.SelectedPoslovnice)
        {
            var selectedPoslovnicaID = int.Parse(poslovnica);
            var existingPoslovnica = await poslovniceRepository.GetAsync(selectedPoslovnicaID);

            if (existingPoslovnica != null)
            {
                selectedPoslovnice.Add(existingPoslovnica);
            }
        }

        artikl.Poslovnice = selectedPoslovnice;

        await artikliRepository.AddAsync(artikl);
        return RedirectToAction("Index", new { id = artikl.IDArtikal });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var artikl = await artikliRepository.GetAsync(id);

        if (artikl == null)
        {
            return NotFound();
        }

        var kategorije = await kategorijeRepository.GetAllAsync();
        var boje = await bojeRepository.GetAllAsync();
        var velicine = await velicineRepository.GetAllAsync();
        var poslovnice = await poslovniceRepository.GetAllAsync();

        var model = new EditArtiklRequest
        {
            IDArtikal = artikl.IDArtikal,
            Naziv = artikl.Naziv,
            Opis = artikl.Opis,
            Cijena = artikl.Cijena,
            Snizen = artikl.Snizen,
            SnizenaCijena = artikl.SnizenaCijena,
            NabavnaKolicina = artikl.NabavnaKolicina,
            TrenutnaKolicina = artikl.TrenutnaKolicina,
            NaStanju = artikl.NaStanju,
            DatumNabave = artikl.DatumNabave,
            NabavnaCijena = artikl.NabavnaCijena,
            CijenaDostave = artikl.CijenaDostave,
            UkupniTrosak = artikl.UkupniTrosak,
            DobavljacIDDobavljac = artikl.DobavljacIDDobavljac,
            BrendIDBrend = artikl.BrendIDBrend,
            // Dobavljacis = applicationDbContext.Dobavljaci.ToList(),
            Dobavljacis = applicationDbContext.Dobavljaci.Where(d => d.SuradnjaAktivna == "Da").ToList(),
            Brendovis = applicationDbContext.Brendovi.ToList(),
            Kategorije = kategorije.Select(k => new SelectListItem
            {
                Text = k.Naziv,
                Value = k.IDKategorija.ToString()
            }),
            Boje = boje.Select(b => new SelectListItem
            {
                Text = b.NazivBoje,
                Value = b.IDBoja.ToString()
            }),
            Velicine = velicine.Select(v => new SelectListItem
            {
                Text = v.Velicina,
                Value = v.IDVelicina.ToString()
            }),
            Poslovnice = poslovnice.Select(p => new SelectListItem
            {
                Text = p.Naziv,
                Value = p.IDPoslovnica.ToString()
            }),
            SelectedKategorije = artikl.Kategorije.Select(k => k.IDKategorija.ToString()).ToArray(),
            SelectedBoje = artikl.Boje.Select(b => b.IDBoja.ToString()).ToArray(),
            SelectedVelicine = artikl.Velicine.Select(v => v.IDVelicina.ToString()).ToArray(),
            SelectedPoslovnice = artikl.Poslovnice.Select(p => p.IDPoslovnica.ToString()).ToArray()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditArtiklRequest editArtiklRequest)
    {
        var artikl = new Artikli
        {
            IDArtikal = editArtiklRequest.IDArtikal,
            Naziv = editArtiklRequest.Naziv,
            Opis = editArtiklRequest.Opis,
            Cijena = editArtiklRequest.Cijena,
            Snizen = editArtiklRequest.Snizen,
            SnizenaCijena = editArtiklRequest.SnizenaCijena,
            NabavnaKolicina = editArtiklRequest.NabavnaKolicina,
            TrenutnaKolicina = editArtiklRequest.TrenutnaKolicina,
            NaStanju = editArtiklRequest.NaStanju,
            DatumNabave = editArtiklRequest.DatumNabave,
            NabavnaCijena = editArtiklRequest.NabavnaCijena,
            CijenaDostave = editArtiklRequest.CijenaDostave,
            UkupniTrosak = editArtiklRequest.CijenaDostave + (editArtiklRequest.NabavnaCijena * editArtiklRequest.NabavnaKolicina),
            DobavljacIDDobavljac = editArtiklRequest.DobavljacIDDobavljac,
            BrendIDBrend = editArtiklRequest.BrendIDBrend
        };

        var selectedKategorije = new List<Kategorije>();
        foreach (var kategorija in editArtiklRequest.SelectedKategorije)
        {
            var selectedKategorijaID = int.Parse(kategorija);
            var existingKategorija = await kategorijeRepository.GetAsync(selectedKategorijaID);

            if (existingKategorija != null)
            {
                selectedKategorije.Add(existingKategorija);
            }
        }

        artikl.Kategorije = selectedKategorije;


        var selectedBoje = new List<Boje>();
        foreach (var boja in editArtiklRequest.SelectedBoje)
        {
            var selectedBojaID = int.Parse(boja);
            var existingBoja = await bojeRepository.GetAsync(selectedBojaID);

            if (existingBoja != null)
            {
                selectedBoje.Add(existingBoja);
            }
        }

        artikl.Boje = selectedBoje;


        var selectedVelicine = new List<Velicine>();
        foreach (var velicina in editArtiklRequest.SelectedVelicine)
        {
            var selectedVelicinaID = int.Parse(velicina);
            var existingVelicina = await velicineRepository.GetAsync(selectedVelicinaID);

            if (existingVelicina != null)
            {
                selectedVelicine.Add(existingVelicina);
            }
        }

        artikl.Velicine = selectedVelicine;


        var selectedPoslovnice = new List<Poslovnice>();
        foreach (var poslovnica in editArtiklRequest.SelectedPoslovnice)
        {
            var selectedPoslovnicaID = int.Parse(poslovnica);
            var existingPoslovnica = await poslovniceRepository.GetAsync(selectedPoslovnicaID);

            if (existingPoslovnica != null)
            {
                selectedPoslovnice.Add(existingPoslovnica);
            }
        }

        artikl.Poslovnice = selectedPoslovnice;

        await artikliRepository.UpdateAsync(artikl);
        return RedirectToAction("Index", new { id = artikl.IDArtikal });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var artikl = await artikliRepository.GetAsync(id);

        if (artikl == null)
        {
            return NotFound();
        }

        // var dobavljaci = await dobavljaciRepository.GetAllAsync();
        var dobavljaci = await dobavljaciRepository.GetActiveDobavljaci();
        var brendovi = await brendoviRepository.GetAllAsync();

        ViewData["Dobavljaci"] = dobavljaci;
        ViewData["Brendovi"] = brendovi;

        return View(artikl);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(EditArtiklRequest editArtiklRequest)
    {
        var artikl = await artikliRepository.DeleteAsync(editArtiklRequest.IDArtikal);

        if (artikl == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index", new { id = editArtiklRequest.IDArtikal });
    }
}