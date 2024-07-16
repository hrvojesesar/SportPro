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

    public async Task<IEnumerable<Poslovnice>> GetAllAsync(string? naziv, string? grad, string? status, string? sortBy, string? sortDirection, int pageNumber = 1, int pageSize = 100)
    {
        var query = applicationDbContext.Poslovnice.AsQueryable();
        if (!string.IsNullOrWhiteSpace(naziv))
        {
            query = query.Where(p => p.Naziv.Contains(naziv));
        }
        if (!string.IsNullOrWhiteSpace(grad))
        {
            query = query.Where(p => p.Grad.Contains(grad));
        }
        if (!string.IsNullOrWhiteSpace(status) && status != "Svi")
        {
            if (status == "Aktivni")
            {
                query = query.Where(x => x.Status == "Aktivna");
            }
            else
            {
                query = query.Where(x => x.Status == "Neaktivna");
            }
        }

        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

            if (string.Equals(sortBy, "Naziv", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(p => p.Naziv) : query.OrderBy(p => p.Naziv);
            }
            if (string.Equals(sortBy, "Adresa", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(p => p.Adresa) : query.OrderBy(p => p.Adresa);
            }
            if (string.Equals(sortBy, "Telefon", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(p => p.Telefon) : query.OrderBy(p => p.Telefon);
            }
            if (string.Equals(sortBy, "Email", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(p => p.Email) : query.OrderBy(p => p.Email);
            }
            if (string.Equals(sortBy, "DatumOtvaranja", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(p => p.DatumOtvaranja) : query.OrderBy(p => p.DatumOtvaranja);
            }
        }

        var skipResults = (pageNumber - 1) * pageSize;
        query = query.Skip(skipResults).Take(pageSize);

        return await query.ToListAsync();
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

    public async Task<int> CountAsync()
    {
        return await applicationDbContext.Poslovnice.CountAsync();
    }

    public async Task<IEnumerable<Poslovnice>> GetAllSecAsync()
    {
        return await applicationDbContext.Poslovnice.ToListAsync();
    }
}