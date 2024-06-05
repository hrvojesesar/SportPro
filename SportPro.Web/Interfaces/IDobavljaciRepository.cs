using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IDobavljaciRepository
{
    Task<IEnumerable<Dobavljaci>> GetAllAsync(int pageNumber = 5, int pageSize = 100);
    Task<Dobavljaci> AddAsync(Dobavljaci dobavljac);
    Task<Dobavljaci>? GetAsync(int? id);
    Task<Dobavljaci>? UpdateAsync(Dobavljaci dobavljac);
    Task<Dobavljaci>? DeleteAsync(int? id);
    Task<IEnumerable<Dobavljaci>> GetActiveDobavljaci();
    Task<int> CountAsync();
}