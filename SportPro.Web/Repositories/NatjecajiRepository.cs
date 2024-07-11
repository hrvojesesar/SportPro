using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using Syncfusion.EJ2.TreeGrid;

namespace SportPro.Web.Repositories;
public class NatjecajiRepository : INatjecajiRepository
{
    private readonly ApplicationDbContext applicationDbContext;

    public NatjecajiRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<Natjecaji>> GetAllAsync(string? searchQuery, string? searchQuery2, int? minValue, int? maxValue, DateTime? startDate, DateTime? endDate, string? sortBy, string? sortDirection, int pageNumber = 1, int pageSize = 100)
    {
        var query = applicationDbContext.Natjecaji.AsQueryable();

        // Filtriranje po nazivu
        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            query = query.Where(x => x.Naziv.Contains(searchQuery));
        }

        // Filtriranje po aktivnosti
        if (!string.IsNullOrWhiteSpace(searchQuery2))
        {
            bool isActive = searchQuery2 == "1";
            query = query.Where(x => x.Aktivan == isActive);
        }

        // Filtriranje po procijenjenoj vrijednosti
        if (minValue.HasValue)
        {
            query = query.Where(x => x.ProcijenjenaVrijednost >= minValue);
        }
        if (maxValue.HasValue)
        {
            query = query.Where(x => x.ProcijenjenaVrijednost <= maxValue);
        }

        // Filtriranje po datumu
        if (startDate.HasValue)
        {
            query = query.Where(x => x.TrajanjeOd >= startDate);
        }
        if (endDate.HasValue)
        {
            query = query.Where(x => x.TrajanjeDo <= endDate);
        }


        //Sortiranje
        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

            if (string.Equals(sortBy, "Naziv", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.Naziv) : query.OrderBy(x => x.Naziv);
            }

            if (string.Equals(sortBy, "ProcijenjenaVrijednost", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.ProcijenjenaVrijednost) : query.OrderBy(x => x.ProcijenjenaVrijednost);
            }

            if (string.Equals(sortBy, "TrajanjeOd", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.TrajanjeOd) : query.OrderBy(x => x.TrajanjeOd);
            }

            if (string.Equals(sortBy, "TrajanjeDo", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.TrajanjeDo) : query.OrderBy(x => x.TrajanjeDo);
            }

            if (string.Equals(sortBy, "DatumObjave", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.DatumObjave) : query.OrderBy(x => x.DatumObjave);
            }

            if (string.Equals(sortBy, "Aktivan", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.Aktivan) : query.OrderBy(x => x.Aktivan);
            }

            if (string.Equals(sortBy, "Dobitnik", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.Dobitnik) : query.OrderBy(x => x.Dobitnik);
            }
        }

        //Paginacija
        var skipResults = (pageNumber - 1) * pageSize;
        query = query.Skip(skipResults).Take(pageSize);

        return await query.ToListAsync();
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

    public async Task<int> CountAsync()
    {
        return await applicationDbContext.Natjecaji.CountAsync();
    }

    public async Task<IEnumerable<Natjecaji>> GetAllForKandidatiAsync()
    {
        return await applicationDbContext.Natjecaji.ToListAsync();
    }
}