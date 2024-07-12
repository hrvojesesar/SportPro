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

    public async Task<IEnumerable<Brendovi>> GetAllAsync(string? nazivBrenda, string? vrsta, string? status, string? sortBy, string? sortDirection, int pageNumber = 1, int pageSize = 100)
    {
        var query = applicationDbContext.Brendovi.AsQueryable();

        if (!string.IsNullOrWhiteSpace(nazivBrenda))
        {
            query = query.Where(x => x.NazivBrenda.Contains(nazivBrenda));
        }


        if (!string.IsNullOrWhiteSpace(vrsta) && vrsta != "Svi")
        {
            if (vrsta == "Javni")
            {
                query = query.Where(x => x.Vrsta == "Javna");
            }
            else
            {
                query = query.Where(x => x.Vrsta == "Privatna");
            }
        }

        if (!string.IsNullOrWhiteSpace(status) && status != "Svi")
        {
            if (status == "Dostupni")
            {
                query = query.Where(x => x.Status == "Dostupan");
            }
            else
            {
                query = query.Where(x => x.Status == "Nedostupan");
            }
        }
        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);
            query = sortBy switch
            {
                "NazivBrenda" => isDesc ? query.OrderByDescending(x => x.NazivBrenda) : query.OrderBy(x => x.NazivBrenda),
                "Osnivac" => isDesc ? query.OrderByDescending(x => x.Osnivac) : query.OrderBy(x => x.Osnivac),
                "GodinaOsnivanja" => isDesc ? query.OrderByDescending(x => x.GodinaOsnivanja) : query.OrderBy(x => x.GodinaOsnivanja),
                "Sjediste" => isDesc ? query.OrderByDescending(x => x.Sjediste) : query.OrderBy(x => x.Sjediste),
                "Predsjednik" => isDesc ? query.OrderByDescending(x => x.Predsjednik) : query.OrderBy(x => x.Predsjednik),
                _ => query
            };
        }
        var skipResults = (pageNumber - 1) * pageSize;
        query = query.Skip(skipResults).Take(pageSize);

        return await query.ToListAsync();
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

    public async Task<int> CountAsync()
    {
        return await applicationDbContext.Brendovi.CountAsync();
    }
}
