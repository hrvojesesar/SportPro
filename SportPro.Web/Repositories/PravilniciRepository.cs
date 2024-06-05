using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class PravilniciRepository : IPravilniciRepository
{
    private readonly ApplicationDbContext applicationDbContext;

    public PravilniciRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<Pravilnici>> GetAllAsync(int pageNumber = 8, int pageSize = 100)
    {
        var skipResults = (pageNumber - 1) * pageSize;
        return await applicationDbContext.Pravilnici.Skip(skipResults).Take(pageSize).ToListAsync();
    }

    public async Task<Pravilnici> AddAsync(Pravilnici pravilnik)
    {
        await applicationDbContext.Pravilnici.AddAsync(pravilnik);
        await applicationDbContext.SaveChangesAsync();
        return pravilnik;
    }

    public async Task<Pravilnici>? GetAsync(int? id)
    {
        return await applicationDbContext.Pravilnici.FirstOrDefaultAsync(x => x.IDPravilnik == id);
    }

    public async Task<Pravilnici>? UpdateAsync(Pravilnici pravilnik)
    {
        applicationDbContext.Pravilnici.Update(pravilnik);
        await applicationDbContext.SaveChangesAsync();
        return pravilnik;
    }

    public async Task<Pravilnici>? DeleteAsync(int? id)
    {
        var pravilnik = await applicationDbContext.Pravilnici.FirstOrDefaultAsync(x => x.IDPravilnik == id);
        if (pravilnik == null)
        {
            return null;
        }

        applicationDbContext.Pravilnici.Remove(pravilnik);
        await applicationDbContext.SaveChangesAsync();
        return pravilnik;
    }

    public async Task<int> CountAsync()
    {
        return await applicationDbContext.Pravilnici.CountAsync();
    }
}
