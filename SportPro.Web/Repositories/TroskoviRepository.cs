using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;
public class TroskoviRepository : ITroskoviRepository
{
    private readonly ApplicationDbContext _context;

    public TroskoviRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Troskovi>> GetAllAsync()
    {
        return await _context.Troskovi.ToListAsync();
    }

    public async Task<Troskovi> AddAsync(Troskovi troskovi)
    {
        await _context.Troskovi.AddAsync(troskovi);
        await _context.SaveChangesAsync();
        return troskovi;
    }

    public async Task<Troskovi>? GetAsync(int? id)
    {
        return await _context.Troskovi.FirstOrDefaultAsync(m => m.IDTrosak == id);
    }

    public async Task<Troskovi> UpdateAsync(Troskovi troskovi)
    {
        var existingTrosak = await _context.Troskovi.FirstOrDefaultAsync(m => m.IDTrosak == troskovi.IDTrosak);
        if (existingTrosak == null)
        {
            return null;
        }

        existingTrosak.Naziv = troskovi.Naziv;
        existingTrosak.Opis = troskovi.Opis;
        existingTrosak.Datum = troskovi.Datum;
        existingTrosak.Iznos = troskovi.Iznos;
        existingTrosak.Mjesec = troskovi.Mjesec;
        existingTrosak.Godina = troskovi.Godina;
        existingTrosak.KategorijeTroskovaIDKategorijaTroska = troskovi.KategorijeTroskovaIDKategorijaTroska;

        await _context.SaveChangesAsync();
        return existingTrosak;
    }

    public async Task<Troskovi> DeleteAsync(int? id)
    {
        var trosak = await _context.Troskovi.FirstOrDefaultAsync(m => m.IDTrosak == id);
        if (trosak == null)
        {
            return null;
        }

        _context.Troskovi.Remove(trosak);
        await _context.SaveChangesAsync();
        return trosak;
    }

    public async Task<int> CountAsync()
    {
        return await _context.Troskovi.CountAsync();
    }
}
