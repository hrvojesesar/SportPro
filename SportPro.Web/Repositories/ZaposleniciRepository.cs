using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class ZaposleniciRepository : IZaposleniciRepository
{
    private readonly ApplicationDbContext _context;

    public ZaposleniciRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Zaposlenici>> GetAllAsync()
    {
        return await _context.Zaposlenici.ToListAsync();
    }

    public async Task<Zaposlenici> AddAsync(Zaposlenici zaposlenici)
    {
        _context.Zaposlenici.Add(zaposlenici);
        await _context.SaveChangesAsync();
        return zaposlenici;
    }

    public async Task<Zaposlenici?> GetAsync(int? id)
    {
        return await _context.Zaposlenici.FindAsync(id);
    }

    public async Task<Zaposlenici?> UpdateAsync(Zaposlenici zaposlenici)
    {
        _context.Zaposlenici.Update(zaposlenici);
        await _context.SaveChangesAsync();
        return zaposlenici;
    }

    public async Task<Zaposlenici?> DeleteAsync(int id)
    {
        var zaposlenici = await _context.Zaposlenici.FindAsync(id);
        if (zaposlenici == null)
        {
            return null;
        }

        _context.Zaposlenici.Remove(zaposlenici);
        await _context.SaveChangesAsync();
        return zaposlenici;
    }

}
