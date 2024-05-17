﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportPro.Web.Data;

#nullable disable

namespace SportPro.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ArtikliBoje", b =>
                {
                    b.Property<int>("ArtikliIDArtikal")
                        .HasColumnType("int");

                    b.Property<int>("BojeIDBoja")
                        .HasColumnType("int");

                    b.HasKey("ArtikliIDArtikal", "BojeIDBoja");

                    b.HasIndex("BojeIDBoja");

                    b.ToTable("ArtikliBoje");
                });

            modelBuilder.Entity("ArtikliKategorije", b =>
                {
                    b.Property<int>("ArtikliIDArtikal")
                        .HasColumnType("int");

                    b.Property<int>("KategorijeIDKategorija")
                        .HasColumnType("int");

                    b.HasKey("ArtikliIDArtikal", "KategorijeIDKategorija");

                    b.HasIndex("KategorijeIDKategorija");

                    b.ToTable("ArtikliKategorije");
                });

            modelBuilder.Entity("ArtikliPoslovnice", b =>
                {
                    b.Property<int>("ArtikliIDArtikal")
                        .HasColumnType("int");

                    b.Property<int>("PoslovniceIDPoslovnica")
                        .HasColumnType("int");

                    b.HasKey("ArtikliIDArtikal", "PoslovniceIDPoslovnica");

                    b.HasIndex("PoslovniceIDPoslovnica");

                    b.ToTable("ArtikliPoslovnice");
                });

            modelBuilder.Entity("ArtikliVelicine", b =>
                {
                    b.Property<int>("ArtikliIDArtikal")
                        .HasColumnType("int");

                    b.Property<int>("VelicineIDVelicina")
                        .HasColumnType("int");

                    b.HasKey("ArtikliIDArtikal", "VelicineIDVelicina");

                    b.HasIndex("VelicineIDVelicina");

                    b.ToTable("ArtikliVelicine");
                });

            modelBuilder.Entity("PozicijeZaposlenici", b =>
                {
                    b.Property<int>("PozicijeIDPozicija")
                        .HasColumnType("int");

                    b.Property<int>("ZaposleniciIDZaposlenik")
                        .HasColumnType("int");

                    b.HasKey("PozicijeIDPozicija", "ZaposleniciIDZaposlenik");

                    b.HasIndex("ZaposleniciIDZaposlenik");

                    b.ToTable("PozicijeZaposlenici");
                });

            modelBuilder.Entity("SportPro.Web.Models.Domains.Artikli", b =>
                {
                    b.Property<int>("IDArtikal")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDArtikal"));

                    b.Property<int>("BrendIDBrend")
                        .HasColumnType("int");

                    b.Property<double>("Cijena")
                        .HasColumnType("float");

                    b.Property<double>("CijenaDostave")
                        .HasColumnType("float");

                    b.Property<DateTime>("DatumNabave")
                        .HasColumnType("datetime2");

                    b.Property<int>("DobavljacIDDobavljac")
                        .HasColumnType("int");

                    b.Property<bool>("NaStanju")
                        .HasColumnType("bit");

                    b.Property<double>("NabavnaCijena")
                        .HasColumnType("float");

                    b.Property<int>("NabavnaKolicina")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Snizen")
                        .HasColumnType("bit");

                    b.Property<double>("SnizenaCijena")
                        .HasColumnType("float");

                    b.Property<int>("TrenutnaKolicina")
                        .HasColumnType("int");

                    b.Property<double>("UkupniTrosak")
                        .HasColumnType("float");

                    b.HasKey("IDArtikal");

                    b.HasIndex("BrendIDBrend");

                    b.HasIndex("DobavljacIDDobavljac");

                    b.ToTable("Artikli");
                });

            modelBuilder.Entity("SportPro.Web.Models.Domains.Boje", b =>
                {
                    b.Property<int>("IDBoja")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDBoja"));

                    b.Property<string>("NazivBoje")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDBoja");

                    b.ToTable("Boje");
                });

            modelBuilder.Entity("SportPro.Web.Models.Domains.Brendovi", b =>
                {
                    b.Property<int>("IDBrend")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDBrend"));

                    b.Property<int>("GodinaOsnivanja")
                        .HasColumnType("int");

                    b.Property<string>("NazivBrenda")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Osnivac")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Predsjednik")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sjediste")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vrsta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDBrend");

                    b.ToTable("Brendovi");
                });

            modelBuilder.Entity("SportPro.Web.Models.Domains.Dobavljaci", b =>
                {
                    b.Property<int>("IDDobavljac")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDDobavljac"));

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Drzava")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("KrajSuradnje")
                        .HasColumnType("datetime2");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PocetakSuradnje")
                        .HasColumnType("datetime2");

                    b.Property<string>("SuradnjaAktivna")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDDobavljac");

                    b.ToTable("Dobavljaci");
                });

            modelBuilder.Entity("SportPro.Web.Models.Domains.Kategorije", b =>
                {
                    b.Property<int>("IDKategorija")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDKategorija"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDKategorija");

                    b.ToTable("Kategorije");
                });

            modelBuilder.Entity("SportPro.Web.Models.Domains.Natjecaji", b =>
                {
                    b.Property<int>("IDNatjecaj")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDNatjecaj"));

                    b.Property<bool>("Aktivan")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DatumObjave")
                        .HasColumnType("datetime2");

                    b.Property<string>("Dobitnik")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProcijenjenaVrijednost")
                        .HasColumnType("int");

                    b.Property<DateTime>("TrajanjeDo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TrajanjeOd")
                        .HasColumnType("datetime2");

                    b.HasKey("IDNatjecaj");

                    b.ToTable("Natjecaji");
                });

            modelBuilder.Entity("SportPro.Web.Models.Domains.Poslovnice", b =>
                {
                    b.Property<int>("IDPoslovnica")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDPoslovnica"));

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DatumOtvaranja")
                        .HasColumnType("datetime2");

                    b.Property<string>("Drzava")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan?>("RadnoVrijemeDo")
                        .HasColumnType("time");

                    b.Property<TimeSpan?>("RadnoVrijemeOd")
                        .HasColumnType("time");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDPoslovnica");

                    b.ToTable("Poslovnice");
                });

            modelBuilder.Entity("SportPro.Web.Models.Domains.Pozicije", b =>
                {
                    b.Property<int>("IDPozicija")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDPozicija"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDPozicija");

                    b.ToTable("Pozicije");
                });

            modelBuilder.Entity("SportPro.Web.Models.Domains.Pravilnici", b =>
                {
                    b.Property<int>("IDPravilnik")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDPravilnik"));

                    b.Property<bool>("Aktivan")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DatumObjavljivanja")
                        .HasColumnType("datetime2");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDPravilnik");

                    b.ToTable("Pravilnici");
                });

            modelBuilder.Entity("SportPro.Web.Models.Domains.ProSportKorisnici", b =>
                {
                    b.Property<int>("IDKorisnik")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDKorisnik"));

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatumRegistracije")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumRodenja")
                        .HasColumnType("datetime2");

                    b.Property<string>("Drzava")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lozinka")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostanskiBroj")
                        .HasColumnType("int");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Spol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Verificiran")
                        .HasColumnType("bit");

                    b.HasKey("IDKorisnik");

                    b.ToTable("ProSportKorisnici");
                });

            modelBuilder.Entity("SportPro.Web.Models.Domains.Promocije", b =>
                {
                    b.Property<int>("IDPromocije")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDPromocije"));

                    b.Property<bool>("Aktivna")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DatumPocetka")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumZavrsetka")
                        .HasColumnType("datetime2");

                    b.Property<string>("DodatniUvjeti")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slika")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoviPromocijaIDTipPromocije")
                        .HasColumnType("int");

                    b.HasKey("IDPromocije");

                    b.HasIndex("TipoviPromocijaIDTipPromocije");

                    b.ToTable("Promocije");
                });

            modelBuilder.Entity("SportPro.Web.Models.Domains.TipoviPromocija", b =>
                {
                    b.Property<int>("IDTipPromocije")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDTipPromocije"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDTipPromocije");

                    b.ToTable("TipoviPromocija");
                });

            modelBuilder.Entity("SportPro.Web.Models.Domains.Velicine", b =>
                {
                    b.Property<int>("IDVelicina")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDVelicina"));

                    b.Property<string>("Velicina")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDVelicina");

                    b.ToTable("Velicine");
                });

            modelBuilder.Entity("SportPro.Web.Models.Domains.VrstePlacanja", b =>
                {
                    b.Property<int>("IDVrstaPlacanja")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDVrstaPlacanja"));

                    b.Property<bool>("Aktivno")
                        .HasColumnType("bit");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDVrstaPlacanja");

                    b.ToTable("VrstePlacanja");
                });

            modelBuilder.Entity("SportPro.Web.Models.Domains.Zaposlenici", b =>
                {
                    b.Property<int>("IDZaposlenik")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDZaposlenik"));

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojBankovnogRacuna")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Certifikati")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatumZaposlenja")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DatumZavrsetkaRadnogOdnosa")
                        .HasColumnType("datetime2");

                    b.Property<string>("Drzava")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JMBG")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kvalifikacija")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PoslovnicaID")
                        .HasColumnType("int");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Spol")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDZaposlenik");

                    b.HasIndex("PoslovnicaID");

                    b.ToTable("Zaposlenici");
                });

            modelBuilder.Entity("ArtikliBoje", b =>
                {
                    b.HasOne("SportPro.Web.Models.Domains.Artikli", null)
                        .WithMany()
                        .HasForeignKey("ArtikliIDArtikal")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportPro.Web.Models.Domains.Boje", null)
                        .WithMany()
                        .HasForeignKey("BojeIDBoja")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ArtikliKategorije", b =>
                {
                    b.HasOne("SportPro.Web.Models.Domains.Artikli", null)
                        .WithMany()
                        .HasForeignKey("ArtikliIDArtikal")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportPro.Web.Models.Domains.Kategorije", null)
                        .WithMany()
                        .HasForeignKey("KategorijeIDKategorija")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ArtikliPoslovnice", b =>
                {
                    b.HasOne("SportPro.Web.Models.Domains.Artikli", null)
                        .WithMany()
                        .HasForeignKey("ArtikliIDArtikal")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportPro.Web.Models.Domains.Poslovnice", null)
                        .WithMany()
                        .HasForeignKey("PoslovniceIDPoslovnica")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ArtikliVelicine", b =>
                {
                    b.HasOne("SportPro.Web.Models.Domains.Artikli", null)
                        .WithMany()
                        .HasForeignKey("ArtikliIDArtikal")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportPro.Web.Models.Domains.Velicine", null)
                        .WithMany()
                        .HasForeignKey("VelicineIDVelicina")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PozicijeZaposlenici", b =>
                {
                    b.HasOne("SportPro.Web.Models.Domains.Pozicije", null)
                        .WithMany()
                        .HasForeignKey("PozicijeIDPozicija")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportPro.Web.Models.Domains.Zaposlenici", null)
                        .WithMany()
                        .HasForeignKey("ZaposleniciIDZaposlenik")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportPro.Web.Models.Domains.Artikli", b =>
                {
                    b.HasOne("SportPro.Web.Models.Domains.Brendovi", "Brend")
                        .WithMany("Artikli")
                        .HasForeignKey("BrendIDBrend")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportPro.Web.Models.Domains.Dobavljaci", "Dobavljac")
                        .WithMany("Artikli")
                        .HasForeignKey("DobavljacIDDobavljac")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brend");

                    b.Navigation("Dobavljac");
                });

            modelBuilder.Entity("SportPro.Web.Models.Domains.Promocije", b =>
                {
                    b.HasOne("SportPro.Web.Models.Domains.TipoviPromocija", "TipoviPromocija")
                        .WithMany("Promocije")
                        .HasForeignKey("TipoviPromocijaIDTipPromocije")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoviPromocija");
                });

            modelBuilder.Entity("SportPro.Web.Models.Domains.Zaposlenici", b =>
                {
                    b.HasOne("SportPro.Web.Models.Domains.Poslovnice", "Poslovnica")
                        .WithMany("Zaposlenici")
                        .HasForeignKey("PoslovnicaID");

                    b.Navigation("Poslovnica");
                });

            modelBuilder.Entity("SportPro.Web.Models.Domains.Brendovi", b =>
                {
                    b.Navigation("Artikli");
                });

            modelBuilder.Entity("SportPro.Web.Models.Domains.Dobavljaci", b =>
                {
                    b.Navigation("Artikli");
                });

            modelBuilder.Entity("SportPro.Web.Models.Domains.Poslovnice", b =>
                {
                    b.Navigation("Zaposlenici");
                });

            modelBuilder.Entity("SportPro.Web.Models.Domains.TipoviPromocija", b =>
                {
                    b.Navigation("Promocije");
                });
#pragma warning restore 612, 618
        }
    }
}
