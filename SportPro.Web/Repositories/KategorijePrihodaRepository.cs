using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class KategorijePrihodaRepository : IKategorijePrihodaRepository
{
    private readonly ApplicationDbContext context;

    public KategorijePrihodaRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<KategorijePrihoda>> GetAllAsync(string? searchQuery, string? sortBy, string? sortDirection, int pageNumber = 5, int pageSize = 100)
    {
        var query = context.KategorijePrihoda.AsQueryable();

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

    public async Task<KategorijePrihoda> AddAsync(KategorijePrihoda kategorijaPrihoda)
    {
        await context.KategorijePrihoda.AddAsync(kategorijaPrihoda);
        await context.SaveChangesAsync();
        return kategorijaPrihoda;
    }

    public async Task<KategorijePrihoda>? GetAsync(int? id)
    {
        return await context.KategorijePrihoda.FirstOrDefaultAsync(k => k.IDKategorijePrihoda == id);
    }

    public async Task<KategorijePrihoda>? UpdateAsync(KategorijePrihoda kategorijaPrihoda)
    {
        context.KategorijePrihoda.Update(kategorijaPrihoda);
        await context.SaveChangesAsync();
        return kategorijaPrihoda;
    }

    public async Task<KategorijePrihoda>? DeleteAsync(int? id)
    {
        var kategorijaPrihoda = await context.KategorijePrihoda.FirstOrDefaultAsync(k => k.IDKategorijePrihoda == id);
        if (kategorijaPrihoda == null)
        {
            return null;
        }

        context.KategorijePrihoda.Remove(kategorijaPrihoda);
        await context.SaveChangesAsync();
        return kategorijaPrihoda;
    }

    public async Task<int> CountAsync()
    {
        return await context.KategorijePrihoda.CountAsync();
    }

    public async Task<IEnumerable<KategorijePrihoda>> GetAllSecAsync()
    {
        return await context.KategorijePrihoda.ToListAsync();
    }
}
