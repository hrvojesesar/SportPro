using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class NatjecajiRepository : INatjecajiRepository
{
    private readonly ApplicationDbContext applicationDbContext;

    public NatjecajiRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<Natjecaji>> GetAllAsync()
    {
        return await applicationDbContext.Natjecaji.ToListAsync();
    }

    public async Task<Natjecaji> AddAsync(Natjecaji natjecaj)
    {
        await applicationDbContext.Natjecaji.AddAsync(natjecaj);
        await applicationDbContext.SaveChangesAsync();
        return natjecaj;
    }

    public async Task<Natjecaji?> GetAsync(int? id)
    {
        return await applicationDbContext.Natjecaji.FirstOrDefaultAsync(x => x.IDNatjecaj == id);
    }

    public async Task<Natjecaji?> UpdateAsync(Natjecaji natjecaj)
    {
        applicationDbContext.Natjecaji.Update(natjecaj);
        await applicationDbContext.SaveChangesAsync();
        return natjecaj;
    }

    public async Task<Natjecaji?> DeleteAsync(int id)
    {
        var natjecaj = await applicationDbContext.Natjecaji.FirstOrDefaultAsync(x => x.IDNatjecaj == id);
        if (natjecaj == null)
        {
            return null;
        }
        applicationDbContext.Natjecaji.Remove(natjecaj);
        await applicationDbContext.SaveChangesAsync();
        return natjecaj;
    }
   

}
