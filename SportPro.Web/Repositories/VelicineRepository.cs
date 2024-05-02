using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class VelicineRepository : IVelicineRepository
{
    private readonly ApplicationDbContext applicationDbContext;

    public VelicineRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<Velicine>> GetAllAsync()
    {
        return await applicationDbContext.Velicine.ToListAsync();
    }

    public async Task<Velicine> AddAsync(Velicine velicina)
    {
        await applicationDbContext.Velicine.AddAsync(velicina);
        await applicationDbContext.SaveChangesAsync();
        return velicina;
    }

    public async Task<Velicine>? GetAsync(int? id)
    {
        return await applicationDbContext.Velicine.FirstOrDefaultAsync(v => v.IDVelicina == id);
    }

    public async Task<Velicine>? UpdateAsync(Velicine velicina)
    {
        applicationDbContext.Velicine.Update(velicina);
        await applicationDbContext.SaveChangesAsync();
        return velicina;
    }

    public async Task<Velicine>? DeleteAsync(int? id)
    {
        var velicina = await applicationDbContext.Velicine.FirstOrDefaultAsync(v => v.IDVelicina == id);
        if (velicina == null)
        {
            return null;
        }
        applicationDbContext.Velicine.Remove(velicina);
        await applicationDbContext.SaveChangesAsync();
        return velicina;
    }
}