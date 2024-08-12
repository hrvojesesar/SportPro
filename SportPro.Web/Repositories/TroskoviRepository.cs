using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;
public class TroskoviRepository : ITroskoviRepository
{
    private readonly ApplicationDbContext _context;

    public TroskoviRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Troskovi>> GetAllAsync(string? naziv, string? kategorijaTroska, int? minValue, int? maxValue, string? sortBy, string? sortDirection, int pageNumber = 1, int pageSize = 100)
    {
        var query = _context.Troskovi.AsQueryable();

        if (!string.IsNullOrWhiteSpace(naziv))
        {
            query = query.Where(x => x.Naziv.Contains(naziv));
        }

        if (!string.IsNullOrWhiteSpace(kategorijaTroska))
        {
            query = query.Where(x => x.KategorijeTroskovaIDKategorijaTroska.ToString() == kategorijaTroska);
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

    public async Task<Troskovi> AddAsync(Troskovi troskovi)
    {
        await _context.Troskovi.AddAsync(troskovi);
        await _context.SaveChangesAsync();
        return troskovi;
    }

    public async Task<Troskovi>? GetAsync(int? id)
    {
        return await _context.Troskovi.FirstOrDefaultAsync(m => m.IDTrosak == id);
    }

    public async Task<Troskovi> UpdateAsync(Troskovi troskovi)
    {
        var existingTrosak = await _context.Troskovi.FirstOrDefaultAsync(m => m.IDTrosak == troskovi.IDTrosak);
        if (existingTrosak == null)
        {
            return null;
        }

        existingTrosak.Naziv = troskovi.Naziv;
        existingTrosak.Opis = troskovi.Opis;
        existingTrosak.Datum = troskovi.Datum;
        existingTrosak.Iznos = troskovi.Iznos;
        existingTrosak.Mjesec = troskovi.Mjesec;
        existingTrosak.Godina = troskovi.Godina;
        existingTrosak.KategorijeTroskovaIDKategorijaTroska = troskovi.KategorijeTroskovaIDKategorijaTroska;

        await _context.SaveChangesAsync();
        return existingTrosak;
    }

    public async Task<Troskovi> DeleteAsync(int? id)
    {
        var trosak = await _context.Troskovi.FirstOrDefaultAsync(m => m.IDTrosak == id);
        if (trosak == null)
        {
            return null;
        }

        _context.Troskovi.Remove(trosak);
        await _context.SaveChangesAsync();
        return trosak;
    }

    public async Task<int> CountAsync()
    {
        return await _context.Troskovi.CountAsync();
    }
}
