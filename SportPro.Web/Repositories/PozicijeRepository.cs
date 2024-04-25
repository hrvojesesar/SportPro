using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class PozicijeRepository : IPozicijeRepository
{
    private readonly ApplicationDbContext _context;

    public PozicijeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pozicije>> GetAllAsync()
    {
        return await _context.Pozicije.ToListAsync();
    }

    public async Task<Pozicije> AddAsync(Pozicije pozicija)
    {
        await _context.Pozicije.AddAsync(pozicija);
        await _context.SaveChangesAsync();
        return pozicija;
    }

    public async Task<Pozicije>? GetAsync(int? id)
    {
        return await _context.Pozicije.FirstOrDefaultAsync(p => p.IDPozicija == id);
    }

    public async Task<Pozicije>? UpdateAsync(Pozicije pozicija)
    {
        _context.Pozicije.Update(pozicija);
        await _context.SaveChangesAsync();
        return pozicija;
    }

    public async Task<Pozicije>? DeleteAsync(int? id)
    {
        var pozicija = await _context.Pozicije.FirstOrDefaultAsync(p => p.IDPozicija == id);
        if (pozicija != null)
        {
            _context.Pozicije.Remove(pozicija);
            await _context.SaveChangesAsync();
        }
        return pozicija;
    }
}
