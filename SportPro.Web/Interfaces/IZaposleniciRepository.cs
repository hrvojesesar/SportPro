using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IZaposleniciRepository
{
    Task<IEnumerable<Zaposlenici>> GetAllAsync(string? ime = null, string? prezime = null, string? grad = null, string? status = null, string? poslovnica = null, string? sortBy = null, string? sortDirection = null, int pageNumber = 5, int pageSize = 100);
    Task<Zaposlenici> AddAsync(Zaposlenici zaposlenik);
    Task<Zaposlenici>? GetAsync(int? id);
    Task<Zaposlenici>? UpdateAsync(Zaposlenici zaposlenik);
    Task<Zaposlenici>? DeleteAsync(int? id);
    Task<int> CountAsync();
}
