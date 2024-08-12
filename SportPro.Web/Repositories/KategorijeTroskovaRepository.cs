using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class KategorijeTroskovaRepository : IKategorijeTroskovaRepository
{
    private readonly ApplicationDbContext _context;

    public KategorijeTroskovaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<KategorijeTroskova>> GetAllAsync(string? searchQuery, string? sortBy, string? sortDirection, int pageNumber = 5, int pageSize = 100)
    {
        var query = _context.KategorijeTroskova.AsQueryable();

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

    public async Task<KategorijeTroskova> AddAsync(KategorijeTroskova kategorijaTroska)
    {
        await _context.KategorijeTroskova.AddAsync(kategorijaTroska);
        await _context.SaveChangesAsync();
        return kategorijaTroska;
    }

    public async Task<KategorijeTroskova>? GetAsync(int? id)
    {
        return await _context.KategorijeTroskova.FirstOrDefaultAsync(x => x.IDKategorijaTroska == id);
    }

    public async Task<KategorijeTroskova>? UpdateAsync(KategorijeTroskova kategorijaTroska)
    {
        _context.KategorijeTroskova.Update(kategorijaTroska);
        await _context.SaveChangesAsync();
        return kategorijaTroska;
    }

    public async Task<KategorijeTroskova>? DeleteAsync(int? id)
    {
        var kategorijaTroska = await _context.KategorijeTroskova.FirstOrDefaultAsync(x => x.IDKategorijaTroska == id);
        if (kategorijaTroska == null)
        {
            return null;
        }

        _context.KategorijeTroskova.Remove(kategorijaTroska);
        await _context.SaveChangesAsync();
        return kategorijaTroska;
    }

    public async Task<int> CountAsync()
    {
        return await _context.KategorijeTroskova.CountAsync();
    }

    public async Task<IEnumerable<KategorijeTroskova>> GetAllSecAsync()
    {
        return await _context.KategorijeTroskova.ToListAsync();
    }

}
