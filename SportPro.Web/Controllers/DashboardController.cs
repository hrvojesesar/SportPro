using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SportPro.Web.Controllers;

[Authorize(Roles = "Vlasnik")]
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> MjesecniPrihodi(int? godina)
    {
        var trenutnaGodina = godina ?? DateTime.Now.Year;

        // Grupiraj prihode po mjesecima
        var prihodiPoMjesecima = await _context.Prihodi
            .Where(p => p.Godina == trenutnaGodina)
            .GroupBy(p => new { p.Mjesec, p.KategorijePrihoda.Naziv })
            .Select(g => new
            {
                Mjesec = g.Key.Mjesec,
                Kategorija = g.Key.Naziv,
                UkupniPrihod = g.Sum(p => p.Iznos)
            })
            .ToListAsync();

        // Ukupni prihod za godinu
        var ukupniPrihodGodina = prihodiPoMjesecima.Sum(p => p.UkupniPrihod);

        // Prosječni prihod po mjesecu
        var prosjecniPrihod = ukupniPrihodGodina / 12;

        // Pripremi podatke za prikaz
        var prihodiData = new decimal[12];
        var prihodiPoKategorijama = new Dictionary<string, decimal[]>();

        foreach (var prihod in prihodiPoMjesecima)
        {
            if (prihod.Mjesec.HasValue)
            {
                // Ukupni prihodi po mjesecima
                prihodiData[prihod.Mjesec.Value - 1] += (decimal)prihod.UkupniPrihod;

                // Prihodi po kategorijama
                if (!prihodiPoKategorijama.ContainsKey(prihod.Kategorija))
                {
                    prihodiPoKategorijama[prihod.Kategorija] = new decimal[12];
                }
                prihodiPoKategorijama[prihod.Kategorija][prihod.Mjesec.Value - 1] += (decimal)prihod.UkupniPrihod;
            }
        }

        // Prosleđivanje podataka na View
        ViewBag.PrihodiData = prihodiData;
        ViewBag.PrihodiPoKategorijama = prihodiPoKategorijama;
        ViewBag.UkupniPrihodGodina = ukupniPrihodGodina;
        ViewBag.ProsjecniPrihod = prosjecniPrihod;
        ViewBag.Godina = trenutnaGodina;

        return View();
    }


    [HttpGet]
    public async Task<IActionResult> MjesecniTroskovi(int? godina)
    {
        var trenutnaGodina = godina ?? DateTime.Now.Year;

        // Grupiraj troškove po mjesecima
        var troskoviPoMjesecima = await _context.Troskovi
            .Where(t => t.Godina == trenutnaGodina)
            .GroupBy(t => new { t.Mjesec, t.KategorijeTroskova.Naziv })
            .Select(g => new
            {
                Mjesec = g.Key.Mjesec,
                Kategorija = g.Key.Naziv,
                UkupniTrosak = g.Sum(t => t.Iznos)
            })
            .ToListAsync();

        // Ukupni trošak za godinu
        var ukupniTrosakGodina = troskoviPoMjesecima.Sum(t => t.UkupniTrosak);

        // Prosječni trošak po mjesecu
        var prosjecniTrosak = ukupniTrosakGodina / 12;

        // Pripremi podatke za prikaz
        var troskoviData = new decimal[12];
        var troskoviPoKategorijama = new Dictionary<string, decimal[]>();

        foreach (var trosak in troskoviPoMjesecima)
        {
            if (trosak.Mjesec.HasValue)
            {
                // Ukupni troškovi po mjesecima
                troskoviData[trosak.Mjesec.Value - 1] += (decimal)trosak.UkupniTrosak;

                // Troškovi po kategorijama
                if (!troskoviPoKategorijama.ContainsKey(trosak.Kategorija))
                {
                    troskoviPoKategorijama[trosak.Kategorija] = new decimal[12];
                }
                troskoviPoKategorijama[trosak.Kategorija][trosak.Mjesec.Value - 1] += (decimal)trosak.UkupniTrosak;
            }
        }

        // Prosleđivanje podataka na View
        ViewBag.TroskoviData = troskoviData;
        ViewBag.TroskoviPoKategorijama = troskoviPoKategorijama;
        ViewBag.UkupniTrosakGodina = ukupniTrosakGodina;
        ViewBag.ProsjecniTrosak = prosjecniTrosak;
        ViewBag.Godina = trenutnaGodina;

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> ProdajaPoKategorijama(string sortDirection = "asc")
    {
        // Učitavanje podataka
        var prodajaPoKategorijama = await _context.Kategorije
            .Select(k => new
            {
                Kategorija = k.Naziv,
                UkupnoProdano = k.Artikli.Sum(a => a.NabavnaKolicina - a.TrenutnaKolicina)
            })
            .ToListAsync();

        // Sortiranje podataka
        if (sortDirection == "asc")
        {
            prodajaPoKategorijama = prodajaPoKategorijama.OrderBy(p => p.UkupnoProdano).ToList();
        }
        else
        {
            prodajaPoKategorijama = prodajaPoKategorijama.OrderByDescending(p => p.UkupnoProdano).ToList();
        }

        // Najpopularnija i najmanje popularna kategorija
        var najpopularnijaKategorija = prodajaPoKategorijama.FirstOrDefault(p => p.UkupnoProdano == prodajaPoKategorijama.Max(x => x.UkupnoProdano));
        var najmanjePopularnaKategorija = prodajaPoKategorijama.FirstOrDefault(p => p.UkupnoProdano == prodajaPoKategorijama.Min(x => x.UkupnoProdano));

        // Prosljeđivanje podataka na View
        ViewBag.ProdajaPoKategorijama = prodajaPoKategorijama;
        ViewBag.NajpopularnijaKategorija = najpopularnijaKategorija;
        ViewBag.NajmanjePopularnaKategorija = najmanjePopularnaKategorija;
        ViewBag.SortDirection = sortDirection;

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> NajprodavanijiArtikli()
    {
        // Dohvati artikle i njihove prodaje te ih sortiraj po prodaji (smanjujuće)
        var najprodavanijiArtikli = await _context.Artikli
            .OrderByDescending(a => a.NabavnaKolicina - a.TrenutnaKolicina)  // Pretpostavljamo da je prodano = NabavnaKolicina - TrenutnaKolicina
            .Take(10)  // Uzimamo top 10 artikala
            .Select(a => new
            {
                a.Naziv,
                Prodano = a.NabavnaKolicina - a.TrenutnaKolicina
            })
            .ToListAsync();

        // Pripremamo podatke za View
        ViewBag.ArtikliNazivi = najprodavanijiArtikli.Select(a => a.Naziv).ToArray();
        ViewBag.ArtikliProdaja = najprodavanijiArtikli.Select(a => a.Prodano).ToArray();

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> MjesecniProfiti(int? godina)
    {
        var trenutnaGodina = godina ?? DateTime.Now.Year;

        // Grupiraj prihode po mjesecima
        var prihodiPoMjesecima = await _context.Prihodi
            .Where(p => p.Godina == trenutnaGodina)
            .GroupBy(p => p.Mjesec)
            .Select(g => new
            {
                Mjesec = g.Key,
                UkupniPrihod = g.Sum(p => p.Iznos)
            })
            .ToListAsync();

        // Grupiraj troškove po mjesecima
        var troskoviPoMjesecima = await _context.Troskovi
            .Where(t => t.Godina == trenutnaGodina)
            .GroupBy(t => t.Mjesec)
            .Select(g => new
            {
                Mjesec = g.Key,
                UkupniTrosak = g.Sum(t => t.Iznos)
            })
            .ToListAsync();

        // Pripremi podatke za prikaz profita
        var profitiPoMjesecima = new decimal[12];

        for (int i = 1; i <= 12; i++)
        {
            var prihod = (decimal)(prihodiPoMjesecima.FirstOrDefault(p => p.Mjesec == i)?.UkupniPrihod ?? 0);
            var trosak = (decimal)(troskoviPoMjesecima.FirstOrDefault(t => t.Mjesec == i)?.UkupniTrosak ?? 0);

            profitiPoMjesecima[i - 1] = prihod - trosak;  // Profit je razlika između prihoda i troškova
        }


        // Ukupni profit za godinu
        var ukupniProfitGodina = profitiPoMjesecima.Sum();

        // Prosječni profit po mjesecu
        var prosjecniProfit = ukupniProfitGodina / 12;

        // Prosleđivanje podataka na View
        ViewBag.ProfitiData = profitiPoMjesecima;
        ViewBag.UkupniProfitGodina = ukupniProfitGodina;
        ViewBag.ProsjecniProfit = prosjecniProfit;
        ViewBag.Godina = trenutnaGodina;

        return View();
    }


}
