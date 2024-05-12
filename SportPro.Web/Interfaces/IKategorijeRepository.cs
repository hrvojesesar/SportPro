using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IKategorijeRepository
{
    Task<IEnumerable<Kategorije>> GetAllAsync();
    Task<Kategorije> AddAsync(Kategorije kategorija);
    Task<Kategorije>? GetAsync(int? id);
    Task<Kategorije>? UpdateAsync(Kategorije kategorija);
    Task<Kategorije>? DeleteAsync(int? id);

}
