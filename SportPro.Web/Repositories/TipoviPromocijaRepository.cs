using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class TipoviPromocijaRepository : ITipoviPromocijaRepository
{
    private readonly ApplicationDbContext _context;

    public TipoviPromocijaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TipoviPromocija>> GetAllAsync()
    {
        return await _context.TipoviPromocija.ToListAsync();
    }

    public async Task<TipoviPromocija> AddAsync(TipoviPromocija tipPromocije)
    {
        await _context.TipoviPromocija.AddAsync(tipPromocije);
        await _context.SaveChangesAsync();
        return tipPromocije;
    }

    public async Task<TipoviPromocija>? GetAsync(int? id)
    {
        return await _context.TipoviPromocija.FirstOrDefaultAsync(x => x.IDTipPromocije == id);
    }

    public async Task<TipoviPromocija>? UpdateAsync(TipoviPromocija tipPromocije)
    {
        _context.TipoviPromocija.Update(tipPromocije);
        await _context.SaveChangesAsync();
        return tipPromocije;
    }

    public async Task<TipoviPromocija>? DeleteAsync(int? id)
    {
        var tipPromocije = await _context.TipoviPromocija.FirstOrDefaultAsync(x => x.IDTipPromocije == id);
        if (tipPromocije == null)
        {
            return null;
        }

        _context.TipoviPromocija.Remove(tipPromocije);
        await _context.SaveChangesAsync();
        return tipPromocije;
    }
}
