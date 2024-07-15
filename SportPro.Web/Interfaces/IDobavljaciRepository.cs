using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IDobavljaciRepository
{
    Task<IEnumerable<Dobavljaci>> GetAllAsync(string? naziv = null, string? grad = null, string? aktivnaSuradnja = null, string? sortBy = null, string? sortDirection = null, int pageNumber = 5, int pageSize = 100);
    Task<Dobavljaci> AddAsync(Dobavljaci dobavljac);
    Task<Dobavljaci>? GetAsync(int? id);
    Task<Dobavljaci>? UpdateAsync(Dobavljaci dobavljac);
    Task<Dobavljaci>? DeleteAsync(int? id);
    Task<IEnumerable<Dobavljaci>> GetActiveDobavljaci();
    Task<int> CountAsync();
    Task<IEnumerable<Dobavljaci>> GetAllSecAsync();
}