using Microsoft.EntityFrameworkCore;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Natjecaji> Natjecaji { get; set; }
    public DbSet<PonudePoslova> PonudePoslova { get; set; }
    public DbSet<Zaposlenici> Zaposlenici { get; set; }
    public DbSet<Pozicije> Pozicije { get; set; }
    public DbSet<Poslovnice> Poslovnice { get; set; }
    public DbSet<Pravilnici> Pravilnici { get; set; }
    public DbSet<Boje> Boje { get; set; }
    public DbSet<Brendovi> Brendovi { get; set; }
    public DbSet<Velicine> Velicine { get; set; }

public DbSet<SportPro.Web.Models.ViewModels.AddVelicinaRequest> AddVelicinaRequest { get; set; } = default!;

public DbSet<SportPro.Web.Models.ViewModels.EditVelicinaRequest> EditVelicinaRequest { get; set; } = default!;


}
