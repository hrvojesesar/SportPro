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

    public async Task<IEnumerable<Dobavljaci>> GetAllAsync(string? naziv, string? grad, string? aktivnaSuradnja, string? sortBy, string? sortDirection, int pageNumber = 5, int pageSize = 100)
    {
        var query = applicationDbContext.Dobavljaci.AsQueryable();

        if (!string.IsNullOrWhiteSpace(naziv))
        {
            query = query.Where(d => d.Naziv.Contains(naziv));
        }

        if (!string.IsNullOrWhiteSpace(grad))
        {
            query = query.Where(d => d.Grad.Contains(grad));
        }

        if (!string.IsNullOrWhiteSpace(aktivnaSuradnja) && aktivnaSuradnja != "Svi")
        {
            if (aktivnaSuradnja == "Aktivna suradnja")
            {
                query = query.Where(d => d.SuradnjaAktivna == "Da");
            }
            else
            {
                query = query.Where(d => d.SuradnjaAktivna == "Ne");
            }
        }

        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

            if (string.Equals(sortBy, "Naziv", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(d => d.Naziv) : query.OrderBy(d => d.Naziv);
            }
            if (string.Equals(sortBy, "Adresa", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(d => d.Adresa) : query.OrderBy(d => d.Adresa);
            }
            if (string.Equals(sortBy, "Telefon", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(d => d.Telefon) : query.OrderBy(d => d.Telefon);
            }
            if (string.Equals(sortBy, "Email", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(d => d.Email) : query.OrderBy(d => d.Email);
            }
            if (string.Equals(sortBy, "PocetakSuradnje", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(d => d.PocetakSuradnje) : query.OrderBy(d => d.PocetakSuradnje);
            }
            if (string.Equals(sortBy, "KrajSuradnje", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(d => d.KrajSuradnje) : query.OrderBy(d => d.KrajSuradnje);
            }
        }

        var skipResults = (pageNumber - 1) * pageSize;
        query = query.Skip(skipResults).Take(pageSize);

        return await query.ToListAsync();

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

    public async Task<int> CountAsync()
    {
        return await applicationDbContext.Dobavljaci.CountAsync();
    }
}
