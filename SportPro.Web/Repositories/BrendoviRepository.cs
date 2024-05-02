using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class BrendoviRepository : IBrendoviRepository
{
    private readonly ApplicationDbContext applicationDbContext;

    public BrendoviRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<Brendovi>> GetAllAsync()
    {
        return await applicationDbContext.Brendovi.ToListAsync();
    }

    public async Task<Brendovi> AddAsync(Brendovi brend)
    {
        await applicationDbContext.Brendovi.AddAsync(brend);
        await applicationDbContext.SaveChangesAsync();
        return brend;
    }

    public async Task<Brendovi> GetAsync(int? id)
    {
        return await applicationDbContext.Brendovi.FirstOrDefaultAsync(x => x.IDBrend == id);
    }

    public async Task<Brendovi> UpdateAsync(Brendovi brend)
    {
        applicationDbContext.Brendovi.Update(brend);
        await applicationDbContext.SaveChangesAsync();
        return brend;
    }

    public async Task<Brendovi> DeleteAsync(int? id)
    {
        var brend = await applicationDbContext.Brendovi.FirstOrDefaultAsync(x => x.IDBrend == id);
        if (brend == null)
        {
            return null;
        }
        applicationDbContext.Brendovi.Remove(brend);
        await applicationDbContext.SaveChangesAsync();
        return brend;
    }
}
