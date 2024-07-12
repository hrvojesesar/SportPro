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

    public async Task<IEnumerable<Pravilnici>> GetAllAsync(string? searchQuery, string? searchQuery2, DateTime? startDate, DateTime? endDate, string? sortBy, string? sortDirection, int pageNumber = 1, int pageSize = 100)
    {
       var query = applicationDbContext.Pravilnici.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            query = query.Where(x => x.Naziv.Contains(searchQuery));
        }

        if (!string.IsNullOrWhiteSpace(searchQuery2))
        {
            query = query.Where(x => x.Aktivan == (searchQuery2 == "1"));
        }

        if (startDate.HasValue)
        {
            query = query.Where(x => x.DatumObjavljivanja >= startDate);
        }

        if (endDate.HasValue)
        {
            query = query.Where(x => x.DatumObjavljivanja <= endDate);
        }

        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

            if (string.Equals(sortBy, "Naziv", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.Naziv) : query.OrderBy(x => x.Naziv);
            }
            if (string.Equals(sortBy, "DatumObjavljivanja", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.DatumObjavljivanja) : query.OrderBy(x => x.DatumObjavljivanja);
            }
            if (string.Equals(sortBy, "Aktivan", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.Aktivan) : query.OrderBy(x => x.Aktivan);
            }
        }

        var skipResults = (pageNumber - 1) * pageSize;
        query = query.Skip(skipResults).Take(pageSize);

        return await query.ToListAsync();
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
