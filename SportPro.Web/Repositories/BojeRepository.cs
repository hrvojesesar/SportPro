using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class BojeRepository : IBojeRepository
{
    private readonly ApplicationDbContext applicationDbContext;

    public BojeRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }
    public async Task<IEnumerable<Boje>> GetAllAsync(string? searchQuery, string? sortBy, string? sortDirection, int pageNumber = 1, int pageSize = 100)
    {
       var query = applicationDbContext.Boje.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            query = query.Where(x => x.NazivBoje.Contains(searchQuery));
        }

        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

            if (string.Equals(sortBy, "NazivBoje", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.NazivBoje) : query.OrderBy(x => x.NazivBoje);
            }
        }

        //Paginacija
        var skipResults = (pageNumber - 1) * pageSize;
        query = query.Skip(skipResults).Take(pageSize);

        return await query.ToListAsync();
    }

    public async Task<Boje> AddAsync(Boje boja)
    {
        await applicationDbContext.Boje.AddAsync(boja);
        await applicationDbContext.SaveChangesAsync();
        return boja;
    }

    public async Task<Boje>? GetAsync(int? id)
    {
        return await applicationDbContext.Boje.FirstOrDefaultAsync(b => b.IDBoja == id);
    }

    public async Task<Boje>? UpdateAsync(Boje boja)
    {
        applicationDbContext.Boje.Update(boja);
        await applicationDbContext.SaveChangesAsync();
        return boja;
    }

    public async Task<Boje>? DeleteAsync(int? id)
    {
        var boja = await applicationDbContext.Boje.FirstOrDefaultAsync(b => b.IDBoja == id);
        if (boja == null)
        {
            return null;
        }

        applicationDbContext.Boje.Remove(boja);
        await applicationDbContext.SaveChangesAsync();
        return boja;
    }

    public async Task<int> CountAsync()
    {
        return await applicationDbContext.Boje.CountAsync();
    }
}
