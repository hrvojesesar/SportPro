using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class ProSportKorisniciRepository : IProSportKorisniciRepository
{
    private readonly ApplicationDbContext _context;

    public ProSportKorisniciRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProSportKorisnici>> GetAllAsync()
    {
        return await _context.ProSportKorisnici.ToListAsync();
    }

    public async Task<ProSportKorisnici> AddAsync(ProSportKorisnici proSportKorisnik)
    {
        await _context.ProSportKorisnici.AddAsync(proSportKorisnik);
        await _context.SaveChangesAsync();
        return proSportKorisnik;
    }

    public async Task<ProSportKorisnici>? GetAsync(int? id)
    {
        return await _context.ProSportKorisnici.FirstOrDefaultAsync(x => x.IDKorisnik == id);
    }

    public async Task<ProSportKorisnici>? UpdateAsync(ProSportKorisnici proSportKorisnik)
    {
        _context.ProSportKorisnici.Update(proSportKorisnik);
        await _context.SaveChangesAsync();
        return proSportKorisnik;
    }

    public async Task<ProSportKorisnici>? DeleteAsync(int? id)
    {
        var proSportKorisnik = await _context.ProSportKorisnici.FirstOrDefaultAsync(x => x.IDKorisnik == id);
        if (proSportKorisnik == null)
        {
            return null;
        }

        _context.ProSportKorisnici.Remove(proSportKorisnik);
        await _context.SaveChangesAsync();
        return proSportKorisnik;
    }
}
