using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class ArtikliRepository : IArtikliRepository
{
    private readonly ApplicationDbContext _context;

    public ArtikliRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Artikli>> GetAllAsync(string? naziv, int? minValue, int? maxValue, string? snizen, string? naStanju, string? kategorija, string? poslovnica, string? sortBy, string? sortDirection, int pageNumber = 5, int pageSize = 100)
    {
        // return await _context.Artikli.Include(a => a.Boje).Include(a => a.Kategorije).Include(a => a.Velicine).Include(a => a.Poslovnice).ToListAsync();

        var query = _context.Artikli.Include(a => a.Boje).Include(a => a.Kategorije).Include(a => a.Velicine).Include(a => a.Poslovnice).AsQueryable();

        if (!string.IsNullOrWhiteSpace(naziv))
        {
            query = query.Where(a => a.Naziv.Contains(naziv));
        }

        if (minValue.HasValue)
        {
            query = query.Where(x => x.Cijena >= minValue);
        }
        if (maxValue.HasValue)
        {
            query = query.Where(x => x.Cijena <= maxValue);
        }

        if (!string.IsNullOrWhiteSpace(snizen) && snizen != "Svi")
        {
            if (snizen == "1")
            {
                query = query.Where(x => x.Snizen == true);
            }
            else
            {
                query = query.Where(x => x.Snizen == false);
            }
        }

        if (!string.IsNullOrWhiteSpace(naStanju) && naStanju != "Svi")
        {
            if (naStanju == "1")
            {
                query = query.Where(x => x.NaStanju == true);
            }
            else
            {
                query = query.Where(x => x.NaStanju == false);
            }
        }

        if (kategorija != null && kategorija.Any() && !kategorija.Contains("Sve kategorije"))
        {
            query = query.Where(x => x.Kategorije.Any(k => kategorija.Contains(k.Naziv)));
        }

        if (poslovnica != null && poslovnica.Any() && !poslovnica.Contains("Sve poslovnice"))
        {
            query = query.Where(x => x.Poslovnice.Any(p => poslovnica.Contains(p.Naziv)));
        }

        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

            if (string.Equals(sortBy, "Naziv", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.Naziv) : query.OrderBy(x => x.Naziv);
            }
            if (string.Equals(sortBy, "Cijena", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.Cijena) : query.OrderBy(x => x.Cijena);
            }
            if (string.Equals(sortBy, "SnizenaCijena", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.SnizenaCijena) : query.OrderBy(x => x.SnizenaCijena);
            }
            if (string.Equals(sortBy, "NabavnaKolicina", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.NabavnaKolicina) : query.OrderBy(x => x.NabavnaKolicina);
            }
            if (string.Equals(sortBy, "TrenutnaKolicina", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.TrenutnaKolicina) : query.OrderBy(x => x.TrenutnaKolicina);
            }
            if (string.Equals(sortBy, "DatumNabave", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.DatumNabave) : query.OrderBy(x => x.DatumNabave);
            }
            if (string.Equals(sortBy, "NabavnaCijena", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.NabavnaCijena) : query.OrderBy(x => x.NabavnaCijena);
            }
        }

        var skipResults = (pageNumber - 1) * pageSize;
        query = query.Skip(skipResults).Take(pageSize);


        return await query.ToListAsync();
    }

    public async Task<Artikli> AddAsync(Artikli artikal)
    {
        await _context.Artikli.AddAsync(artikal);
        await _context.SaveChangesAsync();
        return artikal;
    }

    public async Task<Artikli?> GetAsync(int? id)
    {
        return await _context.Artikli.Include(a => a.Boje).Include(a => a.Kategorije).Include(a => a.Velicine).Include(a => a.Poslovnice).FirstOrDefaultAsync(a => a.IDArtikal == id);
    }

    public async Task<Artikli?> UpdateAsync(Artikli artikal)
    {
        var existingArtikal = await _context.Artikli.Include(a => a.Boje).Include(a => a.Kategorije).Include(a => a.Velicine).Include(a => a.Poslovnice).FirstOrDefaultAsync(a => a.IDArtikal == artikal.IDArtikal);

        if (existingArtikal == null)
        {
            return null;
        }

        existingArtikal.Naziv = artikal.Naziv;
        existingArtikal.Opis = artikal.Opis;
        existingArtikal.Cijena = artikal.Cijena;
        existingArtikal.Snizen = artikal.Snizen;
        existingArtikal.SnizenaCijena = artikal.SnizenaCijena;
        existingArtikal.NabavnaKolicina = artikal.NabavnaKolicina;
        existingArtikal.TrenutnaKolicina = artikal.TrenutnaKolicina;
        existingArtikal.NaStanju = artikal.NaStanju;
        existingArtikal.DatumNabave = artikal.DatumNabave;
        existingArtikal.NabavnaCijena = artikal.NabavnaCijena;
        existingArtikal.CijenaDostave = artikal.CijenaDostave;
        existingArtikal.UkupniTrosak = artikal.UkupniTrosak;
        existingArtikal.UkupnaZarada = artikal.UkupnaZarada;
        existingArtikal.DobavljacIDDobavljac = artikal.DobavljacIDDobavljac;
        existingArtikal.BrendIDBrend = artikal.BrendIDBrend;
        existingArtikal.Kategorije = artikal.Kategorije;
        existingArtikal.Boje = artikal.Boje;
        existingArtikal.Velicine = artikal.Velicine;
        existingArtikal.Poslovnice = artikal.Poslovnice;


        await _context.SaveChangesAsync();
        return existingArtikal;
    }

    public async Task<Artikli?> DeleteAsync(int? id)
    {
        var artikal = await _context.Artikli.FirstOrDefaultAsync(a => a.IDArtikal == id);

        if (artikal == null)
        {
            return null;
        }

        _context.Artikli.Remove(artikal);
        await _context.SaveChangesAsync();
        return artikal;
    }

    public async Task<int> CountAsync()
    {
        return await _context.Artikli.CountAsync();
    }
}
