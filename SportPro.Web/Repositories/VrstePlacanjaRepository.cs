using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class VrstePlacanjaRepository : IVrstePlacanjaRepository
{
    private readonly ApplicationDbContext applicationDbContext;

    public VrstePlacanjaRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<VrstePlacanja>> GetAllAsync()
    {
        return await applicationDbContext.VrstePlacanja.ToListAsync();
    }

    public async Task<VrstePlacanja> AddAsync(VrstePlacanja vrstaPlacanja)
    {
        await applicationDbContext.VrstePlacanja.AddAsync(vrstaPlacanja);
        await applicationDbContext.SaveChangesAsync();
        return vrstaPlacanja;
    }

    public async Task<VrstePlacanja>? GetAsync(int? id)
    {
        return await applicationDbContext.VrstePlacanja.FirstOrDefaultAsync(x => x.IDVrstaPlacanja == id);
    }

    public async Task<VrstePlacanja>? UpdateAsync(VrstePlacanja vrstaPlacanja)
    {
        applicationDbContext.VrstePlacanja.Update(vrstaPlacanja);
        await applicationDbContext.SaveChangesAsync();
        return vrstaPlacanja;
    }

    public async Task<VrstePlacanja>? DeleteAsync(int? id)
    {
        var vrstaPlacanja = await applicationDbContext.VrstePlacanja.FirstOrDefaultAsync(x => x.IDVrstaPlacanja == id);
        if (vrstaPlacanja == null)
        {
            return null;
        }
        applicationDbContext.VrstePlacanja.Remove(vrstaPlacanja);
        await applicationDbContext.SaveChangesAsync();
        return vrstaPlacanja;
    }
}
