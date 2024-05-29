﻿using Microsoft.EntityFrameworkCore;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Data;

public class ApplicationDbContext : DbContext
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
}
