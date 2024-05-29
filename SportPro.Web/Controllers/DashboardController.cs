using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Data;

namespace SportPro.Web.Controllers;

public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        double UkupniTrosak = 0;
        double UkupniPrihod = 0;

        foreach (var narudzba in _context.Narudzbe)
        {
            UkupniTrosak += narudzba.UkupniTrosak;
        }
        foreach (var artikal in _context.Artikli)
        {
            UkupniPrihod += artikal.UkupnaZarada ?? 0;
        }
        foreach (var zaposlenik in _context.Zaposlenici)
        {
            UkupniTrosak += zaposlenik.UkupnaPlaca;
        }

        double UkupniProfit = UkupniPrihod - UkupniTrosak;

        ViewBag.UkupniTrosak = UkupniTrosak;
        ViewBag.UkupniPrihod = UkupniPrihod;
        ViewBag.UkupniProfit = UkupniProfit;

        List<Test> chartData = new List<Test>
            {
                new Test { xValue = "Ukupni profit", yValue = UkupniProfit},
                new Test { xValue = "Ukupni trošak", yValue = UkupniTrosak},
            };
        ViewBag.dataSource = chartData;
        return View();
    }
    public class Test
    {
        public string xValue;
        public double yValue;
    }
}
