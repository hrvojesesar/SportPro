using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class TipoviPromocijaRepository : ITipoviPromocijaRepository
{
    private readonly ApplicationDbContext _context;

    public TipoviPromocijaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TipoviPromocija>> GetAllAsync(string? searchQuery, string? sortBy, string? sortDirection, int pageNumber = 5, int pageSize = 100)
    {
        var query = _context.TipoviPromocija.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            query = query.Where(x => x.Naziv.Contains(searchQuery));
        }

        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

            if (string.Equals(sortBy, "Naziv", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.Naziv) : query.OrderBy(x => x.Naziv);
            }
        }

        var skipResults = (pageNumber - 1) * pageSize;
        query = query.Skip(skipResults).Take(pageSize);

        return await query.ToListAsync();
    }

    public async Task<TipoviPromocija> AddAsync(TipoviPromocija tipPromocije)
    {
        await _context.TipoviPromocija.AddAsync(tipPromocije);
        await _context.SaveChangesAsync();
        return tipPromocije;
    }

    public async Task<TipoviPromocija>? GetAsync(int? id)
    {
        return await _context.TipoviPromocija.FirstOrDefaultAsync(x => x.IDTipPromocije == id);
    }

    public async Task<TipoviPromocija>? UpdateAsync(TipoviPromocija tipPromocije)
    {
        _context.TipoviPromocija.Update(tipPromocije);
        await _context.SaveChangesAsync();
        return tipPromocije;
    }

    public async Task<TipoviPromocija>? DeleteAsync(int? id)
    {
        var tipPromocije = await _context.TipoviPromocija.FirstOrDefaultAsync(x => x.IDTipPromocije == id);
        if (tipPromocije == null)
        {
            return null;
        }

        _context.TipoviPromocija.Remove(tipPromocije);
        await _context.SaveChangesAsync();
        return tipPromocije;
    }

    public async Task<int> CountAsync()
    {
        return await _context.TipoviPromocija.CountAsync();
    }

    public async Task<IEnumerable<TipoviPromocija>> GetAllSecAsync()
    {
        return await _context.TipoviPromocija.ToListAsync();
    }
}
