using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class PosloviRepository : IPosloviRepository
{
    private readonly ApplicationDbContext _context;

    public PosloviRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Poslovi>> GetAllAsync()
    {
        return await _context.Poslovi.ToListAsync();
    }

    public async Task<Poslovi> AddAsync(Poslovi poslovi)
    {
        _context.Poslovi.Add(poslovi);
        await _context.SaveChangesAsync();
        return poslovi;
    }

    public async Task<Poslovi?> GetAsync(int? id)
    {
        return await _context.Poslovi.FirstOrDefaultAsync(x => x.IDPosla == id);
    }

    public async Task<Poslovi?> UpdateAsync(Poslovi poslovi)
    {
        _context.Poslovi.Update(poslovi);
        await _context.SaveChangesAsync();
        return poslovi;
    }

    public async Task<Poslovi?> DeleteAsync(int id)
    {
        var poslovi = await _context.Poslovi.FirstOrDefaultAsync(x => x.IDPosla == id);
        if (poslovi == null)
        {
            return null;
        }
        _context.Poslovi.Remove(poslovi);
        await _context.SaveChangesAsync();
        return poslovi;
    }
}
