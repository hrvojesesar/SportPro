using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Repositories;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<INatjecajiRepository, NatjecajiRepository>();
builder.Services.AddScoped<IPozicijeRepository, PozicijeRepository>();
builder.Services.AddScoped<IPoslovniceRepository, PoslovniceRepository>();
builder.Services.AddScoped<IZaposleniciRepository, ZaposleniciRepository>();
builder.Services.AddScoped<IPravilniciRepository, PravilniciRepository>();
builder.Services.AddScoped<IBojeRepository, BojeRepository>();
builder.Services.AddScoped<IBrendoviRepository, BrendoviRepository>();
builder.Services.AddScoped<IVelicineRepository, VelicineRepository>();
builder.Services.AddScoped<IVrstePlacanjaRepository, VrstePlacanjaRepository>();
builder.Services.AddScoped<IDobavljaciRepository, DobavljaciRepository>();
builder.Services.AddScoped<ITipoviPromocijaRepository, TipoviPromocijaRepository>();
builder.Services.AddScoped<IPromocijeRepository, PromocijeRepository>();
builder.Services.AddScoped<IImagesRepository, ImagesRepository>();
builder.Services.AddScoped<IKategorijeRepository, KategorijeRepository>();
builder.Services.AddScoped<IArtikliRepository, ArtikliRepository>();
builder.Services.AddScoped<ICertifikatiRepository, CertifikatiRepository>();
builder.Services.AddScoped<IKandidatiRepository, KandidatiRepository>();
builder.Services.AddScoped<INarudzbeRepository, NarudzbeRepository>();




builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });
});


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});


app.Run();