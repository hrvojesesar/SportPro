using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;
public class PozicijeRepository : IPozicijeRepository
{
    private readonly ApplicationDbContext _context;

    public PozicijeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pozicije>> GetAllAsync(string? searchQuery, string? sortBy, string? sortDirection, int pageNumber = 1, int pageSize = 100)
    {
        var query = _context.Pozicije.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            query = query.Where(p => p.Naziv.Contains(searchQuery));
        }

        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

            if (string.Equals(sortBy, "Naziv", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(p => p.Naziv) : query.OrderBy(p => p.Naziv);
            }

            if (string.Equals(sortBy, "Opis", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(p => p.Opis) : query.OrderBy(p => p.Opis);
            }
        }

        var skipResults = (pageNumber - 1) * pageSize;
        query = query.Skip(skipResults).Take(pageSize);

        return await query.ToListAsync();
    }

    public async Task<Pozicije> AddAsync(Pozicije pozicija)
    {
        await _context.Pozicije.AddAsync(pozicija);
        await _context.SaveChangesAsync();
        return pozicija;
    }

    public async Task<Pozicije>? GetAsync(int? id)
    {
        return await _context.Pozicije.FirstOrDefaultAsync(p => p.IDPozicija == id);
    }

    public async Task<Pozicije>? UpdateAsync(Pozicije pozicija)
    {
        _context.Pozicije.Update(pozicija);
        await _context.SaveChangesAsync();
        return pozicija;
    }

    public async Task<Pozicije>? DeleteAsync(int? id)
    {
        var pozicija = await _context.Pozicije.FirstOrDefaultAsync(p => p.IDPozicija == id);
        if (pozicija != null)
        {
            _context.Pozicije.Remove(pozicija);
            await _context.SaveChangesAsync();
        }
        return pozicija;
    }

    public async Task<int> CountAsync()
    {
        return await _context.Pozicije.CountAsync();
    }
}