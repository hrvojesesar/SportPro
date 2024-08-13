using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class PrihodiRepository : IPrihodiRepository
{
    private readonly ApplicationDbContext _context;

    public PrihodiRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Prihodi>> GetAllAsync(string? naziv, string? kategorijaPrihoda, int? minValue, int? maxValue, string? sortBy, string? sortDirection, int pageNumber = 1, int pageSize = 100)
    {
        var query = _context.Prihodi.AsQueryable();

        if (!string.IsNullOrWhiteSpace(naziv))
        {
            query = query.Where(x => x.Naziv.Contains(naziv));
        }

        if (!string.IsNullOrWhiteSpace(kategorijaPrihoda))
        {
            query = query.Where(x => x.KategorijePrihodaIDKategorijePrihoda.ToString() == kategorijaPrihoda);
        }

        if (minValue.HasValue)
        {
            query = query.Where(x => x.Iznos >= minValue);
        }
        if (maxValue.HasValue)
        {
            query = query.Where(x => x.Iznos <= maxValue);
        }

        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

            if (string.Equals(sortBy, "Naziv", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.Naziv) : query.OrderBy(x => x.Naziv);
            }
            if (string.Equals(sortBy, "Datum", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.Datum) : query.OrderBy(x => x.Datum);
            }
            if (string.Equals(sortBy, "Iznos", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.Iznos) : query.OrderBy(x => x.Iznos);
            }
        }

        var skipResults = (pageNumber - 1) * pageSize;
        query = query.Skip(skipResults).Take(pageSize);

        return await query.ToListAsync();
    }

    public async Task<Prihodi> AddAsync(Prihodi prihodi)
    {
        _context.Prihodi.Add(prihodi);
        await _context.SaveChangesAsync();
        return prihodi;
    }

    public async Task<Prihodi>? GetAsync(int? id)
    {
        return await _context.Prihodi.FirstOrDefaultAsync(m => m.IDPrihod == id);
    }

    public async Task<Prihodi>? UpdateAsync(Prihodi prihodi)
    {
        var existingPrihod = await _context.Prihodi.FirstOrDefaultAsync(m => m.IDPrihod == prihodi.IDPrihod);
        if (existingPrihod == null)
        {
            return null;
        }

        existingPrihod.Naziv = prihodi.Naziv;
        existingPrihod.Opis = prihodi.Opis;
        existingPrihod.Datum = prihodi.Datum;
        existingPrihod.Iznos = prihodi.Iznos;
        existingPrihod.Mjesec = prihodi.Mjesec;
        existingPrihod.Godina = prihodi.Godina;
        existingPrihod.KategorijePrihodaIDKategorijePrihoda = prihodi.KategorijePrihodaIDKategorijePrihoda;

        await _context.SaveChangesAsync();
        return existingPrihod;
    }

    public async Task<Prihodi> DeleteAsync(int? id)
    {
        var prihod = await _context.Prihodi.FirstOrDefaultAsync(m => m.IDPrihod == id);
        if (prihod == null)
        {
            return null;
        }

        _context.Prihodi.Remove(prihod);
        await _context.SaveChangesAsync();
        return prihod;
    }

    public async Task<int> CountAsync()
    {
        return await _context.Prihodi.CountAsync();
    }

}