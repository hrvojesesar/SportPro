using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class NarudzbeRepository : INarudzbeRepository
{
    private readonly ApplicationDbContext _context;

    public NarudzbeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Narudzbe>> GetAllAsync(string? naziv, DateTime? startDate, DateTime? endDate, string? status, string? sortBy, string? sortDirection, int pageNumber = 1, int pageSize = 100)
    {
        var query = _context.Narudzbe.AsQueryable();

        if (!string.IsNullOrWhiteSpace(naziv))
        {
            query = query.Where(x => x.NazivArtikla.Contains(naziv));
        }

        if (startDate.HasValue)
        {
            query = query.Where(x => x.DatumNabave >= startDate);
        }
        if (endDate.HasValue)
        {
            query = query.Where(x => x.DatumIsporuke <= endDate);
        }

        if (!string.IsNullOrWhiteSpace(status) && status != "Svi")
        {
            if (status == "Na čekanju")
            {
                query = query.Where(x => x.Status == "Na čekanju");
            }
            if (status == "U obradi")
            {
                query = query.Where(x => x.Status == "U obradi");
            }
            if (status == "Odbijeno")
            {
                query = query.Where(x => x.Status == "Odbijeno");
            }
            if (status == "Završeno")
            {
                query = query.Where(x => x.Status == "Završeno");
            }
        }

        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

            if (string.Equals(sortBy, "NazivArtikla", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.NazivArtikla) : query.OrderBy(x => x.NazivArtikla);
            }

            if (string.Equals(sortBy, "DatumNabave", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.DatumNabave) : query.OrderBy(x => x.DatumNabave);
            }

            if (string.Equals(sortBy, "DatumIsporuke", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.DatumIsporuke) : query.OrderBy(x => x.DatumIsporuke);
            }

            if (string.Equals(sortBy, "CijenaPoKomadu", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.CijenaPoKomadu) : query.OrderBy(x => x.CijenaPoKomadu);
            }

            if (string.Equals(sortBy, "Kolicina", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.Kolicina) : query.OrderBy(x => x.Kolicina);
            }

            if (string.Equals(sortBy, "CijenaDostave", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.CijenaDostave) : query.OrderBy(x => x.CijenaDostave);
            }
        }

        var skipResults = (pageNumber - 1) * pageSize;
        query = query.Skip(skipResults).Take(pageSize);

        return await query.ToListAsync();
    }

    public async Task<Narudzbe> AddAsync(Narudzbe narudzba)
    {
        await _context.Narudzbe.AddAsync(narudzba);
        await _context.SaveChangesAsync();
        return narudzba;
    }

    public async Task<Narudzbe>? GetAsync(int? id)
    {
        return await _context.Narudzbe.FirstOrDefaultAsync(x => x.IDNarudzba == id);
    }

    public async Task<Narudzbe>? UpdateAsync(Narudzbe narudzba)
    {
        _context.Narudzbe.Update(narudzba);
        await _context.SaveChangesAsync();
        return narudzba;
    }

    public async Task<Narudzbe>? DeleteAsync(int? id)
    {
        var narudzba = await _context.Narudzbe.FirstOrDefaultAsync(x => x.IDNarudzba == id);

        if (narudzba == null)
        {
            return null;
        }

        _context.Narudzbe.Remove(narudzba);
        await _context.SaveChangesAsync();
        return narudzba;
    }

    public async Task<int> CountAsync()
    {
        return await _context.Narudzbe.CountAsync();
    }
}
