using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Repositories;

public class ArtikliRepository : IArtikliRepository
{
    private readonly ApplicationDbContext _context;

    public ArtikliRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Artikli>> GetAllAsync()
    {
        return await _context.Artikli.Include(a => a.Boje).Include(a => a.Kategorije).Include(a => a.Velicine).Include(a => a.Poslovnice).ToListAsync();
    }

    public async Task<Artikli> AddAsync(Artikli artikal)
    {
        await _context.Artikli.AddAsync(artikal);
        await _context.SaveChangesAsync();
        return artikal;
    }

    public async Task<Artikli?> GetAsync(int? id)
    {
        return await _context.Artikli.Include(a => a.Boje).Include(a => a.Kategorije).Include(a => a.Velicine).Include(a => a.Poslovnice).FirstOrDefaultAsync(a => a.IDArtikal == id);
    }

    public async Task<Artikli?> UpdateAsync(Artikli artikal)
    {
        var existingArtikal = await _context.Artikli.Include(a => a.Boje).Include(a => a.Kategorije).Include(a => a.Velicine).Include(a => a.Poslovnice).FirstOrDefaultAsync(a => a.IDArtikal == artikal.IDArtikal);

        if (existingArtikal == null)
        {
            return null;
        }

        existingArtikal.Naziv = artikal.Naziv;
        existingArtikal.Opis = artikal.Opis;
        existingArtikal.Cijena = artikal.Cijena;
        existingArtikal.Snizen = artikal.Snizen;
        existingArtikal.SnizenaCijena = artikal.SnizenaCijena;
        existingArtikal.NabavnaKolicina = artikal.NabavnaKolicina;
        existingArtikal.TrenutnaKolicina = artikal.TrenutnaKolicina;
        existingArtikal.NaStanju = artikal.NaStanju;
        existingArtikal.DatumNabave = artikal.DatumNabave;
        existingArtikal.NabavnaCijena = artikal.NabavnaCijena;
        existingArtikal.CijenaDostave = artikal.CijenaDostave;
        existingArtikal.UkupniTrosak = artikal.UkupniTrosak;
        existingArtikal.DobavljacIDDobavljac = artikal.DobavljacIDDobavljac;
        existingArtikal.BrendIDBrend = artikal.BrendIDBrend;
        existingArtikal.Kategorije = artikal.Kategorije;
        existingArtikal.Boje = artikal.Boje;
        existingArtikal.Velicine = artikal.Velicine;
        existingArtikal.Poslovnice = artikal.Poslovnice;


        await _context.SaveChangesAsync();
        return existingArtikal;
    }

    public async Task<Artikli?> DeleteAsync(int? id)
    {
        var artikal = await _context.Artikli.FirstOrDefaultAsync(a => a.IDArtikal == id);

        if (artikal == null)
        {
            return null;
        }

        _context.Artikli.Remove(artikal);
        await _context.SaveChangesAsync();
        return artikal;
    }

}
