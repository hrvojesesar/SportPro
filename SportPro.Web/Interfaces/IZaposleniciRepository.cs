using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IZaposleniciRepository
{
    Task<IEnumerable<Zaposlenici>> GetAllAsync();
    Task<Zaposlenici> AddAsync(Zaposlenici zaposlenici);
    Task<Zaposlenici?> GetAsync(int? id);
    Task<Zaposlenici?> UpdateAsync(Zaposlenici zaposlenici);
    Task<Zaposlenici?> DeleteAsync(int id);
}
