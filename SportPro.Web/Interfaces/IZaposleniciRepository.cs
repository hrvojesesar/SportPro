using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IZaposleniciRepository
{
    Task<IEnumerable<Zaposlenici>> GetAllAsync(int pageNumber = 3, int pageSize = 100);
    Task<Zaposlenici> AddAsync(Zaposlenici zaposlenik);
    Task<Zaposlenici>? GetAsync(int? id);
    Task<Zaposlenici>? UpdateAsync(Zaposlenici zaposlenik);
    Task<Zaposlenici>? DeleteAsync(int? id);
    Task<int> CountAsync();
}
