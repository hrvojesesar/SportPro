﻿
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportPro.Web.Data;

#nullable disable

namespace SportPro.Web.Migrations;

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

        modelBuilder.Entity("SportPro.Web.Models.Domains.PonudePoslova", b =>
            {
                b.Property<int>("IDPonudaPoslova")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDPonudaPoslova"));

                b.Property<int>("BrojOsoba")
                    .HasColumnType("int");

                b.Property<double>("BrojSati")
                    .HasColumnType("float");

                b.Property<double>("CijenaSata")
                    .HasColumnType("float");

                b.Property<DateTime>("KrajRadova")
                    .HasColumnType("datetime2");

                b.Property<string>("Lokacija")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Naziv")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("OpisPosla")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("PocetakRadova")
                    .HasColumnType("datetime2");

                b.Property<string>("PotrebnaOprema")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("IDPonudaPoslova");

                b.ToTable("PonudePoslova");
            });
#pragma warning restore 612, 618
    }
}
