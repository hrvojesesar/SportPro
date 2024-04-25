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
}
