using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class ZaposleniciRepository : IZaposleniciRepository
{
    private readonly ApplicationDbContext applicationDbContext;

    public ZaposleniciRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<Zaposlenici>> GetAllAsync()
    {
        return await applicationDbContext.Zaposlenici.ToListAsync();
    }

    public async Task<Zaposlenici> AddAsync(Zaposlenici zaposlenik)
    {
        await applicationDbContext.Zaposlenici.AddAsync(zaposlenik);
        await applicationDbContext.SaveChangesAsync();
        return zaposlenik;
    }

    public Task<Zaposlenici>? GetAsync(int? id)
    {
        return applicationDbContext.Zaposlenici.FirstOrDefaultAsync(z => z.IDZaposlenik == id);
    }

    public async Task<Zaposlenici>? UpdateAsync(Zaposlenici zaposlenik)
    {
        applicationDbContext.Zaposlenici.Update(zaposlenik);
        await applicationDbContext.SaveChangesAsync();
        return zaposlenik;
    }

    public async Task<Zaposlenici>? DeleteAsync(int? id)
    {
        var zaposlenik = await applicationDbContext.Zaposlenici.FirstOrDefaultAsync(z => z.IDZaposlenik == id);
        if (zaposlenik == null)
        {
            return null;
        }
        applicationDbContext.Zaposlenici.Remove(zaposlenik);
        await applicationDbContext.SaveChangesAsync();
        return zaposlenik;
    }

}
