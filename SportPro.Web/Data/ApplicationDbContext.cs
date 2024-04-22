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

public DbSet<SportPro.Web.Models.ViewModels.AddNatjecajRequest> AddNatjecajRequest { get; set; } = default!;

public DbSet<SportPro.Web.Models.ViewModels.EditNatjecajRequest> EditNatjecajRequest { get; set; } = default!;

}
