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

    public async Task<IEnumerable<Narudzbe>> GetAllAsync()
    {
        return await _context.Narudzbe.ToListAsync();
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
}
