using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;
public class PoslovniceRepository : IPoslovniceRepository
{
    private readonly ApplicationDbContext applicationDbContext;

    public PoslovniceRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<Poslovnice>> GetAllAsync()
    {
        return await applicationDbContext.Poslovnice.ToListAsync();
    }

    public async Task<Poslovnice> AddAsync(Poslovnice poslovnice)
    {
        await applicationDbContext.Poslovnice.AddAsync(poslovnice);
        await applicationDbContext.SaveChangesAsync();
        return poslovnice;
    }

    public async Task<Poslovnice>? GetAsync(int? id)
    {
        return await applicationDbContext.Poslovnice.FirstOrDefaultAsync(p => p.IDPoslovnica == id);
    }

    public async Task<Poslovnice>? UpdateAsync(Poslovnice poslovnice)
    {
        applicationDbContext.Poslovnice.Update(poslovnice);
        await applicationDbContext.SaveChangesAsync();
        return poslovnice;
    }

    public async Task<Poslovnice>? DeleteAsync(int? id)
    {
        var poslovnice = await applicationDbContext.Poslovnice.FirstOrDefaultAsync(p => p.IDPoslovnica == id);
        if (poslovnice == null)
        {
            return null;
        }
        applicationDbContext.Poslovnice.Remove(poslovnice);
        await applicationDbContext.SaveChangesAsync();
        return poslovnice;
    }
}