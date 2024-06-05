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

    public async Task<IEnumerable<Kategorije>> GetAllAsync(int pageNumber = 8, int pageSize = 100)
    {
        var skipResults = (pageNumber - 1) * pageSize;
        return await _context.Kategorije.Skip(skipResults).Take(pageSize).ToListAsync();
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
}
