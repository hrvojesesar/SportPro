using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using System.Drawing;

namespace SportPro.Web.Repositories;
public class ZaposleniciRepository : IZaposleniciRepository
{
    private readonly ApplicationDbContext applicationDbContext;

    public ZaposleniciRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<Zaposlenici>> GetAllAsync(string? ime, string? prezime, string? grad, string? status, string? poslovnica, string? sortBy = null, string? sortDirection = null, int pageNumber = 5, int pageSize = 100)
    {
        //  return await applicationDbContext.Zaposlenici.Include(x => x.Pozicije).ToListAsync();

        var query = applicationDbContext.Zaposlenici.AsNoTracking().Include(x => x.Pozicije).Include(x => x.Poslovnica).AsQueryable();

        if (!string.IsNullOrWhiteSpace(ime))
        {
            query = query.Where(z => z.Ime.Contains(ime));
        }

        if (!string.IsNullOrWhiteSpace(prezime))
        {
            query = query.Where(z => z.Prezime.Contains(prezime));
        }

        if (!string.IsNullOrWhiteSpace(grad))
        {
            query = query.Where(z => z.Grad.Contains(grad));
        }

        if (!string.IsNullOrWhiteSpace(status) && status != "Svi")
        {
            if (status == "Slobodni")
            {
                query = query.Where(z => z.Status == "Slobodan");
            }
            else
            {
                query = query.Where(z => z.Status == "Zauzet");
            }
        }
        if (poslovnica != null && poslovnica.Any() && !poslovnica.Contains("Sve poslovnice"))
        {
            query = query.Where(z => z.Poslovnica.Naziv.Contains(poslovnica));
        }

        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

            if (string.Equals(sortBy, "Ime", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(z => z.Ime) : query.OrderBy(z => z.Ime);
            }

            if (string.Equals(sortBy, "Prezime", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(z => z.Prezime) : query.OrderBy(z => z.Prezime);
            }

            if (string.Equals(sortBy, "Adresa", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(z => z.Adresa) : query.OrderBy(z => z.Adresa);
            }

            if (string.Equals(sortBy, "Telefon", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(z => z.Telefon) : query.OrderBy(z => z.Telefon);
            }

            if (string.Equals(sortBy, "Email", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(z => z.Email) : query.OrderBy(z => z.Email);
            }

            if (string.Equals(sortBy, "DatumZaposlenja", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(z => z.DatumZaposlenja) : query.OrderBy(z => z.DatumZaposlenja);
            }

            if (string.Equals(sortBy, "Placa", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(z => z.Placa) : query.OrderBy(z => z.Placa);
            }

            if (string.Equals(sortBy, "TopliObrok", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(z => z.TopliObrok) : query.OrderBy(z => z.TopliObrok);
            }

            if (string.Equals(sortBy, "Prijevoz", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(z => z.Prijevoz) : query.OrderBy(z => z.Prijevoz);
            }

            if (string.Equals(sortBy, "UkupnaPlaca", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(z => z.UkupnaPlaca) : query.OrderBy(z => z.UkupnaPlaca);
            }

            if (string.Equals(sortBy, "JMBG", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(z => z.JMBG) : query.OrderBy(z => z.JMBG);
            }
        }

        var skipResults = (pageNumber - 1) * pageSize;
        query = query.Skip(skipResults).Take(pageSize);

        return await query.ToListAsync();
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