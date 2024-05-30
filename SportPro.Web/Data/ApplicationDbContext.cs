using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Natjecaji> Natjecaji { get; set; }
    public DbSet<Zaposlenici> Zaposlenici { get; set; }
    public DbSet<Pozicije> Pozicije { get; set; }
    public DbSet<Poslovnice> Poslovnice { get; set; }
    public DbSet<Pravilnici> Pravilnici { get; set; }
    public DbSet<Boje> Boje { get; set; }
    public DbSet<Brendovi> Brendovi { get; set; }
    public DbSet<Velicine> Velicine { get; set; }
    public DbSet<VrstePlacanja> VrstePlacanja { get; set; }
    public DbSet<Dobavljaci> Dobavljaci { get; set; }
    public DbSet<TipoviPromocija> TipoviPromocija { get; set; }
    public DbSet<Promocije> Promocije { get; set; }
    public DbSet<Kategorije> Kategorije { get; set; }
    public DbSet<Artikli> Artikli { get; set; }
    public DbSet<Certifikati> Certifikati { get; set; }
    public DbSet<Kandidati> Kandidati { get; set; }
    public DbSet<Narudzbe> Narudzbe { get; set; }
    public DbSet<Dashboard> Dashboard { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Seed Roles (Uposlenik, Vlasnik, Admin, Menadzer)

        var adminRoleId = "01abf0a5-ef31-48b1-82db-7e274a7dbd71";
        var uposlenikRoleId = "ca051bf6-5592-4a4d-a9c5-5f7c1530e61d";
        var vlasnikRoleId = "bf5c73e6-1743-4c02-a224-f7d5908eaa40";
        var menadzerRoleId = "6b8f5bad-e5ca-43e9-ac46-ca02e0c96c5f";
        var roles = new List<IdentityRole>
        {
           new IdentityRole { Name = "Admin", NormalizedName = "Admin", Id = adminRoleId, ConcurrencyStamp = adminRoleId},
           new IdentityRole { Name = "Uposlenik", NormalizedName = "Uposlenik", Id = uposlenikRoleId, ConcurrencyStamp = uposlenikRoleId},
           new IdentityRole { Name = "Vlasnik", NormalizedName = "Vlasnik", Id = vlasnikRoleId, ConcurrencyStamp = vlasnikRoleId},
           new IdentityRole { Name = "Menadzer", NormalizedName = "Menadzer", Id = menadzerRoleId, ConcurrencyStamp = menadzerRoleId}

        };

        builder.Entity<IdentityRole>().HasData(roles);

        // Seed Vlasnik

        var vlasnikUserId = "098294f1-5451-4a33-a567-5922f7c39c4e";
        var vlasnikUser = new IdentityUser
        {
            Id = vlasnikUserId,
            UserName = "vlasnik",
            Email = "vlasnik@sportpro.ba",
            NormalizedEmail = "vlasnik@sportpro.ba".ToUpper(),
            NormalizedUserName = "vlasnik".ToUpper()
        };

        vlasnikUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(vlasnikUser, "Vlasnik12!");

        builder.Entity<IdentityUser>().HasData(vlasnikUser);

        var adminUserId = "2818ecc2-5e74-4df0-a503-57ea46cc3f7c";
        var adminUser = new IdentityUser
        {
            Id = adminUserId,
            UserName = "admin",
            Email = "admin@sportpro.ba",
            NormalizedEmail = "admin@sportpro.ba".ToUpper(),
            NormalizedUserName = "admin".ToUpper()
        };

        adminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(adminUser, "Admin12!");

        builder.Entity<IdentityUser>().HasData(adminUser);

        var vlasnikUserRoles = new List<IdentityUserRole<string>>
        {
            new IdentityUserRole<string> { UserId = vlasnikUserId, RoleId = adminRoleId },
            new IdentityUserRole<string> { UserId = vlasnikUserId, RoleId = uposlenikRoleId },
            new IdentityUserRole<string> { UserId = vlasnikUserId, RoleId = vlasnikRoleId },
            new IdentityUserRole<string> { UserId = vlasnikUserId, RoleId = menadzerRoleId }
        };


        var adminUserRoles = new List<IdentityUserRole<string>>
        {
            new IdentityUserRole<string> { UserId = adminUserId, RoleId = adminRoleId },
            new IdentityUserRole<string> { UserId = adminUserId, RoleId = uposlenikRoleId },
            new IdentityUserRole<string> { UserId = adminUserId, RoleId = menadzerRoleId }
        };

        builder.Entity<IdentityUserRole<string>>().HasData(vlasnikUserRoles);
        builder.Entity<IdentityUserRole<string>>().HasData(adminUserRoles);
    }
}
