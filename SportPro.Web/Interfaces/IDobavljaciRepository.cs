using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IDobavljaciRepository
{
    Task<IEnumerable<Dobavljaci>> GetAllAsync();
    Task<Dobavljaci> AddAsync(Dobavljaci dobavljac);
    Task<Dobavljaci>? GetAsync(int? id);
    Task<Dobavljaci>? UpdateAsync(Dobavljaci dobavljac);
    Task<Dobavljaci>? DeleteAsync(int? id);
    Task<IEnumerable<Dobavljaci>> GetActiveDobavljaci();
    Task<int> CountAsync();
}