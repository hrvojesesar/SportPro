using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class CertifikatiRepository : ICertifikatiRepository
{
    private readonly ApplicationDbContext _context;

    public CertifikatiRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Certifikati>> GetAllAsync(int pageNumber = 3, int pageSize = 100)
    {
        var skipResults = (pageNumber - 1) * pageSize;
        return await _context.Certifikati.Skip(skipResults).Take(pageSize).ToListAsync();
    }

    public async Task<Certifikati> AddAsync(Certifikati certifikat)
    {
        await _context.Certifikati.AddAsync(certifikat);
        await _context.SaveChangesAsync();
        return certifikat;
    }

    public async Task<Certifikati?> GetAsync(int? id)
    {
        return await _context.Certifikati.FirstOrDefaultAsync(c => c.IDCertifikat == id);
    }

    public async Task<Certifikati?> UpdateAsync(Certifikati certifikat)
    {
        _context.Certifikati.Update(certifikat);
        await _context.SaveChangesAsync();
        return certifikat;
    }

    public async Task<int> CountAsync()
    {
        return await _context.Certifikati.CountAsync();
    }
}