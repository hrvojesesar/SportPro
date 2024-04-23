using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class RasporedRepository : IRasporedRepository
{
    private readonly ApplicationDbContext _context;

    public RasporedRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Raspored>> GetAllAsync()
    {
        return await _context.Raspored.ToListAsync();
    }

    public async Task<Raspored> AddAsync(Raspored raspored)
    {
        _context.Raspored.Add(raspored);
        await _context.SaveChangesAsync();
        return raspored;
    }

    public async Task<Raspored?> GetAsync(int? id)
    {
        return await _context.Raspored.FirstOrDefaultAsync(x => x.IDRaspored == id);
    }

    public async Task<Raspored?> UpdateAsync(Raspored raspored)
    {
        _context.Raspored.Update(raspored);
        await _context.SaveChangesAsync();
        return raspored;
    }

    public async Task<Raspored?> DeleteAsync(int id)
    {
        var raspored = await _context.Raspored.FirstOrDefaultAsync(x => x.IDRaspored == id);
        if (raspored == null)
        {
            return null;
        }
        _context.Raspored.Remove(raspored);
        await _context.SaveChangesAsync();
        return raspored;
    }
}
