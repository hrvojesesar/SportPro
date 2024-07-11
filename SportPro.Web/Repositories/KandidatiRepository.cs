using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using System.Linq;

namespace SportPro.Web.Repositories;

public class KandidatiRepository : IKandidatiRepository
{
    private readonly ApplicationDbContext _context;

    public KandidatiRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Kandidati>> GetAllAsync(string? ime, string? prezime, string? grad, string? natjecaj, string? sortBy, string? sortDirection, int pageNumber = 5, int pageSize = 100)
    {
        var query = _context.Kandidati.AsQueryable();

        if (!string.IsNullOrWhiteSpace(ime))
        {
            query = query.Where(k => k.Ime.Contains(ime));
        }

        if (!string.IsNullOrWhiteSpace(prezime))
        {
            query = query.Where(k => k.Prezime.Contains(prezime));
        }

        if (!string.IsNullOrWhiteSpace(grad))
        {
            query = query.Where(k => k.Grad.Contains(grad));
        }

        if (!string.IsNullOrWhiteSpace(natjecaj))
        {
            query = query.Where(k => k.Natjecaji.Any(n => n.Naziv.Contains(natjecaj)));
        }

        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

            if (string.Equals(sortBy, "Ime", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(k => k.Ime) : query.OrderBy(k => k.Ime);
            }

            if (string.Equals(sortBy, "Prezime", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(k => k.Prezime) : query.OrderBy(k => k.Prezime);
            }

            if (string.Equals(sortBy, "Adresa", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(k => k.Grad) : query.OrderBy(k => k.Grad);
            }

            if (string.Equals(sortBy, "Grad", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(k => k.Grad) : query.OrderBy(k => k.Grad);
            }

            if (string.Equals(sortBy, "Drzava", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(k => k.Drzava) : query.OrderBy(k => k.Drzava);
            }

            if (string.Equals(sortBy, "PostanskiBroj", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(k => k.PostanskiBroj) : query.OrderBy(k => k.PostanskiBroj);
            }

            if (string.Equals(sortBy, "Email", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(k => k.Email) : query.OrderBy(k => k.Email);
            }

            if (string.Equals(sortBy, "Telefon", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(k => k.Telefon) : query.OrderBy(k => k.Telefon);
            }

            if (string.Equals(sortBy, "DatumPrijave", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(k => k.DatumPrijave) : query.OrderBy(k => k.DatumPrijave);
            }

            if (string.Equals(sortBy, "Natjecaji", StringComparison.OrdinalIgnoreCase))
            {
                query = isDesc ? query.OrderByDescending(k => k.Natjecaji) : query.OrderBy(k => k.Natjecaji);
            }

        }

        //Paginacija
        var skipResults = (pageNumber - 1) * pageSize;
        query = query.Skip(skipResults).Take(pageSize);

        return await query.Include(k => k.Natjecaji).ToListAsync();
    }

    public async Task<Kandidati> AddAsync(Kandidati kandidat)
    {
        await _context.Kandidati.AddAsync(kandidat);
        await _context.SaveChangesAsync();
        return kandidat;
    }

    public async Task<Kandidati>? GetAsync(int? id)
    {
        return await _context.Kandidati.Include(k => k.Natjecaji).FirstOrDefaultAsync(k => k.IDKandidat == id);
    }

    public async Task<Kandidati>? UpdateAsync(Kandidati kandidat)
    {
        var existingKandidat = await _context.Kandidati.Include(k => k.Natjecaji).FirstOrDefaultAsync(k => k.IDKandidat == kandidat.IDKandidat);

        if (existingKandidat == null)
        {
            return null;
        }

        existingKandidat.Ime = kandidat.Ime;
        existingKandidat.Prezime = kandidat.Prezime;
        existingKandidat.Adresa = kandidat.Adresa;
        existingKandidat.Grad = kandidat.Grad;
        existingKandidat.Drzava = kandidat.Drzava;
        existingKandidat.PostanskiBroj = kandidat.PostanskiBroj;
        existingKandidat.Email = kandidat.Email;
        existingKandidat.Telefon = kandidat.Telefon;
        existingKandidat.DatumPrijave = kandidat.DatumPrijave;
        existingKandidat.Napomene = kandidat.Napomene;
        existingKandidat.Natjecaji = kandidat.Natjecaji;

        await _context.SaveChangesAsync();
        return existingKandidat;
    }

    public async Task<Kandidati>? DeleteAsync(int? id)
    {
        var kandidat = await _context.Kandidati.FirstOrDefaultAsync(k => k.IDKandidat == id);

        if (kandidat == null)
        {
            return null;
        }

        _context.Kandidati.Remove(kandidat);
        await _context.SaveChangesAsync();
        return kandidat;
    }

    public async Task<IEnumerable<string>> GetByNatjecajAsync(int? idNatjecaj)
    {
        var kandidati = await _context.Kandidati
            .Include(k => k.Natjecaji)
            .Where(k => k.Natjecaji.Any(n => n.IDNatjecaj == idNatjecaj))
            .ToListAsync();

        var imePrezimeList = kandidati.Select(k => $"{k.Ime} {k.Prezime}");

        return imePrezimeList;
    }

    public async Task<int> CountAsync()
    {
        return await _context.Kandidati.CountAsync();
    }

}
