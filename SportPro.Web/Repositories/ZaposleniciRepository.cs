using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;
public class ZaposleniciRepository : IZaposleniciRepository
{
    private readonly ApplicationDbContext applicationDbContext;

    public ZaposleniciRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<Zaposlenici>> GetAllAsync(int pageNumber = 3, int pageSize = 100)
    {
        var skipResults = (pageNumber - 1) * pageSize;
        return await applicationDbContext.Zaposlenici.Include(x => x.Pozicije).Skip(skipResults).Take(pageSize).ToListAsync();
    }

    public async Task<Zaposlenici> AddAsync(Zaposlenici zaposlenik)
    {
        await applicationDbContext.Zaposlenici.AddAsync(zaposlenik);
        await applicationDbContext.SaveChangesAsync();
        return zaposlenik;
    }

    public Task<Zaposlenici>? GetAsync(int? id)
    {
        return applicationDbContext.Zaposlenici.Include(x => x.Pozicije).FirstOrDefaultAsync(z => z.IDZaposlenik == id);
    }

    public async Task<Zaposlenici>? UpdateAsync(Zaposlenici zaposlenik)
    {
        var existingZaposlenik = await applicationDbContext.Zaposlenici.Include(x => x.Pozicije).FirstOrDefaultAsync(z => z.IDZaposlenik == zaposlenik.IDZaposlenik);

        if (existingZaposlenik == null)
        {
            return null;
        }

        existingZaposlenik.Ime = zaposlenik.Ime;
        existingZaposlenik.Prezime = zaposlenik.Prezime;
        existingZaposlenik.Spol = zaposlenik.Spol;
        existingZaposlenik.Adresa = zaposlenik.Adresa;
        existingZaposlenik.Grad = zaposlenik.Grad;
        existingZaposlenik.Drzava = zaposlenik.Drzava;
        existingZaposlenik.Telefon = zaposlenik.Telefon;
        existingZaposlenik.Email = zaposlenik.Email;
        existingZaposlenik.DatumZaposlenja = zaposlenik.DatumZaposlenja;
        existingZaposlenik.Placa = zaposlenik.Placa;
        existingZaposlenik.TopliObrok = zaposlenik.TopliObrok;
        existingZaposlenik.Prijevoz = zaposlenik.Prijevoz;
        existingZaposlenik.Bonus = zaposlenik.Bonus;
        existingZaposlenik.UkupnaPlaca = zaposlenik.UkupnaPlaca;
        existingZaposlenik.Certifikati = zaposlenik.Certifikati;
        existingZaposlenik.JMBG = zaposlenik.JMBG;
        existingZaposlenik.BrojBankovnogRacuna = zaposlenik.BrojBankovnogRacuna;
        existingZaposlenik.Kvalifikacija = zaposlenik.Kvalifikacija;
        existingZaposlenik.Status = zaposlenik.Status;
        existingZaposlenik.DatumZavrsetkaRadnogOdnosa = zaposlenik.DatumZavrsetkaRadnogOdnosa;
        existingZaposlenik.PoslovnicaID = zaposlenik.PoslovnicaID;
        existingZaposlenik.Pozicije = zaposlenik.Pozicije;

        await applicationDbContext.SaveChangesAsync();
        return existingZaposlenik;
    }

    public async Task<Zaposlenici>? DeleteAsync(int? id)
    {
        var zaposlenik = await applicationDbContext.Zaposlenici.FirstOrDefaultAsync(z => z.IDZaposlenik == id);
        if (zaposlenik == null)
        {
            return null;
        }
        applicationDbContext.Zaposlenici.Remove(zaposlenik);
        await applicationDbContext.SaveChangesAsync();
        return zaposlenik;
    }

    public async Task<int> CountAsync()
    {
        return await applicationDbContext.Zaposlenici.CountAsync();
    }
}