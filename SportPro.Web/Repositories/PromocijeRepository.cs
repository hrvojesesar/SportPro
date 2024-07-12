using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class PromocijeRepository : IPromocijeRepository
{
    private readonly ApplicationDbContext _context;

    public PromocijeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Promocije>> GetAllAsync(string? naziv, string? tipPromocije, string? aktivan, string? sortBy, string? sortDirection, int pageNumber = 1, int pageSize = 100)
    {
        var query = _context.Promocije.AsQueryable();

        if (!string.IsNullOrWhiteSpace(naziv))
        {
            query = query.Where(x => x.Naziv.Contains(naziv));
        }

        if (!string.IsNullOrWhiteSpace(tipPromocije))
        {
            query = query.Where(x => x.TipoviPromocijaIDTipPromocije.ToString() == tipPromocije);
        }

        if (!string.IsNullOrWhiteSpace(aktivan))
        {
            bool isActive = aktivan == "1";
            query = query.Where(x => x.Aktivna == isActive);
        }

        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

            if (string.Equals(sortBy, "Naziv", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.Naziv) : query.OrderBy(x => x.Naziv);
            }
            else if (string.Equals(sortBy, "DatumPocetka", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.DatumPocetka) : query.OrderBy(x => x.DatumPocetka);
            }
            else if (string.Equals(sortBy, "DatumZavrsetka", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.DatumZavrsetka) : query.OrderBy(x => x.DatumZavrsetka);
            }
        }

        var skipResults = (pageNumber - 1) * pageSize;
        query = query.Skip(skipResults).Take(pageSize);

        return await query.ToListAsync();
    }


    public async Task<Promocije> AddAsync(Promocije promocije)
    {
        await _context.Promocije.AddAsync(promocije);
        await _context.SaveChangesAsync();
        return promocije;
    }

    public Task<Promocije>? GetAsync(int? id)
    {
        return _context.Promocije.FirstOrDefaultAsync(x => x.IDPromocije == id);
    }

    public async Task<Promocije> UpdateAsync(Promocije promocije)
    {
        var existingPromocija = await _context.Promocije.FirstOrDefaultAsync(x => x.IDPromocije == promocije.IDPromocije);
        if (existingPromocija == null)
        {
            return null;
        }

        existingPromocija.Naziv = promocije.Naziv;
        existingPromocija.Opis = promocije.Opis;
        existingPromocija.DatumPocetka = promocije.DatumPocetka;
        existingPromocija.DatumZavrsetka = promocije.DatumZavrsetka;
        existingPromocija.Aktivna = promocije.Aktivna;
        existingPromocija.DodatniUvjeti = promocije.DodatniUvjeti;
        existingPromocija.Slika = promocije.Slika;
        existingPromocija.TipoviPromocijaIDTipPromocije = promocije.TipoviPromocijaIDTipPromocije;


        await _context.SaveChangesAsync();
        return existingPromocija;
    }

    public async Task<Promocije> DeleteAsync(int? id)
    {
        var promocija = await _context.Promocije.FirstOrDefaultAsync(x => x.IDPromocije == id);
        if (promocija == null)
        {
            return null;
        }

        _context.Promocije.Remove(promocija);
        await _context.SaveChangesAsync();
        return promocija;
    }

    public async Task<int> CountAsync()
    {
        return await _context.Promocije.CountAsync();
    }
}
