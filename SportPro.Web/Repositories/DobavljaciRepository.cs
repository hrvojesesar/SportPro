using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class DobavljaciRepository : IDobavljaciRepository
{
    private readonly ApplicationDbContext applicationDbContext;

    public DobavljaciRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<Dobavljaci>> GetAllAsync()
    {
        return await applicationDbContext.Dobavljaci.ToListAsync();
    }

    public async Task<Dobavljaci> AddAsync(Dobavljaci dobavljac)
    {
        await applicationDbContext.Dobavljaci.AddAsync(dobavljac);
        await applicationDbContext.SaveChangesAsync();
        return dobavljac;
    }

    public Task<Dobavljaci>? GetAsync(int? id)
    {
        return applicationDbContext.Dobavljaci.FirstOrDefaultAsync(d => d.IDDobavljac == id);
    }

    public async Task<Dobavljaci>? UpdateAsync(Dobavljaci dobavljac)
    {
        applicationDbContext.Dobavljaci.Update(dobavljac);
        await applicationDbContext.SaveChangesAsync();
        return dobavljac;
    }

    public async Task<Dobavljaci>? DeleteAsync(int? id)
    {
        var dobavljac = await applicationDbContext.Dobavljaci.FirstOrDefaultAsync(d => d.IDDobavljac == id);
        if (dobavljac == null)
        {
            return null;
        }

        applicationDbContext.Dobavljaci.Remove(dobavljac);
        await applicationDbContext.SaveChangesAsync();
        return dobavljac;
    }

    public async Task<IEnumerable<Dobavljaci>> GetActiveDobavljaci()
    {
        return await applicationDbContext.Dobavljaci.Where(d => d.SuradnjaAktivna == "Da").ToListAsync();
    }
}
