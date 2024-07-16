using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class KategorijeRepository : IKategorijeRepository
{
    private readonly ApplicationDbContext _context;

    public KategorijeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Kategorije>> GetAllAsync(string? searchQuery, string? sortBy, string? sortDirection, int pageNumber = 1, int pageSize = 100)
    {
        var query = _context.Kategorije.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            query = query.Where(x => x.Naziv.Contains(searchQuery));
        }

        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

            if (string.Equals(sortBy, "Naziv", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(x => x.Naziv) : query.OrderBy(x => x.Naziv);
            }
        }

        //Paginacija
        var skipResults = (pageNumber - 1) * pageSize;
        query = query.Skip(skipResults).Take(pageSize);

        return await query.ToListAsync();
    }

    public async Task<Kategorije> AddAsync(Kategorije kategorija)
    {
        await _context.Kategorije.AddAsync(kategorija);
        await _context.SaveChangesAsync();
        return kategorija;
    }

    public async Task<Kategorije> GetAsync(int? id)
    {
        return await _context.Kategorije.FirstOrDefaultAsync(x => x.IDKategorija == id);
    }

    public async Task<Kategorije> UpdateAsync(Kategorije kategorija)
    {
        _context.Kategorije.Update(kategorija);
        await _context.SaveChangesAsync();
        return kategorija;
    }

    public async Task<Kategorije> DeleteAsync(int? id)
    {
        var kategorija = await _context.Kategorije.FirstOrDefaultAsync(x => x.IDKategorija == id);
        if (kategorija == null)
        {
            return null;
        }

        _context.Kategorije.Remove(kategorija);
        await _context.SaveChangesAsync();
        return kategorija;
    }

    public async Task<int> CountAsync()
    {
        return await _context.Kategorije.CountAsync();
    }

    public async Task<IEnumerable<Kategorije>> GetAllSecAsync()
    {
        return await _context.Kategorije.ToListAsync();
    }
}
