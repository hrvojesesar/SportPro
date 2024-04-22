using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class PonudePoslovaRepository : IPonudePoslovaRepository
{
    private readonly ApplicationDbContext _context;

    public PonudePoslovaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PonudePoslova>> GetAllAsync()
    {
        return await _context.PonudePoslova.ToListAsync();
    }

    public async Task<PonudePoslova> AddAsync(PonudePoslova ponudaPoslova)
    {
        _context.PonudePoslova.Add(ponudaPoslova);
        await _context.SaveChangesAsync();
        return ponudaPoslova;
    }

    public async Task<PonudePoslova?> GetAsync(int? id)
    {
        return await _context.PonudePoslova.FindAsync(id);
    }

    public async Task<PonudePoslova?> UpdateAsync(PonudePoslova ponudaPoslova)
    {
        _context.PonudePoslova.Update(ponudaPoslova);
        await _context.SaveChangesAsync();
        return ponudaPoslova;
    }

    public async Task<PonudePoslova?> DeleteAsync(int id)
    {
        var ponudaPoslova = await _context.PonudePoslova.FindAsync(id);
        if (ponudaPoslova == null)
        {
            return null;
        }

        _context.PonudePoslova.Remove(ponudaPoslova);
        await _context.SaveChangesAsync();
        return ponudaPoslova;
    }
}
