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
    public async Task<IEnumerable<Boje>> GetAllAsync(int pageNuber = 10, int pageSize = 100)
    {
        var skipResults = (pageNuber - 1) * pageSize;
        return await applicationDbContext.Boje.Skip(skipResults).Take(pageSize).ToListAsync();
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
