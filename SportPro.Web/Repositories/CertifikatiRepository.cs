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
    public async Task<IEnumerable<Certifikati>> GetAllAsync(string? searchQuery, string? sortBy, string? sortDirection, int pageNumber = 1, int pageSize = 100)
    {
        var query = _context.Certifikati.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            query = query.Where(x => x.Naziv.Contains(searchQuery));
        }

        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

            query = sortBy switch
            {
                "Naziv" => isDesc ? query.OrderByDescending(x => x.Naziv) : query.OrderBy(x => x.Naziv),
                "DatumDodjele" => isDesc ? query.OrderByDescending(x => x.DatumDodjele) : query.OrderBy(x => x.DatumDodjele),
                "Organizacija" => isDesc ? query.OrderByDescending(x => x.Organizacija) : query.OrderBy(x => x.Organizacija),
                _ => query
            };
        }

        var skipResults = (pageNumber - 1) * pageSize;
        query = query.Skip(skipResults).Take(pageSize);

        return await query.ToListAsync();
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