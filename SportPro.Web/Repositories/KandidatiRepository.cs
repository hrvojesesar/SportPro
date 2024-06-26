﻿using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class KandidatiRepository : IKandidatiRepository
{
    private readonly ApplicationDbContext _context;

    public KandidatiRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Kandidati>> GetAllAsync(int pageNumber = 5, int pageSize = 100)
    {

        var skipResults = (pageNumber - 1) * pageSize;
        return await _context.Kandidati.Include(k => k.Natjecaji).Skip(skipResults).Take(pageSize).ToListAsync();
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
