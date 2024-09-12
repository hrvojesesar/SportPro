using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SportPro.Web;
using SportPro.Web.Data;
using SportPro.Web.Interfaces;
using SportPro.Web.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(x => x.BufferBody = true);
builder.Services.AddControllersWithViews().AddMvcOptions(options =>
{
    options.RespectBrowserAcceptHeader = true; // Enable respect for UTF-8
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.DictionaryKeyPolicy = null;
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredUniqueChars = 1;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(30); // Produženi vijek trajanja kolaèiæa

    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".SportPro.Session";
    options.IdleTimeout = TimeSpan.FromHours(1); // Produženi vijek trajanja sesije
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<INatjecajiRepository, NatjecajiRepository>();
builder.Services.AddScoped<IPozicijeRepository, PozicijeRepository>();
builder.Services.AddScoped<IPoslovniceRepository, PoslovniceRepository>();
builder.Services.AddScoped<IZaposleniciRepository, ZaposleniciRepository>();
builder.Services.AddScoped<IPravilniciRepository, PravilniciRepository>();
builder.Services.AddScoped<IBojeRepository, BojeRepository>();
builder.Services.AddScoped<IBrendoviRepository, BrendoviRepository>();
builder.Services.AddScoped<IVelicineRepository, VelicineRepository>();
builder.Services.AddScoped<IDobavljaciRepository, DobavljaciRepository>();
builder.Services.AddScoped<ITipoviPromocijaRepository, TipoviPromocijaRepository>();
builder.Services.AddScoped<IPromocijeRepository, PromocijeRepository>();
builder.Services.AddScoped<IImagesRepository, ImagesRepository>();
builder.Services.AddScoped<IKategorijeRepository, KategorijeRepository>();
builder.Services.AddScoped<IArtikliRepository, ArtikliRepository>();
builder.Services.AddScoped<ICertifikatiRepository, CertifikatiRepository>();
builder.Services.AddScoped<IKandidatiRepository, KandidatiRepository>();
builder.Services.AddScoped<INarudzbeRepository, NarudzbeRepository>();
builder.Services.AddScoped<IKategorijeTroskovaRepository, KategorijeTroskovaRepository>();
builder.Services.AddScoped<ITroskoviRepository, TroskoviRepository>();
builder.Services.AddScoped<IKategorijePrihodaRepository, KategorijePrihodaRepository>();
builder.Services.AddScoped<IPrihodiRepository, PrihodiRepository>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });

    //Add JWT Authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
      {
        new OpenApiSecurityScheme
        {
          Reference = new OpenApiReference
          {
           Type = ReferenceType.SecurityScheme,
           Id = "Bearer"
          }
        },
        new string[] { }
      }
    });
});

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".SportPro.Session";
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseRedirectMiddleware();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
